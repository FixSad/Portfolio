using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDrawer
{
    public interface IDrawer
    {
        void Draw(int x, int y, ConsoleColor color = ConsoleColor.White);
        void Erase(int x, int y);
    }
}
