using System;
using System.Collections.Generic;

namespace BFPL.STEPIK.AllAlone
{
    public class GeniusIdiot
    {
        // Cписки вопросов и ответов
        public string[]? Questions { get; private set; }
        public int[]? Answers { get; private set; }

        private string[] diagnoses =
        [
            "Идиот",
            "Кретин",
            "Дурак",
            "Нормальный",
            "Талант",
            "Гений"
        ];

        // Счетчик правильных ответов
        private int correctAnswersCount = 0;
        // Имя пользователя
        public string Name { get; set; }

        // Или Имя пользователя и Количество вопросов
        public GeniusIdiot(string name, uint questionsAndAnswersCount)
        {
            if (questionsAndAnswersCount == 0)
            {
                throw new ArgumentOutOfRangeException($"Количество вопросов и ответов не может быть равно НУЛЮ.");
            }
            Name = name;
            SetQuestionsAndAnswers(questionsAndAnswersCount);
        }

        // Или Имя пользователя и списки Вопросов и Ответов
        public GeniusIdiot(string name, string[] questions, int[] answers)
        {
            if (questions.Length != answers.Length)
            {
                throw new ArgumentOutOfRangeException($"Количество вопросов и ответов должно быть РАВНЫМ.");
            }
            Name = name;
            Questions = questions;
            Answers = answers;
        }

        public void SetQuestionsAndAnswers(uint questionsAndAnswersCount)
        {
            Questions = new string[questionsAndAnswersCount];
            Answers = new int[questionsAndAnswersCount];

            for (int i = 0; i < questionsAndAnswersCount; i++) 
            {
                Console.WriteLine("Введите вопрос:");
                Questions[i] = GetCorrectLine();

                Console.WriteLine("Введите ответ:");
                Questions[i] = GetCorrectLine();
            }
        }

        // Щепотка сокращений
        public static string GetCorrectLine()
        {
            while (true)
            {
                string? input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input)) return input;
                Console.WriteLine("Нельзя ввести пустую строку.");
            }
        }

        // Цикл опроса
        public void StartTestCircle()
        {
            // Обнуляем счётчик
            correctAnswersCount = 0;

            List<int> indexes = new List<int>();
            for (int i = 0; i < Questions.Length; i++) indexes.Add(i);
            Shuffle(indexes); // Не LINQ, Правило: "пока курс не прошёл, не трогаю".

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(Questions[indexes[i]]);           // а) Выводим вопрос

                int userAnswer = GetCorrectUserInput();              // б) Запрашиваем ответ

                if (userAnswer == Answers[indexes[i]])              // в) Сравниваем
                {
                    correctAnswersCount++;                 // г) Увеличиваем счетчик
                }
            }
        }

        // Задание 1 "Сделать защиту от дурака"
        private int GetCorrectUserInput()
        {
            int correctUserInput = 0;

            while (true)
            {
                Console.Write("Ваш ответ: ");
                string userInput = Console.ReadLine() ?? "";

                try
                {
                    correctUserInput = Convert.ToInt32(userInput);
                }
                catch
                {
                    Console.WriteLine("Пожалуйста, введите число!");
                    continue;
                }

                break;
            }

            return correctUserInput;
        }

        // Тасование Фишера–Йетса
        public static void Shuffle<T>(List<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                // Меняем текущий элемент N со случайным индексом K
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        // Цикличный повтор теста
        public void StartRepetitiveTest()
        {
            while (true)
            {
                // 3. Цикл опроса
                StartTestCircle();

                // 4. Вывод результата
                Console.WriteLine($"{Name}, вот результат теста.");
                Console.WriteLine($"Ваш диагноз: {GetDiagnos()}\n");

                while (true)
                {
                    Console.WriteLine("Вы хотите повторить тест? ДА/НЕТ");
                    string unswer = (Console.ReadLine() ?? "").ToUpper();
                    if (unswer == "ДА")
                    {
                        Console.WriteLine("Хорошо, начинаем тест заново.\n");
                        break;
                    }
                    else if (unswer == "НЕТ")
                    {
                        Console.WriteLine("Принято, удачи.\n");
                        return;
                    }
                    else Console.WriteLine("Неопознанный ответ.\n");
                }
            }
        }

        // Вывод результата
        public string GetDiagnos()
        {
            if (correctAnswersCount == 0) // Идиот
            {
                return diagnoses[0];
            }
            if (correctAnswersCount == Questions.Length) // Гений
            {
                return diagnoses[diagnoses.Length - 1];
            }

            // Всемогущие пропорции
            int result = (int)((double)correctAnswersCount * (double)diagnoses.Length / (double)Questions.Length);

            if (result == 0) result++; // Кретин

            return diagnoses[result]; // Остальное
        }

        // Пример работы
        public static void TryExampleOfWork()
        {
            // 1. Создаем списки вопросов и ответов
            string[] questions = new string[5];
            questions[0] = "Сколько будет 2 плюс 2, умноженное на 2?";
            questions[1] = "Бревно нужно распилить на 10 частей. Сколько надо сделать распилов?";
            questions[2] = "На двух руках 10 пальцев. Сколько пальцев на 5 руках?";
            questions[3] = "Укол делают каждые полчаса. Сколько нужно минут для трех уколов?";
            questions[4] = "5 свечей горело, 2 потухли. Сколько свечей осталось?";

            int[] answers = new int[5];
            answers[0] = 6;
            answers[1] = 9;
            answers[2] = 25;
            answers[3] = 60;
            answers[4] = 2;

            // Получаем имя тестируемого
            Console.WriteLine("Введите имя тестируемого:");
            string name = GeniusIdiot.GetCorrectLine();

            GeniusIdiot geniusIdiot = new GeniusIdiot(name, questions, answers);

            // Проводим тестирование
            geniusIdiot.StartRepetitiveTest();
        }
    }
}
