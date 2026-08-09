using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Xml;
using System.Xml.Linq;

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

            for (int i = 0; i < Questions.Length; i++)
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

                // Сохранение результата по желанию
                while (true)
                {
                    Console.WriteLine("Вы хотите сохранить результаты теста? ДА/НЕТ");
                    string unswer = (Console.ReadLine() ?? "").ToUpper();
                    if (unswer == "ДА")
                    {
                        Console.WriteLine("Хорошо, начинаем сохранение.\n");
                        SaveTestResultsToFile();
                        break;
                    }
                    else if (unswer == "НЕТ")
                    {
                        Console.WriteLine("Принято, результат забыт.\n");
                        break;
                    }
                    else Console.WriteLine("Неопознанный ответ.\n");
                }

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

        // Хранение результата
        private void SaveTestResultsToFile(string filePath = "Genius_Idiot_Test_Results.xml")
        {
            /*
            * Шаблон файла:
            *  Subjects
            *      Subject
            *          name
            *          result
            *          diagnose
            *      EndSubject
            *  EndSubjects
            */

            XmlDocument xmlDoc = new XmlDocument();

            try
            {
                Console.WriteLine("Проверка файла с результатами тестирования...");
                xmlDoc.Load(filePath);
                Console.WriteLine("Файл с результатами тестирования НАЙДЕН.");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл с результатами тестирования ОТСУТСВУЕТ.");
                Console.WriteLine("Создание нового файла...");

                XmlDeclaration declaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                xmlDoc.AppendChild(declaration);
            }
            catch (XmlException)
            {
                Console.WriteLine("Отсутствует корень файла.");
                Console.WriteLine("Создание нового корня файла...");

                XmlElement newXmlRoot = xmlDoc.CreateElement("subjects");
                xmlDoc.AppendChild(newXmlRoot);
            }
            Console.WriteLine("Запись данных в файл...");

            XmlElement? xmlRoot = xmlDoc.DocumentElement;

            if (xmlRoot == null)
            {
                xmlRoot = xmlDoc.CreateElement("subjects");
                xmlDoc.AppendChild(xmlRoot);
            }

            XmlElement subjectElem = xmlDoc.CreateElement("subject");
            {
                XmlElement name = xmlDoc.CreateElement("name");
                XmlText nameText = xmlDoc.CreateTextNode(Name);
                name.AppendChild(nameText);

                XmlElement result = xmlDoc.CreateElement("result");
                XmlText resultText = xmlDoc.CreateTextNode(correctAnswersCount.ToString());
                result.AppendChild(resultText);

                XmlElement diagnose = xmlDoc.CreateElement("diagnose");
                XmlText diagnoseText = xmlDoc.CreateTextNode(GetDiagnos());
                diagnose.AppendChild(diagnoseText);

                XmlElement date_time = xmlDoc.CreateElement("date_time");
                XmlText date_timeText = xmlDoc.CreateTextNode(DateTime.Now.ToString("u"));
                date_time.AppendChild(date_timeText);

                subjectElem.AppendChild(name);
                subjectElem.AppendChild(result);
                subjectElem.AppendChild(diagnose);
                subjectElem.AppendChild(date_time);
            }

            xmlRoot?.AppendChild(subjectElem);

            xmlDoc.Save(filePath);
        }

        // Чтение результата
        public static void PrintTestResultsToConsole(string filePath = "Genius_Idiot_Test_Results.xml") 
        {
            /*
                * Шаблон файла:
                * 
                * subject
                *    name
                *    result
                *    diagnose
                * subjectEnd
                * 
                */
            try
            {
                Console.WriteLine("Проверка файла с результатами тестирования...");

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filePath);

                XmlElement? xRoot = xmlDoc.DocumentElement;

                if (xRoot == null)
                {
                    Console.WriteLine("Файл пуст!");
                    return;
                }

                foreach (XmlElement xnode in xRoot)
                {
                    foreach (XmlNode node in xnode.ChildNodes)
                    {
                        Console.Write($"[{node.Name}: {node.InnerText}\t\t]");
                    }
                    Console.WriteLine();
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл с результатами тестирования ОТСУТСВУЕТ.");
            }
        }

        // Пример работы
        public static void TryExampleOfWork()
        {
            GeniusIdiotTest();

            /*
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
            */
        }

        // Играюсь с подменой ввода консоли. И да, мне лень руками тестировать.
        public static void GeniusIdiotTest(int iterationsCount = 10)
        {
            if (iterationsCount <= 0)
            {
                Console.WriteLine("!!! (ﾉಥ益ಥ）ﾉ﻿ ┻━┻ !!!");
                return;
            }

            // Исходные тестовые данные
            string[] defQuestions = new string[5];
            defQuestions[0] = "Сколько будет 2 плюс 2, умноженное на 2?";
            defQuestions[1] = "Бревно нужно распилить на 10 частей. Сколько надо сделать распилов?";
            defQuestions[2] = "На двух руках 10 пальцев. Сколько пальцев на 5 руках?";
            defQuestions[3] = "Укол делают каждые полчаса. Сколько нужно минут для трех уколов?";
            defQuestions[4] = "5 свечей горело, 2 потухли. Сколько свечей осталось?";

            int[] defAnswers = new int[5];
            defAnswers[0] = 6;
            defAnswers[1] = 9;
            defAnswers[2] = 25;
            defAnswers[3] = 60;
            defAnswers[4] = 2;

            string[] names = new string[5];
            names[0] = "Иван";
            names[1] = "Даниил";
            names[2] = "Илья";
            names[3] = "Артём";
            names[4] = "Дмитрий";

            string[] yesAndNo = new string[2];
            yesAndNo[0] = "ДА";
            yesAndNo[1] = "НЕТ";

            // Генерация тестовых данных
            Random rng = new Random();

            string[] questions = new string[iterationsCount];
            int[] answers = new int[iterationsCount];

            for (int i = 0; i < iterationsCount; i++)
            {
                questions[i] = defQuestions[rng.Next(0, 4)];
                answers[i] = defAnswers[rng.Next(0, 4)];
            }

            // Создания мимикрии под ввод пользователя в консоль
            TextReader originalInput = Console.In;

            string mimik = "";

            for (int testIteration = 0; testIteration < iterationsCount; testIteration++)
            {
                for (int answerInIteration = 0; answerInIteration < iterationsCount; answerInIteration++) 
                {
                    mimik += $"{answers[rng.Next(0, iterationsCount - 1)]}\n";
                }
                
                int rnd = (rng.Next(1, 100) >= 50) ? 1 : 0;
                mimik += $"{yesAndNo[rnd]}\n";

                rnd = (rng.Next(1, 100) >= 50) ? 1 : 0;
                string tryAgain = yesAndNo[rnd];
                mimik += $"{tryAgain}\n";

                if (tryAgain == "ДА")
                {
                    testIteration--;
                }
            }

            Console.WriteLine(mimik);

            // Начало теста
            Console.SetIn(new StringReader(mimik));

            for (int testIteration = 0; testIteration < iterationsCount; testIteration++)
            {
                GeniusIdiot geniusIdiot = new GeniusIdiot(names[rng.Next(0, 4)], questions, answers);

                // Проводим тестирование
                geniusIdiot.StartRepetitiveTest();
            }

            Console.SetIn(originalInput);

            PrintTestResultsToConsole();

            // Конец теста
        }
    }
}
