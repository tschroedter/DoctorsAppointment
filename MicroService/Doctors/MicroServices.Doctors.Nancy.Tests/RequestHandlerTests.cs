using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using MicroServices.Common.Tests;
using MicroServices.Doctors.Nancy.Interfaces;
using Nancy;
using NSubstitute;
using NSubstitute.Core;
using Xunit;
using Xunit.Extensions;

namespace MicroServices.Doctors.Nancy.Tests
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
        public void FindById_ReturnsResponse_ForKnownId([NotNull] IDoctorForResponse doctor)
        {
            // Arrange
            var finder = Substitute.For <IInformationFinder>();
            finder.FindById(doctor.Id).Returns(doctor);
            RequestHandler sut = CreateSut(finder);

            // Act
            Response actual = sut.FindById(doctor.Id);

            // Assert
            Assert.Equal(HttpStatusCode.OK,
                         actual.StatusCode);
        }

        [Theory]
        [AutoNSubstituteData]
        public void FindById_ReturnsResponse_ForUnknownId()
        {
            // Arrange
            var finder = Substitute.For <IInformationFinder>();
            finder.FindById(-1).Returns(( IDoctorForResponse ) null);
            RequestHandler sut = CreateSut(finder);

            // Act
            Response actual = sut.FindById(-1);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound,
                         actual.StatusCode);
        }

        [Theory]
        [AutoNSubstituteData]
        public void FindByLastName_ReturnsResponse_ForUnknownId()
        {
            // Arrange
            var finder = Substitute.For <IInformationFinder>();
            finder.FindById(-1).Returns(( IDoctorForResponse ) null);
            RequestHandler sut = CreateSut(finder);

            // Act
            Response actual = sut.FindByLastName("Unknown");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound,
                         actual.StatusCode);
        }

        [Theory]
        [AutoNSubstituteData]
        public void FindByLastName_ReturnsResponseConflict_ForMoreThanOneDoctor([NotNull] IDoctorForResponse doctor)
        {
            // Arrange
            var finder = Substitute.For <IInformationFinder>();
            finder.FindByLastName(doctor.LastName).Returns(CreateList);
            RequestHandler sut = CreateSut(finder);

            // Act
            Response actual = sut.FindByLastName(doctor.LastName);

            // Assert
            Assert.Equal(HttpStatusCode.Conflict,
                         actual.StatusCode);
        }

        [Theory]
        [AutoNSubstituteData]
        public void FindByLastName_ReturnsResponseOK_ForMoreThanOneDoctor([NotNull] IDoctorForResponse doctor)
        {
            // Arrange
            IEnumerable <IDoctorForResponse> list = CreateListOneDoctor(doctor);
            var finder = Substitute.For <IInformationFinder>();
            finder.FindByLastName(doctor.LastName).Returns(list);
            RequestHandler sut = CreateSut(finder);

            // Act
            Response actual = sut.FindByLastName(doctor.LastName);

            // Assert
            Assert.Equal(HttpStatusCode.OK,
                         actual.StatusCode);
        }

        private IEnumerable <IDoctorForResponse> CreateListOneDoctor(IDoctorForResponse doctor)
        {
            var list = new Collection <IDoctorForResponse>
                       {
                           doctor
                       };


            return list.AsQueryable();
        }

        private IQueryable <IDoctorForResponse> CreateList(CallInfo arg)
        {
            var list = new Collection <IDoctorForResponse>
                       {
                           Substitute.For <IDoctorForResponse>(),
                           Substitute.For <IDoctorForResponse>()
                       };


            return list.AsQueryable();
        }

        private RequestHandler CreateSut(IInformationFinder finder)
        {
            return new RequestHandler(finder);
        }
    }
}