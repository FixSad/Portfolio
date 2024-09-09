using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLikeTest
{
    class QuestRoom
    {
        private bool _isPlaying = true;
        private bool _isException = false;
        private char _currentRoom;
        private string _folderName;
        public bool IsAlive { get; private set; } = true;
        public string CurrentLanguage { get; private set; } = "rus";

        // Лист с вариантами ответов
        private List<string> _lines = new List<string>();

        // Лого
        public void Splash(string splashTitle)
        {
            if (_isException == false)
            {
                Console.SetCursorPosition(30, 10);
                Console.WriteLine(splashTitle);
                System.Threading.Thread.Sleep(2000);
                Console.SetCursorPosition(0, 0);
            }
        }

        // Конструктор со считыванием txt
        public QuestRoom(string folderName, string fileName)
        {
            try
            {
                _folderName = folderName;
                ReadFile(folderName, fileName);
            }
            catch (Exception ex)
            {
                /*_isException = true;
                Console.WriteLine("Problems with the txt File");*/
            }
        }

        // Вывод вариантов ответа
        private void PrintOptions(char roomKey)
        {
            bool flag;
            foreach (var item in _lines)
            {
                if (item[0] == roomKey)
                {
                    if (item[1] == '?' || item[1] == '*')
                        _isPlaying = false;
                    if (item[1] == '^')
                        Environment.Exit(0);
                    if (item[2] == '$')
                        IsAlive = false;


                    flag = false;
                    for (int i = 0; i < item.Length; i++)
                    {
                        if (flag == true && item[i] != '|')
                            Console.Write(item[i]);

                        if (item[i] == '|')
                        {
                            Console.WriteLine();
                            flag = true;
                        }
                    }
                    Console.WriteLine();

                    if (item.Contains("=") && _isPlaying == true)
                    {
                        Console.ReadKey(true);
                        StartRoom(item[1]);
                    }
                }
            }
        }


        // Считать ответ игрока
        private void ReadAnswer(char roomKey)
        {
            if (_isPlaying == true)
            {
                ConsoleKeyInfo input = Console.ReadKey(true);

                string stringNumberRoom = input.KeyChar.ToString();

                byte intNumberRoom;

                byte.TryParse(stringNumberRoom, out intNumberRoom);

                char charNumberRoom = (char)intNumberRoom;

                string currentLine = "";


                foreach (var item in _lines)
                {
                    if (item[0] == roomKey)
                        currentLine = item;
                }

                // Подсчитывание вариантов ответа
                byte variations = 0;
                foreach (var item in currentLine)
                {
                    if (item == '|')
                        break;
                    else
                        variations++;
                }
                variations--;

                // Считывание ответа и переход в другую комнату
                if (variations >= intNumberRoom)
                {
                    // Проверка на смену языка
                    if (currentLine.Contains('¡'))
                    {
                        char spec = currentLine[intNumberRoom];

                        foreach (var line in _lines)
                        {
                            if (line[0] == spec)
                            {
                                var menu = line.Split('|');
                                ReadFile(_folderName, menu[1]);
                                StartRoom();
                            }
                        }
                    }
                    else
                        StartRoom(currentLine[intNumberRoom]);
                }    
                else
                    ReadAnswer(_currentRoom);
            }
        }

        // Метод считывания txt 
        private void ReadFile(string folderName, string fileName)
        {
            CurrentLanguage = fileName;
            _lines = new List<string>();

            using (StreamReader sr = new StreamReader(folderName + "/" + fileName + ".txt", true))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line[0] != '!')
                        _lines.Add(line);
                }
            }
        }

        // Запуск комнаты
        public void StartRoom(char key = '1')
        {
            if (_isException == false)
            {
                _currentRoom = key;
                Console.Clear();
                PrintOptions(key);
                if (_isPlaying == true)
                    ReadAnswer(key);
                else
                {
                    //Console.SetCursorPosition(0, 5);
                    //Console.ReadKey();
                    Console.Clear();
                    //Console.WriteLine("GAME OVER");
                }
            }
        }
    }
}
