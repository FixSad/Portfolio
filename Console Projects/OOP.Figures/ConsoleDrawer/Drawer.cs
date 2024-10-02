namespace ConsoleDrawer
{
    internal class Drawer : IDrawer
    {
        private const char SYMBOL = '.';

        public void Draw(int x, int y, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(x, y);
            Console.Write(SYMBOL);
        }

        public void Erase(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(' ');
        }
    }
}
