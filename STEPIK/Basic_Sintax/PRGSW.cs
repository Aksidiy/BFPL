using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace BFPL.STEPIK.Basic_Sintax
{
    static public class PRGSW
    {
        static public readonly Dictionary<string, (int prec, bool rightAssoc, Func<double, double, double> calc)> operators = 
            new Dictionary<string, (int prec, bool rightAssoc, Func<double, double, double> calc)>()
        {
            { "+", (1, false, (a, b) => a + b) },
            { "-", (1, false, (a, b) => a - b) },
            { "*", (2, false, (a, b) => a * b) },
            { "/", (2, false, (a, b) => a / b) },
            { "^", (3, true,  Math.Pow) }
        };

        static void TrowExeption(int index, string item, string massage = "418 I am a Teapot!", [CallerMemberName] string method = "Unknown")
        {
            throw new Exception(
                $"Exception from {method}:\n" +
                $"Ошибка ввода на элементе: index: [ {index} ] value: [ {item} ]\n" +
                $"{massage}"
                );
        }

        static public double calculatePolishReverseGripSteeringWheel(string mathExpression, bool isPRGSW = true) 
        {
            double result = 0;

            if (!isPRGSW) 
            {
                mathExpression = polishReverseGripSteeringWheel(mathExpression);
            }

            List<string> mathExpAfterPRGSW = new List<string>(mathExpression.Split(" "));

            Stack<double> calculate = new Stack<double>();

            for (int i = 0; i < mathExpAfterPRGSW.Count; i++)
            {
                string buff = mathExpAfterPRGSW[i];

                if (Char.IsNumber(buff[buff.Length - 1]))
                {
                    calculate.Push(Convert.ToDouble(buff));
                    continue;
                }

                if (operators.ContainsKey(buff) && calculate.Count != 1)
                {
                    double rightExp = calculate.Pop();
                    double leftExp = calculate.Pop();

                    if (buff == "/" && rightExp == 0)
                    {
                        TrowExeption(i, buff, "Нельзя вместить бесконечность в памяти компьютера.");
                    }

                    calculate.Push(operators[buff].calc(leftExp, rightExp));
                }
                else if (buff == "-" || buff == "+")
                {
                    calculate.Push(operators[buff].calc(0, calculate.Pop()));
                }
                else
                {
                    TrowExeption(i, buff, "Ошибочный или необрабатываеммый символ в выражении.");
                }
            }
            result = calculate.Pop();

            return result;
        }

        static public string undoPolishReverseGripSteeringWheel(string mathExpression)
        {
            string result = "";

            // лексим входящую строку
            List<string> mathExpAfterPRGSW = mathBasicLexer(mathExpression);

            Stack<string> expretion = new Stack<string>();
            Stack<int> priority = new Stack<int>();

            for(int i = 0; i < mathExpAfterPRGSW.Count; i++)
            {
                string buff = mathExpAfterPRGSW[i];

                if ((Char.IsNumber(buff[buff.Length - 1])))
                {
                    expretion.Push(buff);
                    priority.Push(int.MaxValue);
                    continue;
                }

                if (operators.ContainsKey(buff) && expretion.Count != 1)
                {
                    string rightExp = expretion.Pop();
                    int rihgtPrior = priority.Pop();

                    string leftExp = expretion.Pop();
                    int leftPrior = priority.Pop();

                    int opPrior = operators[buff].prec;
                    bool rightAssocOp = operators[buff].rightAssoc;

                    if (leftPrior < opPrior || (leftPrior == opPrior && rightAssocOp == true))
                    {
                        leftExp = "( " + leftExp + " )";
                    }

                    if (rihgtPrior < opPrior || (rihgtPrior == opPrior && rightAssocOp == false))
                    {
                        rightExp = "( " + rightExp + " )";
                    }

                    string newExpr = leftExp + " " + buff + " " + rightExp;

                    expretion.Push(newExpr);
                    priority.Push(opPrior);
                }
                // 6 -
                else if (buff == "-" || buff == "+")
                {
                    string rightExp = expretion.Pop();
                    int rihgtPrior = priority.Pop();

                    int opPrior = operators[buff].prec;
                    bool rightAssocOp = operators[buff].rightAssoc;

                    string newExpr = "";
                    if (buff == "-")
                        newExpr = "( " + buff + " " + rightExp + " )";
                    else
                        newExpr = rightExp;

                    expretion.Push(newExpr);
                    priority.Push(opPrior);
                }
                else
                {
                    TrowExeption(i, buff, "Ошибочный или необрабатываеммый символ в выражении.");
                }
            }
            result = expretion.Pop();

            return result;
        }

        static public string polishReverseGripSteeringWheel(string mathExpression)
        {
            // 2 + 4 / 5 * ( 5 - 3 ) ^ 5 ^ 4
            // 2 4 5 / 5 3 - 5 ^ 4 ^ * +

            string result = "";

            // лексим входящую строку
            List<string> mathExpBeforePRGSW = mathBasicLexer(mathExpression);

            Queue<string> mathExpAfterPRGSW = new Queue<string>();
            Stack<string> operatorsStack = new Stack<string>();

            // Если что-то упадёт, то это явно ошибка ввода
            // Падать не будет, но и вернёт дырку от бублика
            for (int i = 0; i < mathExpBeforePRGSW.Count; i++)
            {
                string buff = mathExpBeforePRGSW[i];

                if ((Char.IsNumber(buff[buff.Length - 1])))
                {
                    mathExpAfterPRGSW.Enqueue(buff);
                }
                else if (buff == "(")
                {
                    operatorsStack.Push(buff);
                }
                else if (buff == ")")
                {
                    while (operatorsStack.Peek() != "(")
                    {
                        mathExpAfterPRGSW.Enqueue(operatorsStack.Pop());
                    }
                    operatorsStack.Pop();
                }
                else if (operators.ContainsKey(buff))
                {
                    if (
                        operatorsStack.Count == 0 ||
                        operatorsStack.Peek() == "(" ||
                        operators[operatorsStack.Peek()].prec < operators[buff].prec
                        )
                    {
                        operatorsStack.Push(buff);
                    }
                    else if (operators[operatorsStack.Peek()].prec >= operators[buff].prec)
                    {
                        while (
                            operatorsStack.Count > 0 &&
                            operatorsStack.Peek() != "(" &&
                            operators[operatorsStack.Peek()].prec >= operators[buff].prec
                            )
                        {
                            mathExpAfterPRGSW.Enqueue(operatorsStack.Pop());
                        }
                        operatorsStack.Push(buff);
                    }
                }
                else
                {
                    TrowExeption(i, buff, "Ошибочный или необрабатываеммый символ в выражении.");
                }
            }

            while (operatorsStack.Count != 0)
            {
                if (operatorsStack.Peek() == "(")
                {
                    TrowExeption(operatorsStack.Count, operatorsStack.Peek(), "Ошибочный или необрабатываеммый символ в выражении.");
                }
                mathExpAfterPRGSW.Enqueue(operatorsStack.Pop());
            }

            result = String.Join(" ", mathExpAfterPRGSW);

            return result;
        }

        static public List<string> mathBasicLexer(string mathExpression)
        {
            List<string> mathExpressionList = new List<string>();

            string buffer = "";
            foreach (char c in mathExpression)
            {
                //2.85+4,78/5*(5-3)^5^4

                //0123456789 '.' ','
                if ('0' <= c && c <= '9' || c == '.' || c == ',')
                {
                    buffer += c;
                }
                // any simbol
                else
                {
                    //write buffer
                    if (buffer != "")
                    {
                        mathExpressionList.Add(buffer);
                        buffer = "";
                    }
                    // ( ) + - * / ^
                    if (c == '(' || c == ')' || c == '+' || c == '-' || c == '*' || c == '/' || c == '^')
                    {
                        mathExpressionList.Add("" + c);
                    }
                }
            }
            //end of string
            if (buffer != "")
            {
                mathExpressionList.Add(buffer);
                buffer = "";
            }

            // переворачиватель минусов
            if (mathExpressionList.Count > 1)
            {
                for (int i = 0; i < mathExpressionList.Count - 1; i++)
                {
                    if (mathExpressionList[i] == "-" && (Char.IsNumber(mathExpressionList[i + 1][mathExpressionList[i + 1].Length - 1])))
                    {
                        if (i - 1 < 0 || mathExpressionList[i - 1] == "(" || operators.ContainsKey(mathExpressionList[i - 1]))
                        {
                            mathExpressionList[i] = mathExpressionList[i] + mathExpressionList[i + 1];
                            mathExpressionList.RemoveAt(i + 1);
                        }
                    }
                }
            }

            return mathExpressionList;
        }
    }
}
