using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BFPL.STEPIK.Basic_Plus_Sintax
{
    public class Fraction
    {

        int Numerator;
        int Denominator;

        public Fraction(int numerator, int denominator = 1)
        {
            Numerator = numerator;
            Denominator = denominator;
            IsZeroDenominator();
            MinusAndMinus();
            ToLower();
        }

        public void IsZeroDenominator()
        {
            if (Denominator == 0)
            {
                throw new ArithmeticException("Denominator can be ZERO, but computer can't calculate INFINITY.");
            }
        }

        public void MinusAndMinus()
        {
            if (Denominator < 0 && Numerator < 0)
            {
                Numerator *= -1;
                Denominator *= -1;
            }
        }

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
