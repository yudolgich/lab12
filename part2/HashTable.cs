using System;
using System.Collections;
using System.Collections.Generic;

namespace CelestialLibrary
{
    public class HashTable<T> : IEnumerable<T> where T : IInit, ICloneable, new()
    {
        private LinkedList<T>[] table;
        public int Size { get; private set; }
        public int Count { get; private set; }

        public HashTable(int size = 10)
        {
            Size = size;
            table = new LinkedList<T>[Size];
            Count = 0;
        }

        public bool Add(T item)
        {
            if (item == null) return false;

            int index = GetHash(item);

            if (table[index] == null)
            {
                table[index] = new LinkedList<T>();
            }

            // Проверка на дубликаты
            foreach (var existingItem in table[index])
            {
                if (existingItem.Equals(item))
                    return false;
            }

            table[index].AddLast((T)item.Clone());
            Count++;
            return true;
        }

        public bool Contains(T item)
        {
            if (item == null) return false;

            int index = GetHash(item);

            if (table[index] == null) return false;

            foreach (var existingItem in table[index])
            {
                if (existingItem.Equals(item))
                    return true;
            }

            return false;
        }

        public bool ContainsName(string name)
        {
            var dummy = new T();
            if (dummy is CelestialBody cb)
            {
                cb.Name = name;
                return Contains((T)(object)cb);
            }
            return false;
        }

        public bool Remove(T item)
        {
            if (item == null) return false;

            int index = GetHash(item);

            if (table[index] == null) return false;

            var node = table[index].Find(item);
            if (node != null)
            {
                table[index].Remove(node);
                Count--;
                return true;
            }

            return false;
        }

        private int GetHash(T item)
        {
            return Math.Abs(item.GetHashCode()) % Size;
        }

        public void Print()
        {
            for (int i = 0; i < Size; i++)
            {
                Console.Write($"{i}: ");
                if (table[i] != null)
                {
                    foreach (var item in table[i])
                    {
                        item.Show();
                        Console.Write(" -> ");
                    }
                }
                Console.WriteLine("null");
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Size; i++)
            {
                if (table[i] != null)
                {
                    foreach (var item in table[i])
                    {
                        yield return item;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public T FindByName(string name)
        {
            foreach (var item in this)
            {
                if (item is CelestialBody cb && cb.Name == name)
                    return item;
            }
            return default;
        }

    }
}
