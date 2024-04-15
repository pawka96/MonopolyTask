namespace MonopolyTask
{
    abstract class StrgObject
    {
        /*
         *  Абстрактный класс для реализации объектов склада.
         */

        public int ID { get; protected set; }   // Уникальный идентификатор

        public double Width {  get; protected set; }    // Ширина

        public double Height { get; protected set; }    // Высота

        public double Depth { get; protected set; } // Глубина

        public double Volume { get; protected set; }    // Объем

        public double Weight { get; protected set; }    // Вес

        public DateOnly DateExpiry { get; protected set; }  // Срок годности

        protected static Random Rand = new Random();    // Экземпляр для генерации
    }
}
