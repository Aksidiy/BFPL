using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BFPL.STEPIK.Basic_Plus_Sintax
{
    /// <summary>
    /// Класс симулирует простую дробь без отделения целой части.
    /// </summary>
    public class Fraction
    {

        int Numerator;
        int Denominator;

        /// <summary>
        /// Конструктор дроби.
        /// </summary>
        /// <remarks>
        /// Содержит проверку на НОЛЬ в Знаменателе.
        /// Сокращает знак минуса если числитель и знаменатель ОБА отрицательные.
        /// Сокращает значения дроби до минимально возможных целых чисел.
        /// </remarks>
        /// <name="numerator"> Числитель дроби. </param>
        /// <name="denominator"> Знаменатель дроби. НЕ МОЖЕТ БЫТЬ РАВЕН НУЛЮ!!! </param>
        public Fraction(int numerator, int denominator = 1)
        {
            Numerator = numerator;
            Denominator = denominator;
            IsZeroDenominator();
            MinusAndMinus();
            ToLower();
        }

        /// <summary>
        /// Проверка на 0 в знаменателе.
        /// </summary>
        /// <exception cref="System.ArithmeticException">
        /// Выбрасывается если Знаменатель равен НУЛЮ.
        /// </exception>
        public void IsZeroDenominator()
        {

            if (Denominator == 0)
            {
                throw new ArithmeticException("Denominator can be ZERO, but computer can't calculate INFINITY.");
            }
        }

        /// <summary>
        /// "Минус на минус даёт плюс"
        /// </summary>
        public void MinusAndMinus()
        {
            if (Denominator < 0 && Numerator < 0)
            {
                Numerator *= -1;
                Denominator *= -1;
            }
        }

        /// <summary>
        /// Сокращеет дробь до минимальных возможных числителя и знаменателя.
        /// </summary>
        public void ToLower()
        {
            int min = (Math.Abs(Numerator) <= Math.Abs(Denominator)) ? Math.Abs(Numerator) : Math.Abs(Denominator);
            for (int i = min - 1; min > 0; i--)
            {
                if ((Numerator % i == 0) && (Denominator % i == 0))
                {
                    Numerator /= i;
                    Denominator /= i;
                    return;
                }
            }
        }

        /// <summary>
        /// Печатает дробь в консоль. Если дробь можно свести к целому числу, то печатает его.
        /// </summary>
        public void PrintFraction()
        {
            IsZeroDenominator();
            MinusAndMinus();
            ToLower();
            if (Numerator % Denominator == 0)
            {
                Console.WriteLine($"{Numerator / Denominator}");
            }
            else
            {
                Console.WriteLine($"{Numerator}/{Denominator}");
            }
        }

        public static Fraction operator +(Fraction a, Fraction b)
        {
            int denominator = a.Denominator * b.Denominator;
            int numerator = (a.Numerator * b.Denominator) + (b.Numerator * a.Denominator);
            return new Fraction(numerator, denominator);
        }

        public static Fraction operator -(Fraction a, Fraction b)
        {
            int denominator = a.Denominator * b.Denominator;
            int numerator = (a.Numerator * b.Denominator) - (b.Numerator * a.Denominator);
            return new Fraction(numerator, denominator);
        }

        public static Fraction operator *(Fraction a, Fraction b)
        {
            int denominator = a.Denominator * b.Denominator;
            int numerator = a.Numerator * b.Numerator;
            return new Fraction(numerator, denominator);
        }

        public static Fraction operator /(Fraction a, Fraction b)
        {
            int denominator = a.Denominator * b.Numerator;
            int numerator = a.Numerator * b.Denominator;
            return new Fraction(numerator, denominator);
        }

        public static Fraction operator +(Fraction a, int b)
        {
            Fraction buffB = new Fraction(b * a.Denominator, a.Denominator);
            return (a + buffB);
        }

        public static Fraction operator +(int a, Fraction b)
        {
            Fraction buffA = new Fraction(a * b.Denominator, b.Denominator);
            return (buffA + b);
        }

        public static Fraction operator -(Fraction a, int b)
        {
            Fraction buffB = new Fraction(b * a.Denominator, a.Denominator);
            return (a - buffB);
        }

        public static Fraction operator -(int a, Fraction b)
        {
            Fraction buffA = new Fraction(a * b.Denominator, b.Denominator);
            return (buffA - b);
        }

        public static Fraction operator *(Fraction a, int b)
        {
            Fraction buffB = new Fraction(b * a.Denominator, a.Denominator);
            return (a * buffB);
        }

        public static Fraction operator *(int a, Fraction b)
        {
            Fraction buffA = new Fraction(a * b.Denominator, b.Denominator);
            return (buffA * b);
        }

        public static Fraction operator /(Fraction a, int b)
        {
            Fraction buffB = new Fraction(b * a.Denominator, a.Denominator);
            return (a * buffB);
        }

        public static Fraction operator /(int a, Fraction b)
        {
            Fraction buffA = new Fraction(a * b.Denominator, b.Denominator);
            return (buffA * b);
        }
    }
}
