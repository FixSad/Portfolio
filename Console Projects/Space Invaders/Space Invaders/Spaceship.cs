using Space_Invaders.Controllers;
using Space_Invaders.Enums;

namespace Space_Invaders
{
    internal class Spaceship : GameObject
    {
        public Direction CurrentDirection { get; private set; }

        private List<Laser> _lasers;

        private Alien[] _aliens;

        public Spaceship(int x, int y, Alien[] aliens, List<Laser> lasers) 
            : base(x, y)
        {
            CurrentDirection = Direction.None;
            _lasers = lasers;
            _aliens = aliens;
            DrawController.Draw(X, Y,
                DrawController.SPACESHIP_CHAR,
                DrawController.SPACESHIP_COLOR);
        }

        private void Move()
        {
            switch (CurrentDirection)
            {
                case Direction.Left:
                    X--;
                    break;
                case Direction.Right:
                    X++;
                    break;
                case Direction.None:
                    break;
            }
        }

        private void ChangeAction()
        {
            ConsoleKeyInfo input = Console.ReadKey(true);
            var key = input.Key;
            switch (key)
            {
                case ConsoleKey.Spacebar:
                    Fire();
                    CurrentDirection = Direction.None;
                    break;
                case ConsoleKey.LeftArrow:
                    CurrentDirection = Direction.Left;
                    break;
                case ConsoleKey.RightArrow:
                    CurrentDirection = Direction.Right;
                    break;
                default:
                    CurrentDirection = Direction.None;
                    break;
            }
        }

        public override Bump CheckBump()
        {
            if (X == 0 || Y == 0 || X == 19 || Y == 39)
                return Bump.Border;
            else
                return Bump.None;
        }

        public override void TryToMove()
        {
            if (IsAlive)
            {
                ChangeAction();
                DrawController.Erase(X, Y);
                Move();
                CurrentBump = CheckBump();

                switch (CurrentBump)
                {
                    case Bump.None:
                        break;
                    case Bump.Border:
                        FlipDirection();
                        Move();
                        break;
                    case Bump.Laser:
                        break;
                    case Bump.Alien:
                        break;
                    case Bump.Bottom:
                        break;
                    default:
                        break;
                }

                DrawController.Draw(X, Y,
                    DrawController.SPACESHIP_CHAR,
                    DrawController.SPACESHIP_COLOR);
            }
        }

        private void FlipDirection()
        {
            if (CurrentDirection == Direction.Left)
                CurrentDirection = Direction.Right;
            else if (CurrentDirection == Direction.Right)
                CurrentDirection = Direction.Left;
        }

        private void Fire()
        {
            Laser laser = new Laser(X, Y, _aliens);
            _lasers.Add(laser);
        }
    }
}