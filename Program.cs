using System;
using System.IO;
using System.Threading;

namespace PacmanProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            char[,] map = ReadMap("maze.txt");
            ConsoleKeyInfo pressedKey = new ConsoleKeyInfo();

            int pacmanX = 1;
            int pacmanY = 1;
            int score = 0;


            while (true)
            {

                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Blue;
                DrawMap(map);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(pacmanX, pacmanY);
                Console.Write("@");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(42, 0);
                Console.Write($"Score: {score}");

                pressedKey = Console.ReadKey();

                HandleInput(pressedKey, ref pacmanX, ref pacmanY, map, ref score);

            }
        }

        static void HandleInput(ConsoleKeyInfo pressedKey, ref int pacmanX, ref int pacmanY, char[,] map, ref int score)
        {
            int[] direction = GetDirection(pressedKey);

            int nextPacmanPositionX = pacmanX + direction[0];
            int nextPacmanPositionY = pacmanY + direction[1];

            char nextCell = map[nextPacmanPositionX, nextPacmanPositionY];

            if (nextCell == ' ' || nextCell == '.')
            {
                pacmanX = nextPacmanPositionX;
                pacmanY = nextPacmanPositionY;
                if (nextCell == '.')
                {
                    score++;
                    map[nextPacmanPositionX, nextPacmanPositionY] = ' ';
                }
            }
        }

        static int[] GetDirection(ConsoleKeyInfo pressedKey)
        {
            int[] direction = { 0, 0 };
            if (pressedKey.Key == ConsoleKey.UpArrow)
            {
                direction[1] = -1;
            }
            if (pressedKey.Key == ConsoleKey.DownArrow)
            {
                direction[1] = 1;
            }
            if (pressedKey.Key == ConsoleKey.LeftArrow)
            {
                direction[0] = -1;
            }
            if (pressedKey.Key == ConsoleKey.RightArrow)
            {
                direction[0] = 1;
            }
            return direction;
        }

        static char[,] ReadMap(string path)
        {
            string[] file = File.ReadAllLines(path);
            char[,] map = new char[GetMaxLengthofLine(file), file.Length];

            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    map[x, y] = file[y][x];
                }
            }
            return map;
        }

        static int GetMaxLengthofLine(string[] lines)
        {
            int maxLength = lines[0].Length;

            foreach (var line in lines)
            {
                if (line.Length > maxLength)
                    maxLength = line.Length;
            }
            return maxLength;
        }

        static void DrawMap(char[,] map)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    Console.Write(map[x, y]);
                }
                Console.Write("\n");
            }
        }
    }
}

