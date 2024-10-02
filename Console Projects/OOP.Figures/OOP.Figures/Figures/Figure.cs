using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP.Figures.Figures
{
    internal abstract class Figure
    {
        public abstract double Area { get; }
        protected abstract double CalculateArea();
        public abstract void Paint();
        public override string ToString()
        {
            return $"Figure: {this.GetType().Name}, Area: {Area}";
        }
    }
}
