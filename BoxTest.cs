using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace MonopolyTask
{

    [TestFixture]
    internal class BoxTest
    {
        /*
         *  Класс реализации тестов методов для класса "Box".
         */

        [Test] 
        public void TestConstructor()
        {

            // Arange
            int id = 1;
            DateOnly dateExpiry = new DateOnly(2024, 04, 5);
            DateOnly dateManufact = new DateOnly(2024, 04, 1);

            // Act
            Box box = new Box(id, dateExpiry, dateManufact);

            // Assert
            Assert.That(id, Is.EqualTo(box.ID));
            Assert.That(dateExpiry, Is.EqualTo(box.DateExpiry));
            Assert.That(dateManufact, Is.EqualTo(box.DateManufact));
            Assert.That(box.Weight, Is.InRange(1, 11));
            Assert.That(box.Height, Is.InRange(1, 4));
            Assert.That(box.Depth, Is.InRange(1, 4));
            Assert.That(box.Width, Is.InRange(1, 4));
            Assert.That(box.Volume, Is.EqualTo(Math.Round(box.Depth * box.Width * box.Height, 1)));
        }

        [Test]
        public void TestGenBoxes()
        {
            
            // Arrage 
            int countBox = 100;
            DateOnly dateStart= new DateOnly(2024, 4, 1);
            DateOnly dateEnd = new DateOnly(2024, 4, 5);

            // Act
            List<Box> boxes = Box.GenBoxes(countBox);

            // Assert
            Assert.That(countBox, Is.EqualTo(boxes.Count));
            Assert.That(countBox, Is.EqualTo(boxes.Select(box => box.ID).Distinct().Count()));

            foreach (var box in boxes)
            {
                if (box.ID % 2 != 0)
                {
                    Assert.That(box.DateExpiry, Is.InRange(dateStart, dateEnd));
                    Assert.That(box.DateManufact, Is.EqualTo(default(DateOnly)));
                }
                else
                {
                    Assert.That(box.DateExpiry, Is.InRange(dateStart.AddDays(100),dateEnd.AddDays(100)));
                    Assert.That(box.DateManufact, Is.InRange(dateStart, dateEnd));
                }
            }
        }
    }
}
