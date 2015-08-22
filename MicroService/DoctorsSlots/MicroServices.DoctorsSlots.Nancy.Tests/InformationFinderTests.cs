using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;
using NSubstitute;
using NSubstitute.Core;
using Xunit;
using Xunit.Extensions;

namespace MicroServices.DoctorsSlots.Nancy.Tests
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public sealed class InformationFinderTests
    {
        private const int DoesNotMatter = int.MinValue;

        [Theory]
        [InlineData("Unknown", 1)]
        [InlineData("Open", 1)]
        [InlineData("Booked", 1)]
        public void List_ReturnsCorrectNumberOfSlots_ForStatusOpen(string slotStatus,
                                                                   int expected)
        {
            // Arrange
            var repository = Substitute.For <IDoctorsSlotsRepository>();
            repository.FindSlotsForDoctorByDoctorId(Arg.Any <int>()).Returns(CreateList);
            InformationFinder sut = CreateSut(repository);

            // Act
            IEnumerable <ISlot> actual = sut.List(DoesNotMatter,
                                                  null,
                                                  slotStatus);

            // Assert
            Assert.Equal(expected,
                         actual.Count());
        }

        [Theory]
        [InlineData("Unknown", 3)]
        [InlineData("Open", 2)]
        [InlineData("Booked", 1)]
        public void List_ReturnsSlots_ForStatusOpen(string slotStatus,
                                                    int expected)
        {
            // Arrange
            var repository = Substitute.For <IDoctorsSlotsRepository>();
            repository.FindSlotsForDoctorByDoctorId(Arg.Any <int>()).Returns(CreateList);
            InformationFinder sut = CreateSut(repository);

            // Act
            ISlot actual = sut.List(DoesNotMatter,
                                    null,
                                    slotStatus).First();

            // Assert
            Assert.Equal(expected,
                         actual.Id);
        }

        [Theory]
        [InlineData("2001-01-01", 1)]
        [InlineData("2001-02-01", 1)]
        [InlineData("2001-03-01", 1)]
        public void List_ReturnsCorrectNumberOfSlots_ForDate(string date,
                                                             int expected)
        {
            // Arrange
            var repository = Substitute.For <IDoctorsSlotsRepository>();
            repository.FindSlotsForDoctorByDoctorId(Arg.Any <int>()).Returns(CreateList);
            InformationFinder sut = CreateSut(repository);

            // Act
            IEnumerable <ISlot> actual = sut.List(DoesNotMatter,
                                                  date,
                                                  null);

            // Assert
            Assert.Equal(expected,
                         actual.Count());
        }

        [Theory]
        [InlineData("2001-01-01", 1)]
        [InlineData("2001-02-01", 2)]
        [InlineData("2001-03-01", 3)]
        public void List_ReturnsSlots_ForDate(string date,
                                              int expected)
        {
            // Arrange
            var repository = Substitute.For <IDoctorsSlotsRepository>();
            repository.FindSlotsForDoctorByDoctorId(Arg.Any <int>()).Returns(CreateList);
            InformationFinder sut = CreateSut(repository);

            // Act
            ISlot actual = sut.List(DoesNotMatter,
                                    date,
                                    null).First();

            // Assert
            Assert.Equal(expected,
                         actual.Id);
        }

        [Theory]
        //        [InlineData("2001-03-01", "Unknown", 1)]
        [InlineData("2001-02-01", "Open", 2)]
        //        [InlineData("2001-01-01", "Booked", 3)]
        public void List_ReturnsSlots_ForDateAndStatus(string date,
                                                       string status,
                                                       int expected)
        {
            // Arrange
            var repository = Substitute.For <IDoctorsSlotsRepository>();
            repository.FindSlotsForDoctorByDoctorId(Arg.Any <int>()).Returns(CreateList);
            InformationFinder sut = CreateSut(repository);

            // Act
            ISlot actual = sut.List(DoesNotMatter,
                                    date,
                                    status).First();

            // Assert
            Assert.Equal(expected,
                         actual.Id);
        }

        [Theory]
        [InlineData(SlotStatus.Unknown, 3)]
        [InlineData(SlotStatus.Open, 3)]
        [InlineData(SlotStatus.Booked, 3)]
        public void List_ReturnsSlots_ForMultipleForStatus(SlotStatus status,
                                                           int expected)
        {
            // Arrange
            IEnumerable <ISlot> list = CreateListWithSameStatus(status);
            var repository = Substitute.For <IDoctorsSlotsRepository>();
            repository.FindSlotsForDoctorByDoctorId(Arg.Any <int>()).Returns(list);
            InformationFinder sut = CreateSut(repository);

            // Act
            IEnumerable <ISlot> actual = sut.List(DoesNotMatter,
                                                  null,
                                                  status.ToString());

            // Assert
            Assert.Equal(expected,
                         actual.Count());
        }

        [Fact]
        public void List_ReturnsSlots_ForMultipleForDates()
        {
            // Arrange
            var date = "2001-01-01";
            IEnumerable <ISlot> list = CreateListWithSameDate(date);
            var repository = Substitute.For <IDoctorsSlotsRepository>();
            repository.FindSlotsForDoctorByDoctorId(Arg.Any <int>()).Returns(list);
            InformationFinder sut = CreateSut(repository);

            // Act
            IEnumerable <ISlot> actual = sut.List(DoesNotMatter,
                                                  date,
                                                  null);

            // Assert
            Assert.Equal(3,
                         actual.Count());
        }

        private IEnumerable <ISlot> CreateListWithSameDate(string date)
        {
            ISlot one = CreateSlot(3,
                                   SlotStatus.Unknown,
                                   date);
            ISlot two = CreateSlot(2,
                                   SlotStatus.Unknown,
                                   date);
            ISlot three = CreateSlot(1,
                                     SlotStatus.Unknown,
                                     date);

            var list = new Collection <ISlot>
                       {
                           one,
                           two,
                           three
                       };

            return list;
        }

        private IEnumerable <ISlot> CreateList(CallInfo arg)
        {
            return CreateList(SlotStatus.Unknown,
                              SlotStatus.Open,
                              SlotStatus.Booked);
        }

        private IEnumerable <ISlot> CreateListWithSameStatus(SlotStatus status)
        {
            return CreateList(status,
                              status,
                              status);
        }

        private static IEnumerable <ISlot> CreateList(SlotStatus statusOne,
                                                      SlotStatus statusTwo,
                                                      SlotStatus statusThree)
        {
            ISlot one = CreateSlot(3,
                                   statusOne,
                                   "2001-03-01");
            ISlot two = CreateSlot(2,
                                   statusTwo,
                                   "2001-02-01");
            ISlot three = CreateSlot(1,
                                     statusThree,
                                     "2001-01-01");

            var list = new Collection <ISlot>
                       {
                           one,
                           two,
                           three
                       };

            return list;
        }

        private static ISlot CreateSlot(int id,
                                        SlotStatus statusOne,
                                        string date)
        {
            var one = Substitute.For <ISlot>();
            one.Id.Returns(id);
            one.Status = statusOne;
            one.StartDateTime = DateTime.Parse(date);
            return one;
        }

        private InformationFinder CreateSut(IDoctorsSlotsRepository repository)
        {
            return new InformationFinder(repository);
        }
    }
}