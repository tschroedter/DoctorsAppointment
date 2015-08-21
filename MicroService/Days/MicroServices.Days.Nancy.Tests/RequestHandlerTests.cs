using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using MicroServices.Common.Tests;
using MicroServices.Days.Nancy.Interfaces;
using Nancy;
using NSubstitute;
using NSubstitute.Core;
using Xunit;
using Xunit.Extensions;

namespace MicroServices.Days.Nancy.Tests
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public sealed class RequestHandlerTests
    {
        // todo don't know how to get content, but integration test cover this

        [Fact]
        public void List_ReturnsResponse_WhenCalled()
        {
            // Arrange
            var finder = Substitute.For <IInformationFinder>();
            finder.List().Returns(CreateList);
            RequestHandler sut = CreateSut(finder);

            // Act
            Response actual = sut.List();

            // Assert
            Assert.Equal(HttpStatusCode.OK,
                         actual.StatusCode);
        }

        [Theory]
        [AutoNSubstituteData]
        public void FindByDate_ReturnsResponse_ForKnownDate([NotNull] IDayForResponse slot)
        {
            // Arrange
            var finder = Substitute.For <IInformationFinder>();
            finder.ListForDate(Arg.Any <string>()).Returns(CreateList);
            RequestHandler sut = CreateSut(finder);

            // Act
            Response actual = sut.FindByDate("any date");

            // Assert
            Assert.Equal(HttpStatusCode.OK,
                         actual.StatusCode);
        }

        [Theory]
        [AutoNSubstituteData]
        public void Find_ReturnsResponse_ForKnownDate([NotNull] IDayForResponse slot)
        {
            // Arrange
            var finder = Substitute.For <IInformationFinder>();
            finder.ListForDateAndDoctorId(Arg.Any <string>(),
                                          Arg.Any <string>()).Returns(CreateList);
            RequestHandler sut = CreateSut(finder);

            // Act
            Response actual = sut.Find("any date",
                                       "-1");

            // Assert
            Assert.Equal(HttpStatusCode.OK,
                         actual.StatusCode);
        }

        [Theory]
        [AutoNSubstituteData]
        public void FindById_ReturnsResponse_ForKnownId([NotNull] IDayForResponse slot)
        {
            // Arrange
            var finder = Substitute.For <IInformationFinder>();
            finder.FindById(slot.Id).Returns(slot);
            RequestHandler sut = CreateSut(finder);

            // Act
            Response actual = sut.FindById(slot.Id);

            // Assert
            Assert.Equal(HttpStatusCode.OK,
                         actual.StatusCode);
        }

        [Theory]
        [AutoNSubstituteData]
        public void FindByDoctorId_ReturnsResponse_ForKnownId([NotNull] IDayForResponse slot)
        {
            // Arrange
            var finder = Substitute.For <IInformationFinder>();
            finder.FindByDoctorId(1).Returns(new[]
                                             {
                                                 slot
                                             });
            RequestHandler sut = CreateSut(finder);

            // Act
            Response actual = sut.FindByDoctorId(1);

            // Assert
            Assert.Equal(HttpStatusCode.OK,
                         actual.StatusCode);
        }

        [Fact]
        public void FindByDoctorId_ReturnsResponseNotFound_ForUnknownId()
        {
            // Arrange
            var finder = Substitute.For <IInformationFinder>();
            finder.FindByDoctorId(-1).Returns(new IDayForResponse[0]);
            RequestHandler sut = CreateSut(finder);

            // Act
            Response actual = sut.FindByDoctorId(-1);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound,
                         actual.StatusCode);
        }

        [Fact]
        public void FindByDoctorId_ReturnsResponse_ForMultiple()
        {
            // Arrange
            var finder = Substitute.For <IInformationFinder>();
            finder.FindByDoctorId(-1).Returns(CreateList);
            RequestHandler sut = CreateSut(finder);

            // Act
            Response actual = sut.FindByDoctorId(-1);

            // Assert
            Assert.Equal(HttpStatusCode.OK,
                         actual.StatusCode);
        }

        [Fact]
        public void FindById_ReturnsResponse_ForUnknownId()
        {
            // Arrange
            var finder = Substitute.For <IInformationFinder>();
            finder.FindById(-1).Returns(( IDayForResponse ) null);
            RequestHandler sut = CreateSut(finder);

            // Act
            Response actual = sut.FindById(-1);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound,
                         actual.StatusCode);
        }

        private IQueryable <IDayForResponse> CreateList(CallInfo arg)
        {
            var list = new Collection <IDayForResponse>
                       {
                           Substitute.For <IDayForResponse>(),
                           Substitute.For <IDayForResponse>()
                       };


            return list.AsQueryable();
        }

        private RequestHandler CreateSut(IInformationFinder finder)
        {
            return new RequestHandler(finder);
        }
    }
}