using System;

namespace CelestialLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            // Создаем хеш-таблицу
            HashTable<CelestialBody> hashTable = new HashTable<CelestialBody>(5);

            while (true)
            {
                Console.WriteLine("\n--- МЕНЮ ---");
                Console.WriteLine("1. Добавить элемент");
                Console.WriteLine("2. Найти элемент");
                Console.WriteLine("3. Удалить элемент");
                Console.WriteLine("4. Показать таблицу");
                Console.WriteLine("5. Выход");
                Console.Write("Выбор: ");

                string choice = SafeReadLine();

                switch (choice)
                {
                    case "1":
                        AddElementMenu(hashTable);
                        break;

                    case "2":
                        SearchElement(hashTable);
                        break;

                    case "3":
                        RemoveElement(hashTable);
                        break;

                    case "4":
                        hashTable.Print();
                        break;

                    case "5":
                        return;

                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }

        static void AddElementMenu(HashTable<CelestialBody> hashTable)
        {
            Console.WriteLine("\nВыберите тип элемента:");
            Console.WriteLine("1. Планета");
            Console.WriteLine("2. Звезда");
            Console.WriteLine("3. Газовый гигант");
            Console.Write("Выбор: ");

            string typeChoice = SafeReadLine();
            if (string.IsNullOrEmpty(typeChoice)) return;

            try
            {
                // Сначала запрашиваем имя
                Console.Write("Введите название: ");
                string name = SafeReadLine();
                if (string.IsNullOrEmpty(name)) return;

                // Проверяем уникальность имени
                if (ContainsName(hashTable, name))
                {
                    Console.WriteLine($"Элемент с именем '{name}' уже существует!");
                    return;
                }

                // Создаем объект соответствующего типа
                CelestialBody newBody = typeChoice switch
                {
                    "1" => new Planet(),
                    "2" => new Star(),
                    "3" => new GasGiant(),
                    _ => throw new ArgumentException("Неверный тип элемента")
                };

                // Устанавливаем имя
                newBody.Name = name;
                if (newBody is Planet planet)
                {
                    planet.Init(name);
                }
                else
                {
                    newBody.Init(name);
                }

                if (hashTable.Add(newBody))
                {
                    Console.WriteLine("Элемент добавлен.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void SearchElement(HashTable<CelestialBody> hashTable)
        {
            Console.Write("\nВведите название элемента для поиска: ");
            string name = SafeReadLine();

            var found = hashTable.FindByName(name);
            if (found != null)
            {
                Console.WriteLine("Элемент найден:");
                found.Show();
            }
            else
            {
                Console.WriteLine("Элемент не найден.");
            }
        }

        static void RemoveElement(HashTable<CelestialBody> hashTable)
        {
            Console.Write("\nВведите название элемента для удаления: ");
            string name = SafeReadLine();

            var found = hashTable.FindByName(name);
            if (found != null && hashTable.Remove(found))
            {
                Console.WriteLine("Элемент удалён.");
            }
            else
            {
                Console.WriteLine("Элемент не найден.");
            }
        }

        static bool ContainsName(HashTable<CelestialBody> hashTable, string name)
        {
            foreach (var item in hashTable) 
            {
                if (item != null && item.Name == name)
                    return true;
            }
            return false;
        }

        static string SafeReadLine()
        {
            try
            {
                return Console.ReadLine();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}