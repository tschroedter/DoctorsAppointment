using System;
using System.Diagnostics.CodeAnalysis;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;
using NSubstitute;
using Xunit;

namespace MicroServices.DoctorsSlots.Nancy.Tests
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public sealed class SlotForResponseTests
    {
        [Fact]
        public void Status_IsSet_WhenCreated()
        {
            // Arrange
            // Act
            SlotForResponse sut = CreateSut();

            // Assert
            Assert.Equal(SlotStatus.Open,
                         sut.Status);
        }

        [Fact]
        public void StartDateTime_IsSet_WhenCreated()
        {
            // Arrange
            // Act
            SlotForResponse sut = CreateSut();

            // Assert
            Assert.Equal(DateTime.Parse("2015-01-02"),
                         sut.StartDateTime);
        }

        [Fact]
        public void EndDateTime_IsSet_WhenCreated()
        {
            // Arrange
            // Act
            SlotForResponse sut = CreateSut();

            // Assert
            Assert.Equal(DateTime.Parse("2015-01-03"),
                         sut.EndDateTime);
        }

        [Fact]
        public void DayId_IsSet_WhenCreated()
        {
            // Arrange
            // Act
            SlotForResponse sut = CreateSut();

            // Assert
            Assert.Equal(2,
                         sut.DayId);
        }

        [Fact]
        public void Id_IsSet_WhenCreated()
        {
            // Arrange
            // Act
            SlotForResponse sut = CreateSut();

            // Assert
            Assert.Equal(1,
                         sut.Id);
        }

        private SlotForResponse CreateSut()
        {
            var slot = Substitute.For <ISlot>();
            slot.Status = SlotStatus.Open;
            slot.StartDateTime = DateTime.Parse("2015-01-02");
            slot.EndDateTime = DateTime.Parse("2015-01-03");
            slot.DayId.Returns(2);
            slot.Id.Returns(1);

            var sut = new SlotForResponse(slot);

            return sut;
        }
    }
}