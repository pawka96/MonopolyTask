using MonopolyTask;

class Program
{
    static void Main()
    {

        var PalletList = Pallet.GenPallets(10);     // Генерация списка из 10 паллет
        var BoxesList = Box.GenBoxes(100);      // Генерация списка из 100 коробок
        int boxCounter = 0;     // Переменная для учета ID коробок

        // С помощью цикла в каждую паллету кладется по 10 коробок
        foreach (var pallet in PalletList)
        {
            for (int i = 0; i < 10; i++)
            {
                pallet.AddBox(BoxesList[boxCounter++]);
            }
        }

        Pallet.GroupByExpiry(PalletList);
        Pallet.SortByMaxExpiry(PalletList);
        
    }
}