//using CosmosLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CelestialLibrary
{
    public class Star : CelestialBody
    {
        public double Temperature { get; set; }

        public Star() : base() { }

        public Star(string name, double mass, double radius, double temperature)
            : base(name, mass, radius)
        {
            Temperature = temperature;
        }

        public override void Show()
        {
            base.Show();
            Console.WriteLine($"Температура: {Temperature}");
        }

        public override void Init()
        {
            base.Init();
            Console.Write("Введите температуру: ");
            Temperature = double.Parse(Console.ReadLine());
        }

        public override void RandomInit()
        {
            base.RandomInit();
            var rnd = new Random();
            Temperature = rnd.Next(2000, 10000); // типичная температура звезды
        }

        public override bool Equals(object obj)
        {
            return obj is Star other && base.Equals(other) && Temperature == other.Temperature;
        }

        public override string ToString()
        {
            return base.ToString() + $", Температура: {Temperature}";
        }
    }
}
