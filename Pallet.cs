using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace MonopolyTask
{
    internal class Pallet : StrgObject
    {
        /*
         * Класс типа "Pallet", наследуемый от абстрактного класса,
         * описывающий паллеты.
         */

        public List<Box> Boxes { get; private set; } = new List<Box>();  // Список коробок в паллете

        /// <summary>
        /// Конструктор, принимающий 1 параметр.
        /// </summary>
        /// <param name="id"> ID паллеты </param>
        public Pallet(int id)
        {
            
            ID = id;

            // Генерация значений для геометрических данных
            Height = Math.Round(Rand.NextDouble() * 3 + 1, 1);
            Depth = Math.Round(Rand.NextDouble() * 15 + 15, 1);
            Width = Math.Round(Rand.NextDouble() * 15 + 15, 1);
            Weight = 30;
            Volume = Math.Round(Width * Height * Depth, 1);
        }

        /// <summary>
        /// Статический метод для генерации списка паллет.
        /// </summary>
        /// <param name="count"> Входной параметр для количества генерации. </param>
        /// <returns> Список паллет </returns>
        public static List<Pallet> GenPallets(int count)
        {

            var pallets = new List<Pallet>();

            for (int i = 1; i <= count; i++)
            {
                pallets.Add(new Pallet(i));
            }

            return pallets;
        }

        /// <summary>
        /// Метод, реализующий добавление коробки в паллету.
        /// </summary>
        /// <param name="box"> Входной параметр в виде ссылки типа "Box"</param>
        public void AddBox(Box box)
        {

            Boxes.Add(box);
            Weight += box.Weight;
            Volume += box.Volume;
            DateExpiry = Boxes.OrderBy(box => box.DateExpiry).FirstOrDefault().DateExpiry;
        }

        /// <summary>
        /// Статический метод для группировки по сроку годности паллет, сортироровки
        /// по возрастанию срока годности и сортировки в каждой группе паллет по весу.
        /// </summary>
        /// <param name="pallets"> Входной параметр в виде ссылки на список паллет. </param>
        public static void GroupByExpiry(List<Pallet> pallets)
        {

            Console.WriteLine($"Группы паллетов, отсортированные по возрастанию срока годности," +
                                       $" в каждой группе отсортированные по весу.\n");

            var groupPalls = pallets.GroupBy(pallet => pallet.DateExpiry)
                                         .OrderBy(group => group.Key);

            foreach (var expiryGroup in groupPalls)
            {
                Console.WriteLine($"Группа для даты {expiryGroup.Key}:\n");

                foreach (var pallet in expiryGroup.OrderBy(p => p.Weight))
                {
                    Console.WriteLine(pallet);
                }
            }
        }

        /// <summary>
        /// Статический метод, который находит 3 палетты, содержащие
        /// коробки с самым большим сроком годности и сортирует их
        /// по возрастанию объема.
        /// </summary>
        /// <param name="pallets"> Входной параметр в виде ссылки на список паллет </param>
        public static void SortByMaxExpiry(List<Pallet> pallets)
        {

            Console.WriteLine("Вывод информации по 3 паллетам, " +
                              "содержащим коробки с наибольшим сроком годности, " +
                              "отсортированные по возрастанию объема:\n");

            var sorted = pallets.OrderByDescending(pallet => pallet.Boxes.Max(box => box.DateExpiry))
                                                                           .Take(3)
                                                                           .OrderBy(pallet => pallet.Volume);
                                                                 
            foreach (var pallet in sorted)
            {
                
                Console.WriteLine($"Срок годности до: " +
                                  $"{string.Join("\n", pallet.Boxes.Max(b => b.DateExpiry))}" +
                                  $"\n{pallet}\n");
            }
        }

        public override string ToString()
        {

            return $"Паллета (ID = {ID}) с характеристиками:" +
                   $"\nВысота: {Height}; Ширина: {Width}; Глубина: {Depth};" +
                   $"\nОбъем: {Volume}; Вес: {Weight}; Срок годности до: {DateExpiry}." +
                   $"\nСостав коробок для паллеты (ID = {ID}): \n{string.Join("\n", Boxes)}\n";
        }

    }
}
