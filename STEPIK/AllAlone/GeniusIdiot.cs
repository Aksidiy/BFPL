using System;
using System.Collections.Generic;
using System.Text;

namespace BFPL.STEPIK.AllAlone
{
    public class GeniusIdiot
    {
        // Cписки вопросов и ответов
        public string[] Questions { get; }
        public int[] Answers { get; }
        // Счетчик правильных ответов
        private int correctAnswersCount = 0;

        // Заполняем списки вопросов и ответов
        public GeniusIdiot(string[] questions, int[] answers)
        {
            if (questions.Length != 5 || answers.Length != 5)
            {
                throw new ArgumentOutOfRangeException($"Количество вопросов и ответов должно быть равно 5. Переданные ");
            }
            Questions = questions;
            Answers = answers;
        }

        // Цикл опроса
        public void StartTestCircle()
        {
            // Обнуляем счётчик
            correctAnswersCount = 0;

            List<int> indexes = new List<int>() { 0, 1, 2, 3, 4 };
            Shuffle(indexes); // Не LINQ, Правило: "пока курс не прошёл, не трогаю".
            indexes.ForEach(item => Console.WriteLine(item));

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(Questions[indexes[i]]);           // а) Выводим вопрос

                Console.Write("Ваш ответ: ");              // б) Запрашиваем ответ
                int userAnswer = int.Parse(Console.ReadLine() ?? "0");

                if (userAnswer == Answers[indexes[i]])              // в) Сравниваем
                {
                    correctAnswersCount++;                 // г) Увеличиваем счетчик
                }
            }
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

        // Вывод результата
        public void GetCorrectAnswersCount()
        {
            Console.WriteLine();
            Console.WriteLine($"Количество правильных ответов: {correctAnswersCount}");
        }

        // Пример работы
        public static void ExampleOfWork()
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

            GeniusIdiot geniusIdiot = new GeniusIdiot(questions, answers);

            // 3. Цикл опроса
            geniusIdiot.StartTestCircle();
            // 4. Вывод результата
            geniusIdiot.GetCorrectAnswersCount();
        }
    }
}
