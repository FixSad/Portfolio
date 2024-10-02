namespace ConsoleDrawer
{
    public interface IDrawer
    {
        void Draw(int x, int y, ConsoleColor color = ConsoleColor.White);
        void Erase(int x, int y);
    }
}
