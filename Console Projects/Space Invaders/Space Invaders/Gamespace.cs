using Space_Invaders.Controllers;

namespace Space_Invaders
{
    internal class Gamespace
    {
        public int MapHeight { get; private set; }
        public int MapWidth { get; private set; }

        private Spaceship _spaceship;
        private Alien[] _aliens;
        private List<Laser> _lasers = new List<Laser>();
        private Random _random = new Random();


        private int _defeatedAliens;



        public Gamespace(int mapWidth, int mapHeight, int aliensCount)
        {
            SetConsoleSettings();
            _aliens = new Alien[aliensCount];
            MapHeight = mapHeight;
            MapWidth = mapWidth;
            GenerateMap();
            _spaceship = new Spaceship(mapWidth / 2, 
                mapHeight - 1, _aliens, _lasers);
            InitAliens();
        }

        private void GenerateMap()
        {
            for (int x = 0; x < (MapWidth + 1); x++)
            {
                for (int y = 0; y < (MapHeight + 1); y++)
                {
                    if (x == 0 && y == 0)
                        DrawController.Draw(x, y,
                            DrawController.TOP_LEFT_BORDER_CHAR);
                    else if (x == MapWidth && y == 0)
                        DrawController.Draw(x, y,
                            DrawController.TOP_RIGHT_BORDER_CHAR);
                    else if (x == 0 && y == MapHeight)
                        DrawController.Draw(x, y,
                            DrawController.BOTTOM_LEFT_BORDER_CHAR);
                    else if (x == MapWidth && y == MapHeight)
                        DrawController.Draw(x, y,
                            DrawController.BOTTOM_RIGHT_BORDER_CHAR);
                    else if (x == 0 && y != MapHeight || x == MapWidth && y != MapHeight)
                        DrawController.Draw(x, y,
                            DrawController.SIDE_BORDERS_CHAR);
                    else if (y == 0 || y == MapHeight)
                        DrawController.Draw(x, y,
                            DrawController.FRONT_BORDERS_CHAR);
                    else
                        DrawController.Draw(x, y, ' ');
                }
            }
        }

        private void SetConsoleSettings()
        {
            Console.SetWindowPosition(0, 0);
            Console.WindowHeight = 47;
            Console.CursorVisible = false;
            Console.Title = "Space Invaders";
        }

        private void InitAliens()
        {
            for (int i = 0; i < _aliens.Length; i++)
            {
                _aliens[i] = new Alien(_random.Next(1, MapWidth),
                    1, MapHeight, _lasers);
            }
        }

        public void Start()
        {
            int flag = 0;
            int alienNumber = 0;

            bool isAliensWon = false;

            while (!isAliensWon && _defeatedAliens != _aliens.Length)
            {
                _defeatedAliens = _aliens.Where(a => !a.IsAlive).Count();

                DrawController.DefeatedAliens(_defeatedAliens, _aliens.Length, MapWidth);
                
                _spaceship.TryToMove();

                _lasers.ForEach(laser => { if (laser.IsAlive)
                    { laser.TryToMove(); } } );
                _lasers.RemoveAll(a => a.IsAlive ==  false);
                
                if(flag == 0 && alienNumber < _aliens.Length)
                {
                    _aliens[alienNumber].IsStarted = true;
                    alienNumber++;
                }

                flag = (flag == 2) ? 0 : flag += 1;

                foreach (Alien alien in _aliens)
                {
                    if (alien.IsStarted && alien.IsAlive)
                        alien.TryToMove();
                    if (alien.IsWon)
                    {
                        isAliensWon = true;
                        break;
                    }
                }
            }

            if (isAliensWon)
                DrawController.Loss();
            else if (_defeatedAliens == _aliens.Length &&
                _spaceship.IsAlive)
                DrawController.Win();
        }
    }
}