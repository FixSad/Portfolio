using ConsoleDrawer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP.Figures.Figures
{
    internal class Circle : Figure
    {
        private readonly int _radius;
        public override double Area { get; }

        public Circle(int radius)
        {
            _radius = Math.Abs(radius);
            Area = CalculateArea();
        }

        public override void Paint()
        {
            for (int i = 0; i <= 2 * _radius; i++)
            {
                for (int j = 0; j <= 2 * _radius; j++)
                {
                    double distance = Math.Sqrt(Math.Pow(j - _radius, 2) + Math.Pow(i - _radius, 2));
                    if(distance<_radius+0.5 && distance > _radius-0.5)
                        DrawProvider.Drawer.Draw(j+1, i+1);
                }
            }
        }

        protected override double CalculateArea() => Math.PI * Math.Pow(_radius, 2);
    }
}
