using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using MicroServices.Common.Tests;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;
using MicroServices.Days.Nancy.Interfaces;
using NSubstitute;
using NSubstitute.Core;
using Xunit;
using Xunit.Extensions;

namespace MicroServices.Days.Nancy.Tests
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public sealed class InformationFinderTests
    {
        private const string DefaultDate = "2000-01-01";
        private const string DefaultDoctorId = "1";

        [Theory]
        [AutoNSubstituteData]
        public void FindById_ReturnsDay_FindByDoctorId([NotNull] IDay day)
        {
            // Arrange
            var repository = Substitute.For <IDaysRepository>();
            repository.FindByDoctorId(1).Returns(CreateList);
            InformationFinder sut = CreateSut(repository);

            // Act
            IEnumerable <IDayForResponse> actual = sut.FindByDoctorId(1);

            // Assert
            Assert.Equal(2,
                         actual.Count());
        }

        [Theory]
        [AutoNSubstituteData]
        public void FindById_ReturnsDay_ForExistingId([NotNull] IDay day)
        {
            // Arrange
            var repository = Substitute.For <IDaysRepository>();
            repository.FindById(day.Id).Returns(day);
            InformationFinder sut = CreateSut(repository);

            // Act
            IDayForResponse actual = sut.FindById(day.Id);

            // Assert
            Assert.Equal(day.Id,
                         actual.Id);
        }

        [Fact]
        public void FilterByDate_ReturnsDays_ForExistingDate()
        {
            // Arrange
            IEnumerable <IDay> days = CreateDaysWithMatchingDateTime(DefaultDate,
                                                                     DefaultDoctorId);
            var repository = Substitute.For <IDaysRepository>();
            repository.All.Returns(days);

            InformationFinder sut = CreateSut(repository);

            // Act
            IEnumerable <IDayForResponse> actual =
                sut.ListForDateAndDoctorId(DefaultDate,
                                           DefaultDoctorId);

            // Assert
            Assert.Equal(2,
                         actual.Count());
        }

        [Fact]
        public void FilterByDate_ReturnsEmpty_ForNotExistingDate()
        {
            // Arrange
            IEnumerable <IDay> days = CreateDaysWithMatchingDateTime(DefaultDate,
                                                                     DefaultDoctorId);
            var repository = Substitute.For <IDaysRepository>();
            repository.All.Returns(days);

            InformationFinder sut = CreateSut(repository);

            // Act
            IEnumerable <IDayForResponse> actual =
                sut.ListForDateAndDoctorId("1999-01-01",
                                           DefaultDoctorId);

            // Assert
            Assert.Equal(0,
                         actual.Count());
        }

        [Fact]
        public void FilterByDoctorId_ReturnsDays_ForExistingDoctorId()
        {
            // Arrange
            IEnumerable <IDay> days = CreateDaysWithMatchingDateTime(DefaultDate,
                                                                     DefaultDoctorId);
            var repository = Substitute.For <IDaysRepository>();
            repository.All.Returns(days);

            InformationFinder sut = CreateSut(repository);

            // Act
            IEnumerable <IDayForResponse> actual =
                sut.ListForDateAndDoctorId(DefaultDate,
                                           DefaultDoctorId);

            // Assert
            Assert.Equal(2,
                         actual.Count());
        }

        [Fact]
        public void FilterByDoctorId_ReturnsEmpty_ForNotExistingDoctorId()
        {
            // Arrange
            IEnumerable <IDay> days = CreateDaysWithMatchingDateTime(DefaultDate,
                                                                     DefaultDoctorId);
            var repository = Substitute.For <IDaysRepository>();
            repository.All.Returns(days);

            InformationFinder sut = CreateSut(repository);

            // Act
            IEnumerable <IDayForResponse> actual =
                sut.ListForDateAndDoctorId(DefaultDate,
                                           "-1");

            // Assert
            Assert.Equal(0,
                         actual.Count());
        }

        [Fact]
        public void FindById_ReturnsNull_ForNotExistingId()
        {
            // Arrange
            var repository = Substitute.For <IDaysRepository>();
            repository.FindById(-1).Returns(( IDay ) null);
            InformationFinder sut = CreateSut(repository);

            // Act
            IDayForResponse actual = sut.FindById(-1);

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public void List_ReturnsDays_WhenCalled()
        {
            // Arrange
            var repository = Substitute.For <IDaysRepository>();
            repository.All.Returns(CreateList);
            InformationFinder sut = CreateSut(repository);

            // Act
            IEnumerable <IDayForResponse> actual = sut.List();

            // Assert
            Assert.Equal(2,
                         actual.Count());
        }

        [Fact]
        public void ListForDate_ReturnsDays_WhenCalled()
        {
            // Arrange
            var repository = Substitute.For <IDaysRepository>();
            repository.FindByDate(Arg.Any <DateTime>()).Returns(CreateList);
            InformationFinder sut = CreateSut(repository);

            // Act
            IEnumerable <IDayForResponse> actual = sut.ListForDate("2000-01-01");

            // Assert
            Assert.Equal(2,
                         actual.Count());
        }

        [Fact]
        public void ListForDate_ReturnsEmpty_ForInvalidDateTime()
        {
            // Arrange
            var repository = Substitute.For <IDaysRepository>();
            InformationFinder sut = CreateSut(repository);

            // Act
            IEnumerable <IDayForResponse> actual = sut.ListForDate("invalid");

            // Assert
            Assert.Equal(0,
                         actual.Count());
        }

        [Fact]
        public void ListForDateAndDoctorId_ReturnsDays_WhenCalled()
        {
            // Arrange
            var repository = Substitute.For <IDaysRepository>();
            repository.All.Returns(CreateDaysForListForDateAndDoctorId);
            InformationFinder sut = CreateSut(repository);

            // Act
            IEnumerable <IDayForResponse> actual =
                sut.ListForDateAndDoctorId("2000-01-01",
                                           DefaultDoctorId);

            // Assert
            Assert.Equal(1,
                         actual.Count());
        }

        private IQueryable <IDay> CreateList(CallInfo arg)
        {
            var list = new Collection <IDay>
                       {
                           Substitute.For <IDay>(),
                           Substitute.For <IDay>()
                       };


            return list.AsQueryable();
        }

        private IQueryable <IDay> CreateDaysWithMatchingDateTime(string dateTime,
                                                                 string doctorId)
        {
            var one = Substitute.For <IDay>();
            one.Date = DateTime.Parse(dateTime);
            one.DoctorId = int.Parse(doctorId);

            var two = Substitute.For <IDay>();
            two.Date = DateTime.Parse(dateTime);
            two.DoctorId = int.Parse(doctorId);

            var days = new[]
                       {
                           one,
                           two
                       };

            return days.AsQueryable();
        }

        private IQueryable <IDay> CreateDaysForListForDateAndDoctorId(CallInfo arg)
        {
            var one = Substitute.For <IDay>();
            one.Date = DateTime.Parse("2000-01-01");
            one.DoctorId = 1;

            var two = Substitute.For <IDay>();
            two.Date = DateTime.Parse("2000-01-01");
            two.DoctorId = 2;

            var three = Substitute.For <IDay>();
            three.Date = DateTime.Parse("2000-01-02");
            three.DoctorId = 1;

            var days = new[]
                       {
                           one,
                           two
                       };

            return days.AsQueryable();
        }

        private InformationFinder CreateSut(IDaysRepository repository)
        {
            return new InformationFinder(repository);
        }
    }
}