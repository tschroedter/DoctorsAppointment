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
    public sealed class DoctorsSlotsRepositoryTests
    {
        [Fact]
        public void FindSlotsForDoctorByLastName_ReturnsSlots_ForKnownDoctorId()
        {
            // Arrange
            var repository = Substitute.For <IDoctorsRepository>();
            repository.FindById(Arg.Any <int>()).Returns(CreateDoctor);
            DoctorsSlotsRepository sut = CreateSut(repository);

            // Act
            IEnumerable <ISlot> actual = sut.FindSlotsForDoctorByDoctorId(1);

            // Assert
            Assert.Equal(2,
                         actual.Count());
        }

        [Fact]
        public void FindSlotsForDoctorByLastName_ReturnsEmpty_ForUnknownDoctorId()
        {
            // Arrange
            var repository = Substitute.For <IDoctorsRepository>();
            repository.FindById(Arg.Any <int>()).Returns(( IDoctor ) null);
            DoctorsSlotsRepository sut = CreateSut(repository);

            // Act
            IEnumerable <ISlot> actual = sut.FindSlotsForDoctorByDoctorId(1);

            // Assert
            Assert.Equal(0,
                         actual.Count());
        }

        private static IDoctor CreateDoctor()
        {
            var day = Substitute.For <IDay>();
            day.AppointmentSlots().Returns(new[]
                                           {
                                               Substitute.For <ISlot>(),
                                               Substitute.For <ISlot>()
                                           });

            var days = new[]
                       {
                           day
                       };

            var one = Substitute.For <IDoctor>();
            one.LastName = "Miller";
            one.FirstName = "Mary";
            one.AppointmentDays.Returns(days);
            return one;
        }

        private IDoctor CreateDoctor(CallInfo arg)
        {
            return CreateDoctor();
        }

        private DoctorsSlotsRepository CreateSut([NotNull] IDoctorsRepository repository)
        {
            return new DoctorsSlotsRepository(repository);
        }
    }
}