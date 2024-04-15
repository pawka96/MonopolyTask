using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace MonopolyTask
{
    internal class Box : StrgObject
    {
        /*
         * Класс типа "Box", наследуемый от абстрактного класса,
         * описывающий коробки.
         */

        public DateOnly DateManufact {  get; private set; }     // Дата производства коробки

        /// <summary>
        /// Конструктор данного класса, имеющий 3 взодных параметра,
        /// 2 из которых являются необязательными.
        /// </summary>
        /// <param name="id"> ID коробки</param>
        /// <param name="expiry"> Срок годности </param>
        /// <param name="manufact"> Дата производства </param>
        public Box(int id, DateOnly expiry = default, DateOnly manufact = default)
        {

            ID = id;
            DateExpiry = expiry;
            DateManufact = manufact;

            // Генерация значений для геометрических данных
            Height = Math.Round(Rand.NextDouble() * 3 + 1, 1);
            Depth = Math.Round(Rand.NextDouble() * 3 + 1, 1);
            Width = Math.Round(Rand.NextDouble() * 3 + 1, 1);
            Weight = Math.Round(Rand.NextDouble() * 10 + 1, 1);
            Volume = Math.Round(Width * Height * Depth, 1);

            if (DateExpiry == default)
            {
                DateExpiry = DateManufact.AddDays(100);
            } 
        }

        /// <summary>
        /// Генерирует заданное количество коробок.
        /// </summary>
        /// <param name="count"> Заданное количество </param>
        /// <returns> Список коробок </returns>
        public static List<Box> GenBoxes(int count)
        {

            var boxes = new List<Box>();
            DateOnly dateRand = new DateOnly(2024, 4, Rand.Next(1, 6));    // Генерация даты

            /* В цикле половине списка генерируемых корбок присваивается
             * дате производства сгенерируемая дата,
             * а второй половине дате срока годности.
             */
            for (int i = 1; i <= count; i++)
            {
                if (i % 2 == 0)
                {
                    boxes.Add(new Box(i, manufact: dateRand));
                    dateRand = DateOnly.Parse($"{Rand.Next(1, 6)}.04.2024");

                }
                else
                {
                    boxes.Add(new Box(i, expiry: dateRand));
                    dateRand = DateOnly.Parse($"{Rand.Next(1, 6)}.04.2024");
                }
            }

            return boxes;
        }

        public override string ToString()
        {

            return $"\nКоробка (ID = {ID}) с характеристиками:" +
                   $"\nВысота: {Height}; Ширина: {Width}; Глубина: {Depth};" +
                   $"\nОбъем: {Volume}; Вес: {Weight}; " +
                   $"Дата производства: {DateManufact}; Срок годности до: {DateExpiry}.";
        }
    }
}
