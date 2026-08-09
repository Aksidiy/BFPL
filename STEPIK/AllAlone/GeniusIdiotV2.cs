using System;

namespace BFPL.STEPIK.AllAlone
{
    public class GeniusIdiotV2
    {
        // TODO: Запросить критику документирования кода.
        /// <summary>
        /// Class for storing question-answer pairs.<br/> Класс для хранения пары вопрос-ответ.
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
    }
}
