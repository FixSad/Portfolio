using Space_Invaders.Controllers;
using Space_Invaders.Enums;

namespace Space_Invaders
{
    internal class Alien : GameObject
    {
        public bool IsStarted { get; set; }
        public bool IsWon { get; set; }

        private int _mapHeight;
        private Random _random;


        private List<Laser> _lasers;

        public Alien(int x, int y, int mapHeight, List<Laser> lasers)
            : base(x, y)
        {
            IsStarted = IsWon = false;
            _lasers = lasers;
            _random = new Random();
            _mapHeight = mapHeight;
        }
         
        public override Bump CheckBump()
        {
            for (int i = 0; i < _lasers.Count; i++)
            {
                if (_lasers[i].X == X && _lasers[i].Y == Y
                    && _lasers[i].IsAlive)
                {
                    _lasers[i].IsAlive = false;
                    return Bump.Laser;
                }
            }
            if (Y >= _mapHeight-1)
                return Bump.Bottom;
            else
                return Bump.None;
        }

        public override void TryToMove()
        {
            DrawController.Erase(X, Y);
            CurrentBump = CheckBump();


            if (CurrentBump == Bump.Bottom)
                IsWon = true;
            else if (CurrentBump == Bump.Laser ||
                CurrentBump == Bump.Bottom)
            {
                Console.SetCursorPosition(30, 30);
                Console.Write($"Bump: {CurrentBump}");
                IsAlive = false;
                DrawController.Erase(X, Y);
            }
            else
            {
                Y += 1;
                DrawController.Draw(X, Y,
                    DrawController.ALIEN_CHAR,
                    DrawController.ALIEN_COLOR);
            }
        }
    }
}