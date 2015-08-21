using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using MicroServices.Common.Tests;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;
using Xunit;
using Xunit.Extensions;

namespace MicroServices.Days.Nancy.Tests
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public sealed class DayForResponseTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void Constructor_SetsId_WhenCreated(
            [NotNull] IDay day)
        {
            // Arrange
            // Act
            var sut = new DayForResponse(day);

            // Assert
            Assert.Equal(day.Id,
                         sut.Id);
        }

        [Theory]
        [AutoNSubstituteData]
        public void Constructor_SetsDate_WhenCreated(
            [NotNull] IDay day)
        {
            // Arrange
            // Act
            var sut = new DayForResponse(day);

            // Assert
            Assert.Equal(day.Date,
                         sut.Date);
        }

        [Theory]
        [AutoNSubstituteData]
        public void Constructor_SetsDoctorId_WhenCreated(
            [NotNull] IDay day)
        {
            // Arrange
            // Act
            var sut = new DayForResponse(day);

            // Assert
            Assert.Equal(day.DoctorId,
                         sut.DoctorId);
        }
    }
}