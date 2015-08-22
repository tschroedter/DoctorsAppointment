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

        [Fact]
        public void FindSlotsForDoctorByLastName_ReturnsSlots_ForKnown()
        {
            // Arrange
            var repository = Substitute.For <IDoctorsRepository>();
            repository.FindByLastName(Arg.Any <string>()).Returns(CreateDoctors);
            DoctorsSlotsRepository sut = CreateSut(repository);

            // Act
            IEnumerable <ISlot> actual = sut.FindSlotsForDoctorByLastName("Miller");

            // Assert
            Assert.Equal(2,
                         actual.Count());
        }

        [Fact]
        public void FindSlotsForDoctorByLastName_ReturnsEmpty_ForUnknown()
        {
            // Arrange
            var repository = Substitute.For <IDoctorsRepository>();
            repository.FindByLastName(Arg.Any <string>()).Returns(new IDoctor[0]);
            DoctorsSlotsRepository sut = CreateSut(repository);

            // Act
            IEnumerable <ISlot> actual = sut.FindSlotsForDoctorByLastName("Unknown");

            // Assert
            Assert.Equal(0,
                         actual.Count());
        }

        [Fact]
        public void FindSlotsForDoctorByLastName_ReturnsEmpty_ForMultipleDoctors()
        {
            // Arrange
            var repository = Substitute.For <IDoctorsRepository>();
            repository.FindByLastName(Arg.Any <string>()).Returns(CreateDoctorsWithSameLastName);
            DoctorsSlotsRepository sut = CreateSut(repository);

            // Act
            IEnumerable <ISlot> actual = sut.FindSlotsForDoctorByLastName("Miller");

            // Assert
            Assert.Equal(0,
                         actual.Count());
        }

        private IEnumerable <IDoctor> CreateDoctors([NotNull] CallInfo arg)
        {
            IDoctor one = CreateDoctor();

            var list = new[]
                       {
                           one
                       };

            return list;
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

        private IEnumerable <IDoctor> CreateDoctorsWithSameLastName([NotNull] CallInfo arg)
        {
            var one = Substitute.For <IDoctor>();
            one.LastName = "Miller";
            one.FirstName = "Mary";

            var two = Substitute.For <IDoctor>();
            two.LastName = "Miller";
            two.FirstName = "Jane";

            var list = new[]
                       {
                           one,
                           two
                       };

            return list;
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