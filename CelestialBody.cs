using CelestialLibrary;

namespace CelestialLibrary
{
    public class CelestialBody : IInit, IComparable, ICloneable
    {
        protected string name;
        protected double mass;
        protected double radius;

        public string Name
        {
            get => name;
            set => name = value ?? "Unknown";
        }

        public double Mass
        {
            get => mass;
            set
            {
                if (value <= 0) throw new ArgumentException("Масса должна быть положительным числом");
                mass = value;
            }
        }

        public double Radius
        {
            get => radius;
            set
            {
                if (value <= 0) throw new ArgumentException("Радиус должен быть положительным числом");
                radius = value;
            }
        }

        public IdNumber Id { get; set; } // Новое ссылочное поле

        public CelestialBody() {
            Id = new IdNumber(); // Чтобы всегда был объект, даже если не задан
        }

        public CelestialBody(string name, double mass, double radius)
        {
            Name = name;
            Mass = mass;
            Radius = radius;
            Id = new IdNumber(); // Добавить сюда
        }

        public virtual void Show()
        {
            Console.WriteLine($"Название: {Name}, Масса: {Mass}, Радиус: {Radius}, {Id}");
        }

        // НЕвиртуальный метод — будет использоваться для сравнения с виртуальным Show()
        public void ShowNonVirtual()
        {
            Console.WriteLine($"Небесное тело: {Name}, Масса: {Mass}, Радиус: {Radius}");
        }


        public virtual void Init()
        {
            Console.Write("Введите название: ");
            Name = Console.ReadLine();
            Console.Write("Введите массу: ");
            Mass = double.Parse(Console.ReadLine());
            Console.Write("Введите радиус: ");
            Radius = double.Parse(Console.ReadLine());
        }

        public virtual void RandomInit()
        {
            var rnd = new Random();
            Name = "Object_" + rnd.Next(1000);
            Mass = rnd.NextDouble() * 100;
            Radius = rnd.NextDouble() * 10;
        }

        public override bool Equals(object obj)
        {
            if (obj is CelestialBody other)
                return Name == other.Name && Mass == other.Mass && Radius == other.Radius;
            return false;
        }

        // Реализация CompareTo для сортировки
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            CelestialBody otherBody = obj as CelestialBody;
            if (otherBody != null)
            {
                return this.Mass.CompareTo(otherBody.Mass);
            }
            else
            {
                throw new ArgumentException("Не является объектом CelestialBody");
            }
        }

        public class IdNumber
        {
            private int number;

            public int Number
            {
                get { return number; }
                set
                {
                    if (value < 0)
                        throw new ArgumentException("ID не может быть меньше 0");
                    number = value;
                }
            }

            public IdNumber()  // Конструктор по умолчанию
            {
                Number = 0;
            }

            public IdNumber(int number)  // Конструктор с параметром
            {
                Number = number;
            }

            public override bool Equals(object obj)
            {
                if (obj is IdNumber other)
                    return this.Number == other.Number;
                return false;
            }

            public override string ToString()
            {
                return $"ID: {Number}";
            }

            


        }

        public virtual object Clone()
        {
            CelestialBody clone = (CelestialBody)this.MemberwiseClone(); // поверхностная копия
            clone.Id = new IdNumber(this.Id.Number); // вручную копируем вложенный объект (глубокая копия)
            return clone;
        }

        public CelestialBody ShallowCopy()
        {
            return (CelestialBody)this.MemberwiseClone(); // всё копируется как ссылки
        }

        public override string ToString()
        {
            return $"Название: {Name}, Масса: {Mass}, Радиус: {Radius}";
        }
    }
}
