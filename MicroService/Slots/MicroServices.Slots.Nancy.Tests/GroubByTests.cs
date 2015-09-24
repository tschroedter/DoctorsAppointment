using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MicroServices.Slots.Nancy.Tests
{
    public sealed class GroubByTests
    {
        [Fact]
        public void GroupAndCount_ReturnsGroups_UnderCondition()
        {
            // Arrange
            Item[] items = CreateItems();

            // Act
            IEnumerable <dynamic> actual = GroupAndCount(items);

            // Assert
            Assert.Equal(3,
                         actual.Count());
        }

        [Fact]
        public void GroupAndCount_ReturnsCount_ForGroupWithIdOne()
        {
            // Arrange
            Item[] items = CreateItems();

            // Act
            dynamic actual = GroupAndCount(items).FirstOrDefault(x => x.Id == 1);

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(3,
                         actual.Count);
        }

        [Fact]
        public void GroupAndCount_ReturnsCount_ForGroupWithIdTwo()
        {
            // Arrange
            Item[] items = CreateItems();

            // Act
            dynamic actual = GroupAndCount(items).FirstOrDefault(x => x.Id == 2);

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(20,
                         actual.Count);
        }

        [Fact]
        public void GroupAndCount_ReturnsCount_ForGroupWithIdThree()
        {
            // Arrange
            Item[] items = CreateItems();

            // Act
            dynamic actual = GroupAndCount(items).FirstOrDefault(x => x.Id == 3);

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(30,
                         actual.Count);
        }

        private static IEnumerable <dynamic> GroupAndCount(Item[] items)
        {
            return from item in items
                   group item by item.Id
                   into groupedById
                   select new
                          {
                              Id = groupedById.Key,
                              Count = groupedById.Select(x => x.Quantity).Sum()
                          };
        }

        private Item[] CreateItems()
        {
            var oneOne = new Item
                         {
                             Id = 1,
                             Quantity = 1
                         };

            var oneTwo = new Item
                         {
                             Id = 1,
                             Quantity = 2
                         };

            var twoOne = new Item
                         {
                             Id = 2,
                             Quantity = 20
                         };

            var threeTwo = new Item
                           {
                               Id = 3,
                               Quantity = 30
                           };
            return new[]
                   {
                       oneOne,
                       oneTwo,
                       twoOne,
                       threeTwo
                   };
        }

        private class Item
        {
            public int Id { get; set; }
            public int Quantity { get; set; }
        }
    }
}