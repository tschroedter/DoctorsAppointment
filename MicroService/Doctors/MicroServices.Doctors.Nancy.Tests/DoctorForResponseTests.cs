using System.Diagnostics.CodeAnalysis;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;
using NSubstitute;
using Xunit;

namespace MicroServices.Doctors.Nancy.Tests
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public sealed class DoctorForResponseTests
    {
        [Fact]
        public void LastName_IsSet_WhenCreated()
        {
            // Arrange
            // Act
            DoctorForResponse sut = CreateSut();

            // Assert
            Assert.Equal("Miller",
                         sut.LastName);
        }

        [Fact]
        public void FirstName_IsSet_WhenCreated()
        {
            // Arrange
            // Act
            DoctorForResponse sut = CreateSut();

            // Assert
            Assert.Equal("Marry",
                         sut.FirstName);
        }

        [Fact]
        public void Id_IsSet_WhenCreated()
        {
            // Arrange
            // Act
            DoctorForResponse sut = CreateSut();

            // Assert
            Assert.Equal(1,
                         sut.Id);
        }

        [Fact]
        public void DefaultConstructor_SetsId_WhenCreated()
        {
            // Arrange
            // Act
            var sut = new DoctorForResponse();

            // Assert
            Assert.Equal(-1,
                         sut.Id);
        }

        [Fact]
        public void DefaultConstructor_SetsFirstName_WhenCreated()
        {
            // Arrange
            // Act
            var sut = new DoctorForResponse();

            // Assert
            Assert.Equal(string.Empty,
                         sut.FirstName);
        }

        [Fact]
        public void DefaultConstructor_SetsLastName_WhenCreated()
        {
            // Arrange
            // Act
            var sut = new DoctorForResponse();

            // Assert
            Assert.Equal(string.Empty,
                         sut.LastName);
        }

        private DoctorForResponse CreateSut()
        {
            var doctor = Substitute.For <IDoctor>();
            doctor.Id.Returns(1);
            doctor.FirstName = "Marry";
            doctor.LastName = "Miller";

            var sut = new DoctorForResponse(doctor);

            return sut;
        }
    }
}