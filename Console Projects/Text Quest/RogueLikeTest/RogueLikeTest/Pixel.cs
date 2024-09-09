using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RogueLikeTest
{
    class Pixel
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Symbol { get; }
        public ConsoleColor Color { get; }
        public byte Height { get; set; }
        public byte Width { get; set; }

        public Pixel(int x, int y, char symbol, ConsoleColor color = ConsoleColor.White)
        {
            X = x;
            Y = y;
            Symbol = symbol;
            Color = color;
        }

        public void Draw()
        {
            Console.SetCursorPosition(X, Y);
            Console.ForegroundColor = Color;
            Console.Write(Symbol);
        }

        public void Clear()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(' ');
        }
    }

    class GameObject : Pixel
    {
        public bool IsAlive { get; private set; } = true;
        public byte HP { get; private set; } = 55;
        protected byte _damage;
        protected Direction _currentDirection = Direction.None;
        private byte _deadEnemies = 0;

        public GameObject(int x, int y, char symbol, ConsoleColor color = ConsoleColor.White) : base (x, y, symbol, color) 
        {


        }

        public void PrintHealthBar(Pixel[,] map, List<string> lines)
        {

            byte tempHP = HP;

            Console.SetCursorPosition(map.GetLength(1) + 1, 1);
            Console.ForegroundColor = ConsoleColor.White;

            foreach (var line in lines)
            {
                if (line[0] == '1')
                {
                    var tempLime = line.Split('|');
                    Console.Write(tempLime[1]);
                }
            }

            if (HP % 10 != 0)
            {
                while (tempHP % 10 != 0)
                {
                    tempHP++;
                }
            }

            int sticksNumber = tempHP / 10;

            for (int i = 1; i <= 10; i++)
            {

                Console.SetCursorPosition(map.GetLength(1) + i + 4, 1);
                if (sticksNumber > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    sticksNumber--;
                }
                else
                    Console.ForegroundColor = ConsoleColor.Red;

                Console.Write('█');
            }

            Console.SetCursorPosition(map.GetLength(1) + 17, 1);
            Console.WriteLine(HP);
            Console.ForegroundColor = ConsoleColor.Black;
        }

        public void Move()
        {
            switch (_currentDirection)
            {
                case Direction.Up:
                    Y--;
                    break;
                case Direction.Down:
                    Y++;
                    break;
                case Direction.Left:
                    X--;
                    break;
                case Direction.Right:
                    X++;
                    break;
                case Direction.None:
                    break;
                default:
                    break;
            }
        }

        protected void ReverseDirection()
        {
            switch (_currentDirection)
            {
                case Direction.Up:
                    _currentDirection = Direction.Down;
                    break;
                case Direction.Down:
                    _currentDirection = Direction.Up;
                    break;
                case Direction.Left:
                    _currentDirection = Direction.Right;
                    break;
                case Direction.Right:
                    _currentDirection = Direction.Left;
                    break;
                case Direction.None:
                    break;
                default:
                    break;
            }
        }

        protected virtual void ChangeDirection()
        {
            ConsoleKeyInfo input = Console.ReadKey(true);
            var key = input.Key;
            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    _currentDirection = Direction.Left;
                    break;
                case ConsoleKey.UpArrow:
                    _currentDirection = Direction.Up;
                    break;
                case ConsoleKey.RightArrow:
                    _currentDirection = Direction.Right;
                    break;
                case ConsoleKey.DownArrow:
                    _currentDirection = Direction.Down;
                    break;
                default:
                    _currentDirection = Direction.None;
                    break;
            }
        }

        public void TryMove(List<Enemy> enemies, Pixel[,] pixels2, Pixel heart)
        {
            ChangeDirection();
            Clear();

            Move();
            Bump currentBump = CheckBump(enemies, pixels2, heart);

            if (currentBump == Bump.Portal)
            {
                IsAlive = false;
                HP -= 10;
            }
            else if (currentBump == Bump.Wall)
            {
                ReverseDirection();
                Move();
            }
            else if (currentBump == Bump.Heal)
            {
                HP += 10;
            }
            else if (currentBump == Bump.Enemy)
            {
                HP -= 10;
                _deadEnemies++;
                foreach (var enemy in enemies)
                {
                    if (X == enemy.X && Y == enemy.Y)
                    {
                        enemy.IsAlive = false;
                        enemy.X = pixels2.GetLength(1) + 16 + _deadEnemies;
                        enemy.Y = 3;
                    }
                }
            }

            Draw();
        }

        private Bump CheckBump(List<Enemy> enemies, Pixel[,] pixels2, Pixel heart)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].X == X && enemies[i].Y == Y)
                {
                    return Bump.Enemy;
                }
            }
            if (X == pixels2.GetLength(1) - 1 && Y == pixels2.GetLength(0) / 2)
                return Bump.Portal;
            else if (X == 0 || Y == 0 || X == pixels2.GetLength(1) - 1 || Y == pixels2.GetLength(0) - 1)
                return Bump.Wall;
            else if (X == heart.X && Y == heart.Y)
                return Bump.Heal;
            else
                return Bump.None;
        }
    }

    class Enemy : GameObject
    {
        public byte ID;
        public bool IsAlive = true;
        public Enemy(int x, int y, char symbol, ConsoleColor color = ConsoleColor.White) : base(x, y, symbol, color) { }

        public new void ChangeDirection()
        {
            if (IsAlive)
            {
                Clear();
                Random random = new Random();
                byte dirNumber = (byte)random.Next(0, 5);

                switch (dirNumber)
                {
                    case 1:
                        _currentDirection = Direction.Up;
                        break;
                    case 2:
                        _currentDirection = Direction.Down;
                        break;
                    case 3:
                        _currentDirection = Direction.Left;
                        break;
                    case 4:
                        _currentDirection = Direction.Right;
                        break;
                    case 5:
                        _currentDirection = Direction.None;
                        break;
                    default:
                        break;
                }
            }
            else
                _currentDirection = Direction.None;
        }
        public void TryMove(GameObject gameObject, Pixel[,] pixels2, Bump inputBump)
        {
            Bump currentBump = inputBump;

            if (currentBump == Bump.Wall || currentBump == Bump.Heal || currentBump == Bump.Hero || currentBump == Bump.Enemy)
            {
                ReverseDirection();
                Move();
            }

            Draw();
        }


        public Bump CheckBump(GameObject gameObject, Pixel[,] pixels2, List<Enemy> enemies, Pixel heart)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].X == X && enemies[i].Y == Y && ID != enemies[i].ID)
                {
                    return Bump.Enemy;
                }
            }

            if (X == 0 || Y == 0 || X == pixels2.GetLength(1) - 1 || Y == pixels2.GetLength(0) - 1)
                return Bump.Wall;
            else if (X == heart.X && Y == heart.Y)
                return Bump.Heal;
            else if (X == gameObject.X && Y == gameObject.Y)
                return Bump.Hero;
            else
                return Bump.None;
        }
    }
}
