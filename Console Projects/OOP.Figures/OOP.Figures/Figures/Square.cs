using ConsoleDrawer;

namespace OOP.Figures.Figures
{
    internal class Square : Figure
    {
        private readonly int _side;
        public override double Area { get; }

        public Square(int side)
        {
            _side = Math.Abs(side);
            Area = CalculateArea();
        }

        public override void Paint()
        {
            for (int i = 0; i < _side; i++)
            {
                for (int j = 0; j < _side; j++)
                {
                    if (i == 0 || i == _side - 1 || j == 0 || j == _side - 1)
                        DrawProvider.Drawer.Draw(i, j); 
                }
            }
            Console.WriteLine();
        }

        protected override double CalculateArea() => Math.Pow(_side, 2);
    }
}
