using System;
using System.Collections.Generic;
using System.Text;

namespace BFPL.STEPIK.Basic_Sintax
{
    internal class MySqrt
    {
        static public double MySqrtMethod(double X = 1867680290161, double accuracy = 0.00001)
        {
            // Тестовые данные:
            // Входящий X = 1867680290161
            // Результат Xn = 136631

            // Тут я предположил следующее:
            // Ближайшим к искомому квадратному корню будет число имеющее вдвое меньше разрядов знаков чем X.
            // Так же я заметил, что первое значение Xn приблизительно на треть больше финального результата.
            // Потому я решил умножить первое Xn на 0.666, думаю правильнее было бы на 0.7, но мне просто нравится число 666.
            // double Xn = (X/Math.Pow(10, (int)((Math.Log10(X) + 1) / 2)))*0.666;
            // Оказалось, что это излишне и стоит просто разделить X на 2

            if (X < 0)
                throw new ArgumentException("Negative number");

            if (X == 0)
                return 0;

            double Xn1 = X / 2;
            double Xn, Delta;
            int count = 0;
            do
            {
                count++;
                Xn = Xn1;
                Xn1 = (Xn + X / Xn) / 2;
                Delta = Math.Abs(Xn1 - Xn);
                Console.WriteLine($"Iter {count} : Xn = {Xn} | Xn1 = {Xn1} | Delta = {Delta}");
                Xn = Xn1;
            }//Нейронка постучала по рукам за Math.Abs((Xn1 * Xn1) - X), так что:
            while (Delta > accuracy);
            Console.WriteLine($"Result: {Xn}");
            return Xn;
        }
    }
}
