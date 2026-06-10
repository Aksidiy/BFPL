using System;
using System.Collections.Generic;
using System.Text;

namespace BFPL.STEPIK.Basic_Sintax
{
    static public class XesAndZeros
    {
        static public void GoXesAndZeros()
        {
            int uzwer = 0;
            Console.WriteLine("Добро пожаловать в игру 'Крестики и Нолики'!");
            PrintMainMemu();
            while (true)
            {
                string input = Console.ReadLine().ToUpper();
                switch (input)
                {
                    case "START":
                        {
                            Console.WriteLine("Старт новой игры...");
                            GameBody();
                            PrintMainMemu();
                            break;
                        }
                    case "CLEAR":
                        {
                            Console.Clear();
                            Console.WriteLine("Добро пожаловать в игру 'Крестики и Нолики'!");
                            PrintMainMemu();
                            break;
                        }
                    case "IDDQD":
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("RIP AND TEAR!!!");
                            DOOM();
                            Console.ResetColor();
                            PrintMainMemu();
                            break;
                        }
                    case "EXIT":
                        {
                            Console.WriteLine("До новых встреч!");
                            return;
                        }
                    default:
                        {
                            Console.WriteLine("Ошибка ввода, попробуйте снова.");
                            break;
                        }
                }
            }
        }

        static void PrintMainMemu()
        {
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Новыя игра: START");
            Console.ResetColor();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Очистить консоль: CLEAR");
            Console.ResetColor();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Выход: EXIT");
            Console.ResetColor();

            Console.WriteLine();
            Console.Write("Введите ключевое слово (регистр не важен): ");
        }

        static void GameBody()
        {
            //Первоначальные данные
            bool isZeroNow = false;
            string[,] gameMap = new string[3, 3]
            {
                {"1", "2", "3"},
                {"4", "5", "6"},
                {"7", "8", "9"}
            };

            //Старт игры
            for (int i = 0; i < 9; i++)
            {
                //Кто ходит?
                if (!isZeroNow) Console.WriteLine("Ходят крестики");
                else Console.WriteLine("Ходят нолики");

                PrintMap(gameMap);

                Console.WriteLine("Введите цифру вашего хода:");
                string cellNumber = GetPlayerCellNumber(gameMap);
                MakeMove(gameMap, cellNumber, isZeroNow);

                bool isWinner = HasWinner(gameMap);
                if (isWinner && !isZeroNow) { PrintMap(gameMap); Console.WriteLine("Крестики победили!"); return; }
                else if (isWinner && isZeroNow) { PrintMap(gameMap); Console.WriteLine("Нолики победили!"); return; }
                else if (i == 8) { PrintMap(gameMap); Console.WriteLine("Ничья!"); return; }

                //Передаём ход следующему игроку
                isZeroNow = !isZeroNow;
            }
        }
        static bool HasWinner(string[,] gameMap)
        {
            bool hasWinner = false;
            string[] combinations = new string[8];
            for (int i = 0; i < 3; i++) combinations[i] = gameMap[i, 0] + gameMap[i, 1] + gameMap[i, 2];
            for (int i = 0, j = 3; i < 3; i++, j++) combinations[j] = gameMap[0, i] + gameMap[1, i] + gameMap[2, i];
            combinations[6] = gameMap[0, 0] + gameMap[1, 1] + gameMap[2, 2];
            combinations[7] = gameMap[0, 2] + gameMap[1, 1] + gameMap[2, 0];

            foreach (string combination in combinations)
            {
                if (combination == "XXX" || combination == "OOO") hasWinner = true;
            }

            return hasWinner;
        }

        static string[,] MakeMove(string[,] gameMap, string cellNumber, bool isZeroNow)
        {
            int cellNumberInt = Convert.ToInt32(cellNumber) - 1;
            int row = cellNumberInt / 3, col = cellNumberInt % 3;

            if (isZeroNow) gameMap[row, col] = "O";
            else gameMap[row, col] = "X";

            return gameMap;
        }

        static string GetPlayerCellNumber(string[,] gameMap)
        {
            while (true)
            {
                string cellNumber = Console.ReadLine();

                if (cellNumber.Length == 1)
                {
                    if ('1' <= Convert.ToChar(cellNumber) && Convert.ToChar(cellNumber) <= '9')
                    {
                        if (IsMoveCorrect(gameMap, cellNumber)) return cellNumber;
                        else Console.WriteLine("Неверный ввод. Пожалуйста, введите цифру пустой ячейки.");
                    }
                    else Console.WriteLine("Неверный ввод. Пожалуйста, введите цифру от 1 до 9.");
                }
                else Console.WriteLine("Неверный ввод. Пожалуйста, введите цифру от 1 до 9.");
            }
        }

        static bool IsMoveCorrect(string[,] gameMap, string cellNumber)
        {
            int cellNumberInt = Convert.ToInt32(cellNumber) - 1;
            int row = cellNumberInt / 3, col = cellNumberInt % 3;
            return (gameMap[row, col] != "X" && gameMap[row, col] != "O") ? true : false;
        }

        static void PrintMap(string[,] gameMap)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (gameMap[i, j] == "X")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"{gameMap[i, j]} ");
                        Console.ResetColor();
                    }
                    else if (gameMap[i, j] == "O")
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write($"{gameMap[i, j]} ");
                        Console.ResetColor();
                    }
                    else Console.Write($"{gameMap[i, j]} ");
                }
                Console.WriteLine();
            }
        }

        static void DOOM()
        {
            Console.WriteLine(
                "+-----------------------------------------------------------------------------+\r\n" +
                "| |       |\\                                           -~ /     \\  /          |\r\n" +
                "|~~__     | \\                                         | \\/       /\\          /|\r\n" +
                "|    --   |  \\                                        | / \\    /    \\     /   |\r\n" +
                "|      |~_|   \\                                   \\___|/    \\/         /      |\r\n" +
                "|--__  |   -- |\\________________________________/~~\\~~|    /  \\     /     \\   |\r\n" +
                "|   |~~--__  |~_|____|____|____|____|____|____|/ /  \\/|\\ /      \\/          \\/|\r\n" +
                "|   |      |~--_|__|____|____|____|____|____|_/ /|    |/ \\    /   \\       /   |\r\n" +
                "|___|______|__|_||____|____|____|____|____|__[]/_|----|    \\/       \\  /      |\r\n" +
                "|  \\mmmm :   | _|___|____|____|____|____|____|___|  /\\|   /  \\      /  \\      |\r\n" +
                "|      B :_--~~ |_|____|____|____|____|____|____|  |  |\\/      \\ /        \\   |\r\n" +
                "|  __--P :  |  /                                /  /  | \\     /  \\          /\\|\r\n" +
                "|~~  |   :  | /                                 ~~~   |  \\  /      \\      /   |\r\n" +
                "|    |      |/                        .-.             |  /\\          \\  /     |\r\n" +
                "|    |      /                        |   |            |/   \\          /\\      |\r\n" +
                "|    |     /                        |     |            -_   \\       /    \\    |\r\n" +
                "+-----------------------------------------------------------------------------+\r\n" +
                "|          |  /|  |   |  2  3  4  | /~~~~~\\ |       /|    |_| ....  ......... |\r\n" +
                "|          |  ~|~ | % |           | | ~J~ | |       ~|~ % |_| ....  ......... |\r\n" +
                "|   AMMO   |  HEALTH  |  5  6  7  |  \\===/  |    ARMOR    |#| ....  ......... |\r\n" +
                "+-----------------------------------------------------------------------------+\r\n" +
                "          \"From the people who gave you VI - NEW Ascii DOOM!\"     -Beth Mayo");

        }
    }
}
