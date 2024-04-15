using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace MonopolyTask
{

    [TestFixture]
    internal class PalletTest
    {
        /*
         *  Класс реализации тестов методов для класса "Pallet".
         */

        [Test]
        public void TestConstructor()
        {

            // Arrange
            int id = 1;
            double weight = 30;

            // Act
            Pallet pallet = new Pallet(id);

            // Assert
            Assert.That(id, Is.EqualTo(pallet.ID));
            Assert.That(weight, Is.EqualTo(pallet.Weight));
            Assert.That(pallet.Depth, Is.InRange(15, 30));
            Assert.That(pallet.Width, Is.InRange(15, 30));
            Assert.That(pallet.Height, Is.InRange(1, 4));
            Assert.That(pallet.Volume, Is.EqualTo(Math.Round(pallet.Depth * pallet.Width * pallet.Height, 1)));
        }

        [Test]
        public void TestGenPallets()
        {

            // Arrange

            int countPall = 10;

            // Act
            List<Pallet> pallets = Pallet.GenPallets(countPall); 

            // Assert
            Assert.That(countPall, Is.EqualTo(pallets.Count));
            Assert.That(countPall, Is.EqualTo(pallets.Select(pallet => pallet.ID).Distinct().Count()));
        }

        [Test]
        public void TestAddBox()
        {

            // Arrange
            double weight = 30;
            Pallet pallet = new Pallet(1);
            double volume = pallet.Volume;
            Box box = new Box(1);

            // Act
            pallet.AddBox(box);

            // Assert
            Assert.That(pallet.Boxes.Count, Is.EqualTo(1));
            Assert.That(pallet.Weight, Is.EqualTo(weight + box.Weight));
            Assert.That(pallet.Volume, Is.EqualTo(volume + box.Volume));
            Assert.That(pallet.DateExpiry, Is.EqualTo(box.DateExpiry));
        }

        [Test]
        public void TestGroupByExpiry()
        {

            // Arrange
            int count = 4;

            DateOnly firstDate = new DateOnly(2024,4,1);
            DateOnly secondDate = new DateOnly(2024, 4, 3);
            DateOnly thirdDate = new DateOnly(2024, 4, 5);

            Box box1 = new Box(1, expiry: thirdDate);
            Box box2 = new Box(2, expiry: firstDate);
            Box box3 = new Box(3, expiry: secondDate);
            Box box4 = new Box(4, expiry: secondDate);

            // Act
            var pallets = Pallet.GenPallets(count);

            pallets[0].AddBox(box1);
            pallets[1].AddBox(box2);
            pallets[2].AddBox(box3);
            pallets[3].AddBox(box4);

            Pallet.GroupByExpiry(pallets);

            var countGroups = pallets.GroupBy(pallet => pallet.DateExpiry)
                                      .Count();

            var expFirstDate = pallets.GroupBy(pallet => pallet.DateExpiry)
                                      .OrderBy(group => group.Key)
                                      .First()
                                      .Key;

            var expLastDate = pallets.GroupBy(pallet => pallet.DateExpiry)
                                      .OrderBy(group => group.Key)
                                      .Last()
                                      .Key;

            var secondGroup = pallets.GroupBy(pallet => pallet.DateExpiry)
                                      .OrderBy(group => group.Key)
                                      .Skip(1)
                                      .First()
                                      .OrderBy(group => group.Weight);

            //Assert
            Assert.That(3, Is.EqualTo(countGroups));
            Assert.That(firstDate, Is.EqualTo(expFirstDate));
            Assert.That(thirdDate, Is.EqualTo(expLastDate));
            Assert.That(secondGroup.Last().Weight, Is.GreaterThan(secondGroup.First().Weight));
        }

        [Test]
        public void TestSortByMaxExpiry()
        {

            // Arrange
            int count = 4;

            DateOnly firstDate = new DateOnly(2024, 4, 1);
            DateOnly secondDate = new DateOnly(2024, 4, 3);
            DateOnly thirdDate = new DateOnly(2024, 4, 5);
            DateOnly fourthDate = new DateOnly(2024, 4, 7);

            Box box1 = new Box(1, expiry: thirdDate);
            Box box2 = new Box(2, expiry: firstDate);
            Box box3 = new Box(3, expiry: secondDate);
            Box box4 = new Box(4, expiry: fourthDate);

            // Act
            var pallets = Pallet.GenPallets(count);

            pallets[0].AddBox(box1);
            pallets[1].AddBox(box2);
            pallets[2].AddBox(box3);
            pallets[3].AddBox(box4);

            Pallet.SortByMaxExpiry(pallets);

            var sorted = pallets.OrderByDescending(pallet => pallet.Boxes.Min(box => box.DateExpiry))
                                                                          .Take(3)
                                                                          .OrderBy(pallet => pallet.Volume);

            // Assert
            Assert.That(3, Is.EqualTo(sorted.Count()));

            foreach (var pallet in sorted)
            {
                var maxExpiryDate = pallet.Boxes.Max(box => box.DateExpiry);
                Assert.That(maxExpiryDate, Is.Not.EqualTo(firstDate));
            }

            Assert.That(sorted.Skip(1).First().Volume, Is.GreaterThan(sorted.First().Volume));
            Assert.That(sorted.Last().Volume, Is.GreaterThan(sorted.Skip(1).First().Volume));

        }
    }
}
