//using CosmosLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CelestialLibrary
{
    public class Planet : CelestialBody
    {
        public CelestialBody BaseObject
        {
            get => new CelestialBody
            {
                Name = this.Name,
                Mass = this.Mass,
                Radius = this.Radius,
                Id = this.Id
            };
        }


        protected int numSatellites;

        public int NumSatellites
        {
            get => numSatellites;
            set
            {
                if (value < 0) throw new ArgumentException("Количество спутников не может быть отрицательным.");
                numSatellites = value;
            }
        }

        public Planet() : base() { }

        public Planet(string name, double mass, double radius, int numSatellites)
            : base(name, mass, radius)
        {
            NumSatellites = numSatellites;
        }

        public override void Show()
        {
            base.Show();
            Console.WriteLine($"Количество спутников: {NumSatellites}");
        }

        public override void Init()
        {
            base.Init();
            Console.Write("Введите количество спутников: ");
            NumSatellites = int.Parse(Console.ReadLine());
        }

        public override void RandomInit()
        {
            base.RandomInit();
            var rnd = new Random();
            NumSatellites = rnd.Next(0, 100);
        }

        public override bool Equals(object obj)
        {
            return obj is Planet other && base.Equals(other) && NumSatellites == other.NumSatellites;
        }

        public override string ToString()
        {
            return base.ToString() + $", Спутников: {NumSatellites}";
        }
    }
}
