using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CelestialLibrary
{
    public interface IInit
    {
        void Init(); // Инициализация с клавиатуры
        void RandomInit(); // Инициализация случайными значениями
        void Show();
    }
}
