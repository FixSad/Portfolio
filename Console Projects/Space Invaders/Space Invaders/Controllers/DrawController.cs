using Space_Invaders.Interfaces;

namespace Space_Invaders.Controllers
{
    internal class DrawController : IDrawable
    {
        public const char ALIEN_CHAR = '╦';
        public const ConsoleColor ALIEN_COLOR = ConsoleColor.Red;

        public const char LASER_CHAR = '^';
        public const ConsoleColor LASER_COLOR = ConsoleColor.Blue;

        public const char TOP_LEFT_BORDER_CHAR = '╔';
        public const char TOP_RIGHT_BORDER_CHAR = '╗';
        public const char BOTTOM_RIGHT_BORDER_CHAR = '╝';
        public const char BOTTOM_LEFT_BORDER_CHAR = '╚';
        public const char FRONT_BORDERS_CHAR = '═';
        public const char SIDE_BORDERS_CHAR = '║';
        public const ConsoleColor BORDER_COLOR = ConsoleColor.Gray;

        public const char SPACESHIP_CHAR = '╩';
        public const ConsoleColor SPACESHIP_COLOR = ConsoleColor.Green;

        public static void Draw(int x, int y, char symbol,
            ConsoleColor foreground = ConsoleColor.Gray)
        {
            Console.ForegroundColor = foreground;
            Console.SetCursorPosition(x, y);
            Console.Write(symbol);
        }

        public static void Erase(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(' ');
        }

        public static void Loss()
        {
            Console.SetCursorPosition(10, 10);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"You Have Lost...\n" +
                $"Aliens have taken over the universe.");
        }

        public static void Win()
        {
            Console.SetCursorPosition(10, 10);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("You Have Won!\n" +
                "All aliens have been destroyed.");
        }

        public static void DefeatedAliens(int defeated,
            int inTotal, int mapWidth)
        {
            Console.SetCursorPosition(mapWidth + 2, 0);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Defeated Aliens {defeated}/{inTotal}   ");
        }
    }
}