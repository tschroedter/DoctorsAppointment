﻿using System.Collections.Generic;
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
    public sealed class SlotsRepositoryTests
    {
        [Fact]
        public void All_ReturnsSlots_WhenCalled()
        {
            // Arrange
            var context = Substitute.For <ISlotsContext>();
            context.Slots().Returns(CreateSlots);
            SlotsRepository sut = CreateSut(context);

            // Act
            IQueryable <ISlot> actual = sut.All;

            // Assert
            Assert.Equal(2,
                         actual.Count());
        }

        [Fact]
        public void FindByDayId_ReturnsSlots_ForGivenDayId()
        {
            // Arrange
            var context = Substitute.For <ISlotsContext>();
            context.Slots().Returns(CreateSlots);
            SlotsRepository sut = CreateSut(context);

            // Act
            IEnumerable <ISlot> actual = sut.FindByDayId(1);

            // Assert
            Assert.Equal(1,
                         actual.Count());
        }

        private IQueryable <ISlot> CreateSlots([NotNull] CallInfo arg)
        {
            var one = Substitute.For <ISlot>();
            one.DayId.Returns(1);

            var two = Substitute.For <ISlot>();
            two.DayId.Returns(2);

            var list = new[]
                       {
                           one,
                           two
                       };

            return list.AsQueryable();
        }

        private SlotsRepository CreateSut([NotNull] ISlotsContext context)
        {
            return new SlotsRepository(context);
        }
    }
}