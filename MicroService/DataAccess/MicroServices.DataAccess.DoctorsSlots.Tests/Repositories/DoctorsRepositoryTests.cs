using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;
using MicroServices.DataAccess.DoctorsSlots.Repositories;
using NSubstitute;
using NSubstitute.Core;
using Xunit;

namespace MicroServices.DataAccess.DoctorsSlots.Tests.Repositories
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public sealed class DoctorsRepositoryTests
    {
        [Fact]
        public void FindByLastName_ReturnsCorrectNumberOfDoctors_ForKnownLastName()
        {
            // Arrange
            var context = Substitute.For <IDoctorsContext>();
            context.Doctors().Returns(CreateDoctors);
            DoctorsRepository sut = CreateSut(context);

            // Act
            IEnumerable <IDoctor> actual = sut.FindByLastName("One");

            // Assert
            Assert.Equal(1,
                         actual.Count());
        }

        [Fact]
        public void FindByLastName_ReturnsDoctors_ForKnownLastName()
        {
            // Arrange
            var context = Substitute.For <IDoctorsContext>();
            context.Doctors().Returns(CreateDoctors);
            DoctorsRepository sut = CreateSut(context);

            // Act
            IDoctor actual = sut.FindByLastName("One").First();

            // Assert
            Assert.Equal("One",
                         actual.LastName);
        }

        private IQueryable <IDoctor> CreateDoctors(CallInfo arg)
        {
            var one = Substitute.For <IDoctor>();
            one.LastName = "One";

            var two = Substitute.For <IDoctor>();
            two.LastName = "Two";

            return new[]
                   {
                       one,
                       two
                   }.AsQueryable();
        }

        private DoctorsRepository CreateSut([NotNull] IDoctorsContext context)
        {
            return new DoctorsRepository(context);
        }
    }
}