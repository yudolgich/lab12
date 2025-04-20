using System;
using CelestialLibrary;

namespace lab12
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new DoublyLinkedList<CelestialBody>();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n=== Меню работы с двунаправленным списком ===");
                Console.WriteLine("1. Добавить элемент в список");
                Console.WriteLine("2. Добавить элемент по индексу");
                Console.WriteLine("3. Удалить все элементы после заданного имени");
                Console.WriteLine("4. Вывести список");
                Console.WriteLine("5. Клонировать список");
                Console.WriteLine("6. Очистить список");
                Console.WriteLine("7. Выход");
                Console.Write("Выберите действие: ");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Ошибка ввода!");
                    continue;
                }

                try
                {
                    switch (choice)
                    {
                        case 1: // Добавление в конец
                            Console.WriteLine("\nВыберите тип объекта:");
                            Console.WriteLine("1. Планета");
                            Console.WriteLine("2. Звезда");
                            Console.WriteLine("3. Газовый гигант");
                            Console.Write("Ваш выбор: ");
                            int type = int.Parse(Console.ReadLine());

                            CelestialBody body = CreateCelestialBody(type);
                            list.AddToEnd(body);
                            Console.WriteLine("Элемент добавлен!");
                            break;

                        case 2: // Добавление по индексу
                            Console.Write("\nВведите индекс для вставки: ");
                            int index = int.Parse(Console.ReadLine());

                            Console.WriteLine("\nВыберите тип объекта:");
                            Console.WriteLine("1. Планета");
                            Console.WriteLine("2. Звезда");
                            Console.WriteLine("3. Газовый гигант");
                            Console.Write("Ваш выбор: ");
                            type = int.Parse(Console.ReadLine());

                            body = CreateCelestialBody(type);
                            list.AddAt(index, body);
                            Console.WriteLine("Элемент добавлен!");
                            break;

                        case 3: // Удаление после имени
                            Console.Write("\nВведите имя элемента, после которого нужно удалить все: ");
                            string name = Console.ReadLine();
                            list.RemoveAllAfter(name);
                            Console.WriteLine("Элементы удалены!");
                            break;

                        case 4: // Вывод списка
                            Console.WriteLine("\nТекущий список:");
                            list.ShowList();
                            break;

                        case 5: // Клонирование
                            var clonedList = list.DeepClone();
                            Console.WriteLine("\nКлонированный список:");
                            clonedList.ShowList();
                            break;

                        case 6: // Очистка
                            list.Clear();
                            Console.WriteLine("\nСписок очищен!");
                            break;

                        case 7: // Выход
                            exit = true;
                            break;

                        default:
                            Console.WriteLine("Неверный выбор!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }

        // Создание небесного тела
        static CelestialBody CreateCelestialBody(int type)
        {
            Console.Write("Введите название: ");
            string name = Console.ReadLine();

            Console.Write("Введите массу: ");
            double mass = double.Parse(Console.ReadLine());

            Console.Write("Введите радиус: ");
            double radius = double.Parse(Console.ReadLine());

            switch (type)
            {
                case 1: // Планета
                    Console.Write("Введите количество спутников: ");
                    int satellites = int.Parse(Console.ReadLine());
                    return new Planet(name, mass, radius, satellites);

                case 2: // Звезда
                    Console.Write("Введите температуру: ");
                    double temp = double.Parse(Console.ReadLine());
                    return new Star(name, mass, radius, temp);

                case 3: // Газовый гигант
                    Console.Write("Введите количество спутников: ");
                    satellites = int.Parse(Console.ReadLine());
                    Console.Write("Есть ли кольца (true/false): ");
                    bool hasRings = bool.Parse(Console.ReadLine());
                    return new GasGiant(name, mass, radius, satellites, hasRings);

                default:
                    throw new ArgumentException("Неверный тип объекта");
            }
        }
    }
}
