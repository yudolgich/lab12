namespace CelestialLibrary
{
    public class Point<T> where T : class, ICloneable
    {
        public T Data { get; set; }
        public Point<T> Next { get; set; }
        public Point<T> Prev { get; set; }

        public Point(T data)
        {
            Data = data;
            Next = null;
            Prev = null;
        }

        public override string ToString()
        {
            return Data.ToString();
        }
    }
}
