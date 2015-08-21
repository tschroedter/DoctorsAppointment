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
            repository.FindById(-1).Returns(( ISlot ) null);
            InformationFinder sut = CreateSut(repository);

            // Act
            ISlotForResponse actual = sut.FindById(-1);

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