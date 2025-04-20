using System;
using System.Collections;
using System.Collections.Generic;

namespace CelestialLibrary
{
    public class DoublyLinkedList<T> : IEnumerable<T> where T : class, ICloneable
    {
        private Point<T> begin; // Начало списка
        private Point<T> end; // Конец списка
        public int Count { get; private set; }

        #region Test Helpers
        internal Point<T> GetFirst() => begin;
        internal Point<T> GetLast() => end;
        #endregion

        // Добавление в конец
        public void AddToEnd(T data)
        {
            Point<T> newPoint = new Point<T>(data);
            if (begin == null)
            {
                begin = newPoint;
                end = newPoint;
            }
            else
            {
                end.Next = newPoint;
                newPoint.Prev = end;
                end = newPoint;
            }
            Count++;
        }

        // Добавление по индексу
        public void AddAt(int index, T data)
        {
            if (index < 0 || index > Count)
                throw new IndexOutOfRangeException();

            if (index == 0) // В начало
            {
                Point<T> newPoint = new Point<T>(data);
                newPoint.Next = begin;
                if (begin != null)
                    begin.Prev = newPoint;
                else
                    end = newPoint;
                begin = newPoint;
            }
            else if (index == Count) // В конец
            {
                AddToEnd(data);
                return;
            }
            else // В середину
            {
                Point<T> current = begin;
                for (int i = 0; i < index - 1; i++)
                    current = current.Next;

                Point<T> newPoint = new Point<T>(data);
                newPoint.Next = current.Next;
                newPoint.Prev = current;
                current.Next.Prev = newPoint;
                current.Next = newPoint;
            }
            Count++;
        }

        // Удаление всех элементов после заданного имени
        public void RemoveAllAfter(string name)
        {
            Point<T> current = begin;
            while (current != null)
            {
                if (current.Data is CelestialBody body && body.Name == name)
                {
                    if (current.Next != null)
                    {
                        current.Next = null;
                        end = current;
                        Count = GetCount(); // Пересчет элементов
                    }
                    return;
                }
                current = current.Next;
            }
            throw new Exception($"Элемент с именем '{name}' не найден");
        }

        // Глубокое клонирование списка
        public DoublyLinkedList<T> DeepClone()
        {
            DoublyLinkedList<T> newList = new DoublyLinkedList<T>();
            Point<T> current = begin;
            while (current != null)
            {
                newList.AddToEnd((T)current.Data.Clone());
                current = current.Next;
            }
            return newList;
        }

        // Очистка списка
        public void Clear()
        {
            begin = null;
            end = null;
            Count = 0;
        }

        // Вывод списка
        public void ShowList()
        {
            Point<T> current = begin;
            while (current != null)
            {
                Console.WriteLine(current);
                current = current.Next;
            }
        }

        // Подсчет элементов (вспомогательный метод)
        private int GetCount()
        {
            int count = 0;
            Point<T> current = begin;
            while (current != null)
            {
                count++;
                current = current.Next;
            }
            return count;
        }

        // Реализация IEnumerable<T> для foreach
        public IEnumerator<T> GetEnumerator()
        {
            Point<T> current = begin;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}