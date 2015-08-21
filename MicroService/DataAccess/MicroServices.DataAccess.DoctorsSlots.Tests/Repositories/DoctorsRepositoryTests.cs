using System.Linq;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;
using MicroServices.DataAccess.DoctorsSlots.Repositories;
using NSubstitute;
using NSubstitute.Core;
using Xunit;

namespace MicroServices.DataAccess.DoctorsSlots.Tests.Repositories
{
    public sealed class DoctorsRepositoryTests
    {
        [Fact]
        public void FindByLastName_ReturnsCorrectNumberOfDoctors_ForKnownLastName()
        {
            // Arrange
            var context = Substitute.For <IDoctorsContext>();
            context.Doctors().Returns(CreateDoctors);
            var sut = CreateSut(context);

            // Act
            var actual = sut.FindByLastName("One");

            // Assert
            Assert.Equal(1,
                         actual.Count());
        }
        
        [Fact]
        public void FindByLastName_ReturnsDoctors_ForKnownLastName()
        {
            // Arrange
            var context = Substitute.For<IDoctorsContext>();
            context.Doctors().Returns(CreateDoctors);
            var sut = CreateSut(context);

            // Act
            var actual = sut.FindByLastName("One").First();

            // Assert
            Assert.Equal("One",
                         actual.LastName);
        }

        private IQueryable <IDoctor> CreateDoctors(CallInfo arg)
        {
            var one = Substitute.For <IDoctor>();
            one.LastName = "One";

            var two = Substitute.For<IDoctor>();
            two.LastName = "Two";

            return new[]
                   {
                       one,
                       two
                   }.AsQueryable();
        }

        private DoctorsRepository CreateSut(IDoctorsContext context)
        {
            return new DoctorsRepository(context);
        }
    }
}