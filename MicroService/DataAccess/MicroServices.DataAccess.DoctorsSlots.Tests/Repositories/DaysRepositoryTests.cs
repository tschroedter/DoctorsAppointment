using System;
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
    public sealed class DaysRepositoryTests
    {
        [Fact]
        public void FindByDoctorId_ReturnsDays_ForKnownId()
        {
            // Arrange
            var context = Substitute.For <IDaysContext>();
            context.Days().Returns(CreateDaysForFindByDoctorIdKnownId);
            DaysRepository sut = CreateSut(context);

            // Act
            IEnumerable <IDay> actual = sut.FindByDoctorId(1);

            // Assert
            Assert.Equal(1,
                         actual.Count());
        }

        [Fact]
        public void FindByDoctorId_ReturnsEmpty_ForUnknownId()
        {
            // Arrange
            var context = Substitute.For <IDaysContext>();
            context.Days().Returns(CreateDaysForFindByDoctorIdKnownId);
            DaysRepository sut = CreateSut(context);

            // Act
            IEnumerable <IDay> actual = sut.FindByDoctorId(-1);

            // Assert
            Assert.Equal(0,
                         actual.Count());
        }

        [Fact]
        public void FindByDate_ReturnsDays_ForKnownDay()
        {
            // Arrange
            var context = Substitute.For <IDaysContext>();
            context.Days().Returns(CreateDaysForFindByDoctorIdKnownId);
            DaysRepository sut = CreateSut(context);

            // Act
            IEnumerable <IDay> actual = sut.FindByDate(DateTime.Parse("2001-02-02"));

            // Assert
            Assert.Equal(2,
                         actual.Count());
        }

        [Fact]
        public void FindByDate_ReturnsEmpty_ForUnknownId()
        {
            // Arrange
            var context = Substitute.For <IDaysContext>();
            context.Days().Returns(CreateDaysForFindByDoctorIdKnownId);
            DaysRepository sut = CreateSut(context);

            // Act
            IEnumerable <IDay> actual = sut.FindByDate(DateTime.Parse("1999-01-01"));

            // Assert
            Assert.Equal(0,
                         actual.Count());
        }

        private IQueryable <IDay> CreateDaysForFindByDoctorIdKnownId(CallInfo arg)
        {
            IDay one = CreateDay(1,
                                 "2001-01-01",
                                 1);

            IDay two = CreateDay(2,
                                 "2001-02-02",
                                 2);

            IDay three = CreateDay(3,
                                   "2001-02-02",
                                   2);

            var list = new[]
                       {
                           one,
                           two,
                           three
                       };

            return list.AsQueryable();
        }

        private static IDay CreateDay(int id,
                                      [NotNull] string date,
                                      int doctorId)
        {
            var one = Substitute.For <IDay>();

            one.Id.Returns(id);
            one.Date = DateTime.Parse(date);
            one.DoctorId = doctorId;

            return one;
        }

        private DaysRepository CreateSut(IDaysContext context)
        {
            return new DaysRepository(context);
        }
    }
}