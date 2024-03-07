using Space_Invaders;

Gamespace gamespace = new Gamespace(19, 39, 2);
gamespace.Start();
ConsoleKeyInfo key = Console.ReadKey();

while (key.Key != ConsoleKey.Escape)
{
    gamespace.Start();
    key = Console.ReadKey();
}