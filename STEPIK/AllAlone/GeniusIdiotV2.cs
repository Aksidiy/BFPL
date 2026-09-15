using System;
using static BFPL.STEPIK.AllAlone.GeniusIdiotV2;

namespace BFPL.STEPIK.AllAlone
{
    public class GeniusIdiotV2
    {
        // TODO: Запросить критику документирования кода.
        /// <summary>
        /// Class for storing question-answer pair.<br/> Класс для хранения пары вопрос-ответ.
        /// </summary>
        public class Question
        {
            /// <summary>
            /// Text of question.<br/> Текст вопроса.
            /// </summary>
            public required string Text { get; set; }
            /// <summary>
            /// Answer to question.<br/> Ответ на вопрос.
            /// </summary>
            public required string Answer { get; set; }
            /// <summary>
            /// Case sensitivity of answer.<br/> Регистрозависимость ответа.
            /// </summary>
            public required bool СaseSensitivity { get; set; }

            /// <param name="text">
            /// Text of question.<br/> Текст вопроса.
            /// </param>
            /// <param name="answer">
            /// Answer to question.<br/> Ответ на вопрос.
            /// </param>
            /// <param name="caseSensitivity">
            /// Case sensitivity of answer.<br/> Регистрозависимость ответа.
            /// </param>
            public Question(string text, string answer, bool caseSensitivity = false)
            {
                if (text == "" || text.IsWhiteSpace())
                {
                    throw new ArgumentException("Вопрос не может быть пустым.");
                }
                if (answer == "" || answer.IsWhiteSpace())
                {
                    throw new ArgumentException("ответ не может быть пустым.");
                }

                Text = text;
                Answer = answer;
                СaseSensitivity = caseSensitivity;
            }

            /// <summary>
            /// Comparing answers.<br/> Сравнение ответов.
            /// </summary>
            /// <param name="answer">
            /// Suggested answer to the question.<br/> Предложенный ответ на вопрос.
            /// </param>
            /// <returns>
            /// If <paramref name="answer"/> is equals to <see cref="Answer"/> with current value of <see cref="СaseSensitivity"/>
            /// then returns <see langword="true"/>, else returns <see langword="false"/>.<br/>
            /// Если <paramref name="answer"/> равен <see cref="Answer"/> в соответствии со значением <see cref="СaseSensitivity"/>
            /// то вернёт <see langword="true"/> иначе вернёт <see langword="false"/>.
            /// </returns>
            public bool IsCorrectAnswer(string answer)
            {
                if (СaseSensitivity)
                {
                    return answer.Equals(Answer);
                }
                else
                {
                    return answer.Equals(Answer, StringComparison.CurrentCultureIgnoreCase);
                }
            }
        }

        /// <summary>
        /// Class for storing list of question-answer pairs.<br/> Класс для хранения списка пар вопрос-ответ.
        /// </summary>
        public class QuestionsStorage
        {
            /// <summary>
            /// List of <see cref="Question"/>.<br/> Список <see cref="Question"/>.
            /// </summary>
            public List<Question> Questions { get; set; } = new List<Question>();

            /// <summary>
            /// Shuffles questions by using Fisher-Yates shuffle method.<br/> Перемешивает вопросы методом тасования Фишера–Йетса.
            /// </summary>
            public void ShuffleQuestions()
            {
                Shuffle(Questions);
            }

            /// <summary>
            /// Fisher-Yates shuffle method.<br/> Тасование Фишера–Йетса.
            /// </summary>
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
        }

        /// <summary>
        /// Class for storing user information and test results.<br/> 
        /// Класс для хранения сведений о пользователе и результатов прохождения теста.
        /// </summary>
        public class User
        {
            /// <summary>
            /// Users name.<br/> Имя полоьзователя.
            /// </summary>
            public required string Name { get; set; }
        }

        public class UsersResultStorage
        {
            /// <summary>
            /// Structure for storing a pair of test result - timestamp.<br/>
            /// Структура для хранения пары результат теста - временная отметка.
            /// </summary>
            /// <param name="user">
            /// Test subject.<br/> Испытуемый.
            /// </param>
            /// <param name="testResult">
            /// Test result.<br/> Результат прохождения теста.
            /// </param>
            /// <param name="dateTime">
            /// Time stamp.<br/> Временная отметка.
            /// </param>
            readonly public struct TestResultWithDate(User user, int testResult, string dateTime)
            {
                /// <summary>
                /// Test result.<br/> Результат прохождения теста.
                /// </summary>
                readonly User user = user;
                /// <summary>
                /// Test result.<br/> Результат прохождения теста.
                /// </summary>
                readonly int testResult = testResult;
                /// <summary>
                /// Time stamp.<br/> Временная отметка.
                /// </summary>
                readonly string dateTime = dateTime;
            }

            /// <summary>
            /// List for storing test results with a time stamp.<br/>
            /// Список для хранения результатов прохождения теста с временной отметкой.
            /// </summary>
            public List<TestResultWithDate> UsersTestResults { get; private set; } = new List<TestResultWithDate>();

            /// <summary>
            /// Saves the test result with a time stamp to <see cref="UsersTestResults"/>.<br/> 
            /// Сохраняет результат прохождения теста с временной отметкой в <see cref="UsersTestResults"/>.
            /// </summary>
            /// <param name="user">
            /// Test subject.<br/> Испытуемый.
            /// </param>
            /// <param name="testResult">
            /// Test result.<br/> Результат прохождения теста.
            /// </param>
            /// <returns>
            /// Return <see langword="true"/> if succeeded save result to <see cref="UsersTestResults"/><br/>
            /// Return <see langword="false"/> if an error occurs.<br/>
            /// Вернёт <see langword="true"/> если удалось сохранить результат в <see cref="UsersTestResults"/><br/>
            /// Вернёт <see langword="false"/> если возникла ошибка.
            /// </returns>
            public bool SaveTestResult(User user, int testResult)
            {
                try
                {
                    UsersTestResults.Add(new TestResultWithDate(user, testResult, DateTime.Now.ToString("u")));
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return false;
                }
            }

            /// <summary>
            /// Saves the test result with a time stamp to the dictionary.<br/> Сохраняет результат прохождения теста с временной отметкой в словарь.
            /// </summary>
            /// <param name="index">
            /// Test result.<br/> Результат прохождения теста.
            /// </param>
            /// <returns>
            /// Return <see langword="true"/> if succeeded save result to <see cref="UsersTestResults"/><br/>
            /// Return <see langword="false"/> if an error occurs.<br/>
            /// Вернёт <see langword="true"/> если удалось сохранить результат в <see cref="UsersTestResults"/><br/>
            /// Вернёт <see langword="false"/> если возникла ошибка.
            /// </returns>
            public bool DeleteTestResult(int index)
            {
                try
                {
                    UsersTestResults.RemoveAt(index);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return false;
                }
            }
        }

        /// <summary>
        /// Method for testing code.<br/> Метод для тестирования кода.
        /// </summary>
        public static void StartGeniusIdiotV2Test()
        {

        }
    }
}
