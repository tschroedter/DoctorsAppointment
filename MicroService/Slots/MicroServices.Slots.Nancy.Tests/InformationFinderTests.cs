using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using MicroServices.Common.Tests;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;
using MicroServices.Slots.Nancy.Interfaces;
using NSubstitute;
using NSubstitute.Core;
using Xunit;
using Xunit.Extensions;

namespace MicroServices.Slots.Nancy.Tests
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public sealed class InformationFinderTests
    {
        private const int IdDoesNotMatter = -1;

        [Theory]
        [AutoNSubstituteData]
        public void FindById_ReturnsSlot_ForExistingId([NotNull] ISlot slot)
        {
            // Arrange
            var repository = Substitute.For <ISlotsRepository>();
            repository.FindById(slot.Id).Returns(slot);
            InformationFinder sut = CreateSut(repository);

            // Act
            ISlotForResponse actual = sut.FindById(slot.Id);

            // Assert
            Assert.Equal(slot.Id,
                         actual.Id);
        }

        [Fact]
        public void FindById_ReturnsNull_ForNotExistingId()
        {
            // Arrange
            var repository = Substitute.For <ISlotsRepository>();
            repository.FindById(IdDoesNotMatter).Returns(( ISlot ) null);
            InformationFinder sut = CreateSut(repository);

            // Act
            ISlotForResponse actual = sut.FindById(IdDoesNotMatter);

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public void List_ReturnsSlots_WhenCalled()
        {
            // Arrange
            var repository = Substitute.For <ISlotsRepository>();
            repository.All.Returns(CreateList);
            InformationFinder sut = CreateSut(repository);

            // Act
            IEnumerable <ISlotForResponse> actual = sut.List();

            // Assert
            Assert.Equal(2,
                         actual.Count());
        }

        [Theory]
        [AutoNSubstituteData]
        public void Save_CallsSave_WhenCalled([NotNull] ISlotForResponse slot)
        {
            // Arrange
            var repository = Substitute.For <ISlotsRepository>();
            InformationFinder sut = CreateSut(repository);

            // Act
            sut.Save(slot);

            // Assert
            repository.Received().Save(Arg.Is <ISlot>(x => x.DayId == slot.DayId &&
                                                           x.StartDateTime == slot.StartDateTime &&
                                                           x.EndDateTime == slot.EndDateTime &&
                                                           x.Status == slot.Status));
        }


        [Theory]
        [AutoNSubstituteData]
        public void Save_ReturnsSlot_WhenCalled([NotNull] ISlotForResponse slot)
        {
            // Arrange
            var repository = Substitute.For <ISlotsRepository>();
            InformationFinder sut = CreateSut(repository);

            // Act
            ISlotForResponse actual = sut.Save(slot);

            // Assert
            AssertSlotIgnoreId(slot,
                               actual);
        }

        [Theory]
        [AutoNSubstituteData]
        public void Delete_ReturnsDeletedSlot_WhenCalled([NotNull] ISlot slot)
        {
            // Arrange
            var repository = Substitute.For <ISlotsRepository>();
            repository.FindById(Arg.Any <int>()).Returns(slot);
            InformationFinder sut = CreateSut(repository);

            // Act
            ISlotForResponse actual = sut.Delete(IdDoesNotMatter);

            // Assert
            Assert.Equal(slot.Id,
                         actual.Id);
        }

        [Theory]
        [AutoNSubstituteData]
        public void Delete_CallsRemove_WhenCalled([NotNull] ISlot slot)
        {
            // Arrange
            var repository = Substitute.For <ISlotsRepository>();
            repository.FindById(Arg.Any <int>()).Returns(slot);
            InformationFinder sut = CreateSut(repository);

            // Act
            sut.Delete(IdDoesNotMatter);

            // Assert
            repository.Received().Remove(slot);
        }

        [Fact]
        public void Delete_ReturnsNull_ForUnknownId()
        {
            // Arrange
            var repository = Substitute.For <ISlotsRepository>();
            repository.FindById(Arg.Any <int>()).Returns(( ISlot ) null);
            InformationFinder sut = CreateSut(repository);

            // Act
            ISlotForResponse actual = sut.Delete(IdDoesNotMatter);

            // Assert
            Assert.Null(actual);
        }

        private static void AssertSlotIgnoreId(ISlotForResponse expected,
                                               ISlotForResponse actual)
        {
            Console.WriteLine("Comparing days with id {0} and {1}...",
                              expected.Id,
                              actual.Id);

            Assert.True(expected.DayId == actual.DayId,
                        "DayId");
            Assert.True(expected.StartDateTime == actual.StartDateTime,
                        "StartDateTime");
            Assert.True(expected.EndDateTime == actual.EndDateTime,
                        "EndDateTime");
        }

        private IQueryable <ISlot> CreateList(CallInfo arg)
        {
            var list = new Collection <ISlot>
                       {
                           Substitute.For <ISlot>(),
                           Substitute.For <ISlot>()
                       };


            return list.AsQueryable();
        }

        private InformationFinder CreateSut(ISlotsRepository repository)
        {
            return new InformationFinder(repository);
        }
    }
}