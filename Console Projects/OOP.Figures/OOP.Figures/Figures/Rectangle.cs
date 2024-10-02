using ConsoleDrawer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP.Figures.Figures
{
    internal class Rectangle : Figure
    {
        private readonly int _width, _height;
        public override double Area { get; }

        public Rectangle(int width, int height)
        {
            _width = Math.Abs(width);
            _height = Math.Abs(height);
            Area = CalculateArea();
        }

        public override void Paint()
        {
            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    if (i == 0 || i == _height - 1 || j == 0 || j == _width - 1)
                        DrawProvider.Drawer.Draw(j, i);
                }
            }
        }

        protected override double CalculateArea() => _width * _height;
    }
}
