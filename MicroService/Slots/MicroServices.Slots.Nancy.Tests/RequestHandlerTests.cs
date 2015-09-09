using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using MicroServices.Common.Tests;
using MicroServices.Slots.Nancy.Interfaces;
using Nancy;
using NSubstitute;
using NSubstitute.Core;
using Xunit;
using Xunit.Extensions;

namespace MicroServices.Slots.Nancy.Tests
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public sealed class RequestHandlerTests
    {
        // todo don't know how to get content, but integration test cover this
        private const int IdDoesNotMatter = -1;

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
        public void FindById_ReturnsResponse_ForKnownId(
            [NotNull] ISlotForResponse slot)
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
        public void FindById_ReturnsResponse_ForUnknownId()
        {
            // Arrange
            var finder = Substitute.For <IInformationFinder>();
            finder.FindById(IdDoesNotMatter).Returns(( ISlotForResponse ) null);
            RequestHandler sut = CreateSut(finder);

            // Act
            Response actual = sut.FindById(IdDoesNotMatter);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound,
                         actual.StatusCode);
        }

        [Theory]
        [AutoNSubstituteData]
        public void Save_ReturnsResponse_ForSlot([NotNull] ISlotForResponse slot)
        {
            // Arrange
            var finder = Substitute.For <IInformationFinder>();
            finder.FindById(Arg.Any <int>()).Returns(slot);
            RequestHandler sut = CreateSut(finder);

            // Act
            Response actual = sut.Save(slot);

            // Assert
            Assert.Equal(HttpStatusCode.OK,
                         actual.StatusCode);
        }

        [Theory]
        [AutoNSubstituteData]
        public void DeleteById_ReturnsResponse_WhenCalled([NotNull] ISlotForResponse slot)
        {
            // Arrange
            var finder = Substitute.For <IInformationFinder>();
            finder.Delete(slot.Id).Returns(slot);
            RequestHandler sut = CreateSut(finder);

            // Act
            Response actual = sut.DeleteById(slot.Id);

            // Assert
            Assert.Equal(HttpStatusCode.OK,
                         actual.StatusCode);
        }

        [Fact]
        public void DeleteById_ReturnsResponse_ForAddFailed()
        {
            // Arrange
            var finder = Substitute.For <IInformationFinder>();
            finder.Delete(IdDoesNotMatter).Returns(( ISlotForResponse ) null);
            RequestHandler sut = CreateSut(finder);

            // Act
            Response actual = sut.DeleteById(IdDoesNotMatter);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound,
                         actual.StatusCode);
        }

        private IQueryable <ISlotForResponse> CreateList(CallInfo arg)
        {
            var list = new Collection <ISlotForResponse>
                       {
                           Substitute.For <ISlotForResponse>(),
                           Substitute.For <ISlotForResponse>()
                       };


            return list.AsQueryable();
        }

        private RequestHandler CreateSut(IInformationFinder finder)
        {
            return new RequestHandler(finder);
        }
    }
}