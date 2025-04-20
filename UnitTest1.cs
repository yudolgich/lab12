using System.Numerics;
using CelestialLibrary;
using NUnit.Framework;

namespace TestProject12
{
    [TestFixture]
    public class PointTests
    {
        [Test]
        public void Point_CreateWithData_DataIsStored()
        {
            var planet = new Planet("Earth", 5.97, 6371, 1);
            var point = new Point<CelestialBody>(planet);

            Assert.That(point.Data, Is.EqualTo(planet));
            Assert.That(point.Next, Is.Null);
            Assert.That(point.Prev, Is.Null);
        }

        [Test]
        public void Point_LinkingNodes_UpdatesRelationsCorrectly()
        {
            var earth = new Planet("Earth", 5.97, 6371, 1);
            var mars = new Planet("Mars", 0.642, 3389, 2);

            var point1 = new Point<CelestialBody>(earth);
            var point2 = new Point<CelestialBody>(mars);

            point1.Next = point2;
            point2.Prev = point1;

            Assert.That(point1.Next.Data.Name, Is.EqualTo("Mars"));
            Assert.That(point2.Prev.Data.Name, Is.EqualTo("Earth"));
        }

        [Test]
        public void ToString_ReturnsCorrectRepresentation()
        {
            var star = new Star("Sun", 1989000, 696340, 5778);
            var point = new Point<CelestialBody>(star);

            Assert.That(point.ToString(), Does.Contain("Sun"));
            Assert.That(point.ToString(), Does.Contain("5778"));
        }
    }

    [TestFixture]
    public class DoublyLinkedListTests
    {
        private DoublyLinkedList<CelestialBody> _list;
        private Planet _earth;
        private Star _sun;

        [SetUp]
        public void Setup()
        {
            _list = new DoublyLinkedList<CelestialBody>();
            _earth = new Planet("Earth", 5.97, 6371, 1);
            _sun = new Star("Sun", 1989000, 696340, 5778);
        }

        // Добавление в пустой список
        [Test]
        public void AddToEnd_EmptyList_AddsAsFirstElement()
        {
            _list.AddToEnd(_earth);

            Assert.That(_list.Count, Is.EqualTo(1));
            Assert.That(_list.GetFirst().Data.Name, Is.EqualTo("Earth"));
        }

        // Добавление по индексу
        [Test]
        public void AddAt_ValidIndex_InsertsCorrectly()
        {
            _list.AddToEnd(_earth);
            _list.AddAt(1, _sun);

            Assert.That(_list.Count, Is.EqualTo(2));
            Assert.That(_list.GetFirst().Next.Data.Name, Is.EqualTo("Sun"));
        }

        // Удаление после заданного элемента
        [Test]
        public void RemoveAllAfter_ExistingName_RemovesTail()
        {
            _list.AddToEnd(_earth);
            _list.AddToEnd(_sun);
            _list.AddToEnd(new Planet("Mars", 0.642, 3389, 2));

            _list.RemoveAllAfter("Sun");

            Assert.That(_list.Count, Is.EqualTo(2));
            Assert.That(_list.GetLast().Data.Name, Is.EqualTo("Sun"));
        }

        // Глубокое клонирование
        [Test]
        public void DeepClone_CreatesIndependentCopy()
        {
            _list.AddToEnd(_earth);
            var clone = _list.DeepClone();

            clone.AddToEnd(_sun);

            Assert.That(_list.Count, Is.EqualTo(1));
            Assert.That(clone.Count, Is.EqualTo(2));
        }

        // Обработка неверного индекса
        [Test]
        public void AddAt_InvalidIndex_ThrowsException()
        {
            Assert.Throws<IndexOutOfRangeException>(() => _list.AddAt(100, _earth));
        }

        [Test]
        public void RemoveAllAfter_EmptyList_ThrowsException()
        {
            Assert.Throws<Exception>(() => _list.RemoveAllAfter("Earth"));
        }

        // Добавление в начало непустого списка
        [Test]
        public void AddAt_ZeroIndex_NonEmptyList_UpdatesLinks()
        {
            _list.AddToEnd(_sun);
            _list.AddAt(0, _earth);

            Assert.That(_list.GetFirst().Data.Name, Is.EqualTo("Earth"));
            Assert.That(_list.GetFirst().Next.Data.Name, Is.EqualTo("Sun"));
            Assert.That(_list.GetLast().Prev.Data.Name, Is.EqualTo("Earth"));
        }

        // Проверка очистки списка
        [Test]
        public void Clear_NonEmptyList_ResetsToInitialState()
        {
            _list.AddToEnd(_earth);
            _list.AddToEnd(_sun);
            _list.Clear();

            Assert.That(_list.Count, Is.EqualTo(0));
            Assert.That(_list.GetFirst(), Is.Null);
            Assert.That(_list.GetLast(), Is.Null);
        }

        // Итерация по пустому списку
        [Test]
        public void GetEnumerator_EmptyList_ReturnsEmptySequence()
        {
            var items = new List<CelestialBody>();
            foreach (var item in _list)
            {
                items.Add(item);
            }

            Assert.That(items, Is.Empty);
        }

        // Удаление после последнего элемента
        [Test]
        public void RemoveAllAfter_LastElement_DoesNothing()
        {
            _list.AddToEnd(_earth);
            _list.AddToEnd(_sun);

            _list.RemoveAllAfter("Sun");

            Assert.That(_list.Count, Is.EqualTo(2));
        }
    }

    [TestFixture]
    public class CelestialBodyTests
    {
        [Test]
        public void Clone_CreatesDeepCopy()
        {
            var original = new Planet("Earth", 5.97, 6371, 1);
            var clone = (Planet)original.Clone();

            clone.Name = "Mars";

            Assert.That(original.Name, Is.EqualTo("Earth"));
            Assert.That(clone.Name, Is.EqualTo("Mars"));
        }

        // Новый тест 1: Проверка сравнения объектов
        [Test]
        public void Equals_DifferentObjects_ReturnsCorrectResults()
        {
            var body1 = new Planet("Earth", 5.97, 6371, 1);
            var body2 = new Planet("Earth", 5.97, 6371, 1);
            var body3 = new Planet("Mars", 0.642, 3389, 2);

            Assert.That(body1.Equals(body2), Is.True);
            Assert.That(body1.Equals(body3), Is.False);
            Assert.That(body1.Equals(null), Is.False);
        }

        // Проверка поверхностного копирования
        [Test]
        public void ShallowCopy_CreatesShallowClone()
        {
            var original = new Planet("Earth", 5.97, 6371, 1);
            var clone = (Planet)original.ShallowCopy();

            clone.Name = "Mars";
            clone.Id.Number = 100;

            Assert.That(original.Name, Is.EqualTo("Earth"));
            Assert.That(clone.Name, Is.EqualTo("Mars"));
            Assert.That(original.Id.Number, Is.EqualTo(100)); // Id ссылается на тот же объект
        }

        // Проверка инициализации
        [Test]
        public void RandomInit_CreatesValidObject()
        {
            var body = new Planet();
            body.RandomInit();

            Assert.That(body.Name, Is.Not.Null.Or.Empty);
            Assert.That(body.Mass, Is.GreaterThan(0));
            Assert.That(body.Radius, Is.GreaterThan(0));
        }
    }

    [TestFixture]
    public class StarTests
    {
        // Проверка инициализации температуры
        [Test]
        public void Temperature_AfterInit_HasValidValue()
        {
            var star = new Star("Sun", 1989000, 696340, 5778);
            Assert.That(star.Temperature, Is.InRange(2000, 1000000));
        }

        // Проверка вывода информации
        [Test]
        public void Show_DisplaysTemperature()
        {
            var star = new Star("Sun", 1989000, 696340, 5778);
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                star.Show();
                var output = sw.ToString();

                Assert.That(output, Does.Contain("Sun"));
                Assert.That(output, Does.Contain("5778"));
            }
        }
    }

    [TestFixture]
    public class PlanetTests
    {
        // Проверка количества спутников
        [Test]
        public void NumSatellites_NegativeValue_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                new Planet("Earth", 5.97, 6371, -1));
        }

        // Проверка вывода информации
        [Test]
        public void Show_DisplaysSatellitesCount()
        {
            var planet = new Planet("Earth", 5.97, 6371, 1);
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                planet.Show();
                var output = sw.ToString();

                Assert.That(output, Does.Contain("Earth"));
                Assert.That(output, Does.Contain("1"));
            }
        }
    }
}