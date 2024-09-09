using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace RogueLikeTest
{
    class Game
    {
        private List<string> _lines = new List<string>();
        public void Start()
        {
            
            QuestRoom txtQuest = new QuestRoom("Menu", "rus");
            txtQuest.StartRoom();
            txtQuest = new QuestRoom("Prologue", txtQuest.CurrentLanguage);
            txtQuest.StartRoom();

            if (txtQuest.IsAlive == true)
            {
                var lines = new List<string>();

                using (StreamReader sr = new StreamReader("InGame/" + txtQuest.CurrentLanguage + ".txt", true))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line[0] != '!')
                            lines.Add(line);
                    }
                }


                // Игра
                Random random = new Random();

                Console.Clear();
                GameObject gameObject = new GameObject(2, 3, '☺', ConsoleColor.Cyan);
                List<Enemy> enemies = new List<Enemy>();
                EnemiesActions enemiesActions = new EnemiesActions(5, gameObject);

                Console.Clear();
                enemiesActions.room.Draw();
                enemiesActions.GenerateHeart();

                gameObject.Draw();


                Thread thread = new Thread(ThreadAction);
                thread.Start(enemiesActions);

                gameObject.PrintHealthBar(enemiesActions.room.pixels2, lines);
                enemiesActions.PrintKilledEnemies(lines);

                while (gameObject.IsAlive == true)
                {
                    enemiesActions.PrintKilledEnemies(lines);

                    gameObject.PrintHealthBar(enemiesActions.room.pixels2, lines);

                    gameObject.TryMove(enemiesActions.enemies, enemiesActions.room.pixels2, enemiesActions.heart);

                }
                gameObject = null;
                enemiesActions = null;
                enemies = null;
                thread.Abort();

                Console.Clear();

                txtQuest = new QuestRoom("Epilogue", txtQuest.CurrentLanguage);
                txtQuest.StartRoom();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Ты проиграл");
            }

        }

        private void ThreadAction(object obj)
        {
            if (obj is EnemiesActions actions)
            {
                actions.MoveEnemies();
            }
        }
    }


    class EnemiesActions
    {

        private Random random = new Random();
        public Room room = new Room();
        public List<Enemy> enemies = new List<Enemy>();
        public byte EnemyCount;
        private GameObject _hero;
        public Pixel heart;

        public EnemiesActions(byte countEnemy, GameObject hero)
        {
            EnemyCount = countEnemy;
            AddEnemies();
            _hero = hero;
        }

        public void PrintKilledEnemies(List<string> lines)
        {
            string[] tempLine = new string[2];
            foreach (var line in lines)
            {
                if (line[0] == '2')
                {
                    tempLine = line.Split('|');
                }
            }

            Console.SetCursorPosition(room.pixels2.GetLength(1) + 1, 3);
            Console.Write(tempLine[1]);
        }

        public void GenerateHeart()
        {
            Pixel heart1 = new Pixel(random.Next(1, room.pixels2.GetLength(1) - 2), random.Next(1, room.pixels2.GetLength(0) - 2), '♥', ConsoleColor.Red);
            foreach (var item in enemies)
            {
                if (item.X == heart1.X || item.Y == heart1.Y)
                    GenerateHeart();
                else
                {
                    heart1.Draw();
                    break;
                }
            }
            heart = heart1;
        }

        private void AddEnemies()
        {
            for (byte i = 0; i < EnemyCount; i++)
            {
                enemies.Add(new Enemy(random.Next(1, room.pixels2.GetLength(1) - 2), random.Next(1, room.pixels2.GetLength(0) - 2), '♂', ConsoleColor.DarkMagenta) { ID = i });
            }

            for (byte i = 0; i < enemies.Count; i++)
            {
                for (byte j = (byte)(i + 1); j < enemies.Count; j++)
                {
                    if (enemies[i].X == enemies[j].X && enemies[i].Y == enemies[j].Y)
                        enemies[j] = new Enemy(random.Next(1, room.pixels2.GetLength(1) - 2), random.Next(1, room.pixels2.GetLength(0) - 2), '♂', ConsoleColor.DarkMagenta) { ID = j };
                }
            }
        }

        public void MoveEnemies()
        {
            Bump currentBump = Bump.None;
            while (true)
            {
                foreach (var item in enemies)
                {
                    item.ChangeDirection();
                    item.Move();
                    currentBump = item.CheckBump(_hero, room.pixels2, enemies, heart);
                    item.TryMove(_hero, room.pixels2, currentBump);
                    Thread.Sleep(100);
                }
            }
        }
    }
}
