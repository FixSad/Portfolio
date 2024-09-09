using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLikeTest
{
    class Figure
    {
        public Pixel[,] pixels2 { get; set; }

        public void Draw()
        {
            foreach (Pixel pixel in pixels2)
            {
                pixel.Draw();
            }
        }

        public void Clear()
        {
            foreach (Pixel pixel in pixels2)
            {
                pixel.Clear();
            }
        }

    }

    class Room : Figure
    {
        private Random _random = new Random();
        private byte _chanseHeal;



        public Room(byte chanseHeal = 50)
        {
            pixels2 = new Pixel[_random.Next(10, 31), _random.Next(10, 31)];
            GenerateRoom();
            _chanseHeal = chanseHeal;
        }

        private void GenerateRoom()
        {
            for (int i = 0; i < pixels2.GetLength(0); i++)
            {
                for (int j = 0; j < pixels2.GetLength(1); j++)
                {
                    if (i == 0 && j == 0) // Левый верхний угол
                        pixels2[i, j] = new Pixel(j, i, '█', ConsoleColor.Gray);
                    else if (i == 0 && j == pixels2.GetLength(1) - 1) // Правый верхний угол
                        pixels2[i, j] = new Pixel(j, i, '█', ConsoleColor.Gray);
                    else if (i == pixels2.GetLength(0) - 1 && j == pixels2.GetLength(1) - 1) // Правый нижний угол
                        pixels2[i, j] = new Pixel(j, i, '█', ConsoleColor.Gray);
                    else if (i == 0) // Верхняя сторона
                        pixels2[i, j] = new Pixel(j, i, '▀', ConsoleColor.Gray);
                    else if (j == 0) // Левая сторона
                        pixels2[i, j] = new Pixel(j, i, '█', ConsoleColor.Gray);
                    else if (i == pixels2.GetLength(0) - 1) // Нижняя сторона
                        pixels2[i, j] = new Pixel(j, i, '▄', ConsoleColor.Gray);
                    else if (j == pixels2.GetLength(1) - 1 && i != pixels2.GetLength(0) / 2) // Правая сторона
                        pixels2[i, j] = new Pixel(j, i, '█', ConsoleColor.Gray);
                    else if (j == pixels2.GetLength(1) - 1 && i == pixels2.GetLength(0)/2) // Portal
                        pixels2[i, j] = new Pixel(j, i, '█', ConsoleColor.Green);
                    else // Пустое пространство
                        pixels2[i, j] = new Pixel(j, i, ' ');
                }
            }
        }
    }
}
