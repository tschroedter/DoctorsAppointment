using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;
using MicroServices.DoctorsSlots.Nancy.Interfaces;
using Nancy;
using NSubstitute;
using NSubstitute.Core;
using Xunit;

namespace MicroServices.DoctorsSlots.Nancy.Tests
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public sealed class RequestHandlerTests
    {
        // todo don't know how to get content, but integration test cover this

        // Attention: InformationFinder tests cover all List() cases!!!
        [Fact]
        public void List_ReturnsResponse_WhenCalled()
        {
            // Arrange
            var finder = Substitute.For <IInformationFinder>();
            finder.List(Arg.Any <string>(),
                        Arg.Any <string>(),
                        Arg.Any <string>()).Returns(CreateList);
            RequestHandler sut = CreateSut(finder);

            // Act
            Response actual = sut.List("doesn't matter",
                                       "doesn't matter",
                                       "doesn't matter");

            // Assert
            Assert.Equal(HttpStatusCode.OK,
                         actual.StatusCode);
        }

        private IQueryable <ISlot> CreateList(CallInfo arg)
        {
            var list = new Collection <ISlot>
                       {
                           Substitute.For <ISlot>(),
                           Substitute.For <ISlot>()
                       };


            return list.AsQueryable();
        }

        private RequestHandler CreateSut(IInformationFinder finder)
        {
            return new RequestHandler(finder);
        }
    }
}