using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CelestialLibrary
{
    public class GasGiant : Planet
    {
        public bool HasRings { get; set; }

        public GasGiant() : base() { }

        public GasGiant(string name, double mass, double radius, int numSatellites, bool hasRings)
            : base(name, mass, radius, numSatellites)
        {
            HasRings = hasRings;
        }

        public override void Show()
        {
            base.Show();
            Console.WriteLine($"Наличие колец: {(HasRings ? "Да" : "Нет")}");
        }

        public override void Init()
        {
            base.Init();
            Console.Write("Есть ли кольца (true/false): ");
            HasRings = bool.Parse(Console.ReadLine());
        }

        public override void RandomInit()
        {
            base.RandomInit();
            var rnd = new Random();
            HasRings = rnd.Next(0, 2) == 1;
        }

        public override bool Equals(object obj)
        {
            return obj is GasGiant other && base.Equals(other) && HasRings == other.HasRings;
        }

        public override string ToString()
        {
            return base.ToString() + $", Кольца: {(HasRings ? "Да" : "Нет")}";
        }
    }
}
