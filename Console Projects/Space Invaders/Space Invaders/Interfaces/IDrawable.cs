using System;
using System.Collections.Generic;

namespace Space_Invaders.Interfaces
{
    internal interface IDrawable
    {
        public static void Draw(int x, int y, char symbol, ConsoleColor foreground) { }
        public static void Erase(int x, int y) { }
        public static void Win() { }
        public static void Loss() { }
        public static void DefeatedAliens(int defeated,
            int inTotal, int mapWidth)
        { }
    }
}