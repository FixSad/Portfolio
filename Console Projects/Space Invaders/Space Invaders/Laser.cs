using Space_Invaders.Controllers;
using Space_Invaders.Enums;

namespace Space_Invaders
{
    internal class Laser : GameObject
    {
        private bool _isStartLife = true;

        private Alien[] _aliens;

        public Laser(int x, int y, Alien[] aliens)
            : base(x, y)
        {
            _aliens = aliens;
        }

        public override void TryToMove()
        {
            if(!_isStartLife)
                DrawController.Erase(X, Y);

            CurrentBump = CheckBump();

            if(CurrentBump == Bump.Border 
                || CurrentBump == Bump.Alien)
            {
                DrawController.Erase(X, Y);
                IsAlive = false;
            }

            if (IsAlive)
            {
                Y -= 1;
                DrawController.Draw(X, Y,
                    DrawController.LASER_CHAR,
                    DrawController.LASER_COLOR);
                

                _isStartLife = false;
            }

            if(IsAlive)
                CurrentBump = Bump.None;
        }

        public override Bump CheckBump()
        {
            for (int i = 0; i < _aliens.Length; i++)
            {
                if (_aliens[i].X == X && _aliens[i].Y == Y
                    && _aliens[i].IsAlive)
                {
                    _aliens[i].IsAlive = false;
                    return Bump.Alien;
                }
            }
            if (Y == 1)
                return Bump.Border;
            else
                return Bump.None;
        }
    }
}