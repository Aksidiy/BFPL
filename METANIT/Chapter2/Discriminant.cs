namespace BFPL.METANIT.Chapter2
{
    /*
     * Class for test basic math and if else sintax
     */
    public class DiscriminantClass
    {

        private double coeffA, coeffB, coeffC;
        private double D;
        private object[] Result;

        public DiscriminantClass(double A = 1,
                                 double B = 2,
                                 double C = 1)
        { SetCoeff(A, B, C); CalculateD(); SolveEquation(); }

        public Array GetResult() => Result;

        private void SetCoeff(double A = 1, double B = 2, double C = 1) { coeffA = A; coeffB = B; coeffC = C; }

        private void CalculateD() { D = (coeffB * coeffB) - 4 * coeffA * coeffC; }

        private void SolveEquation()
        {
            if (D < 0)
            {
                Result = new object[] { "Equation has no real roots." };
            }
            else if (D == 0)
            {
                Result = new object[] { (double)(-coeffB / (2 * coeffA)) };
            }
            else
            {
                double x1, x2;
                x1 = (double)((-coeffB + Math.Sqrt(D)) / (2 * coeffA));
                x2 = (double)((-coeffB - Math.Sqrt(D)) / (2 * coeffA));
                Result = new object[] { x1, x2 };
            }
        }

        private void ShowCurentData()
        {
            CalculateD();
            Console.WriteLine($"Curent data: A = {coeffA} B = {coeffB} C = {coeffC} D = {D}\n");
        }

        private void ShowResult() 
        {
            if (Result.Length == 2)
            {
                Console.WriteLine($"X1 = {Result[0]} X2 = {Result[1]}");
            }
            else if (Result[0] is string)
            {
                Console.WriteLine(Result[0]);
            }
            else
            {
                Console.WriteLine($"X1 = X2 = {Result[0]}");
            }
        } 

        private void UserInputCoeff()
        {
            Console.WriteLine("Input coefficients separated by a space:");
            string[] CoeffABC = Console.ReadLine().Replace(".", ",").Split(" ");
            SetCoeff(
                Convert.ToDouble(CoeffABC[0]),
                Convert.ToDouble(CoeffABC[1]),
                Convert.ToDouble(CoeffABC[2])
                );
        }

        private void PrintDiscriminantMenu()
        {
            Console.WriteLine
                (
                "\nDiscriminant menu:\n" +
                "\tset   - Set coefficients (by default A = 1, B = 2, C = 1)\n" +
                "\tcalc  - Calculate discriminant\n" +
                "\tsolve - Solve equation\n" +
                "\tshow  - Show curent data\n" +
                "\tmenu  - Show discriminant menu\n" +
                "\texit  - Exit to main menu\n" +
                "Write keyword:\n"
                );
        }
        public void DiscriminantMenu()
        {
            PrintDiscriminantMenu();

            while (true)
            {
                try
                {
                    string shosenPath = Console.ReadLine().ToLower();

                    if (shosenPath == "set")
                    {
                        UserInputCoeff();
                        Console.WriteLine($"Sucesessful: A = {coeffA} B = {coeffB} C = {coeffC}\n");
                    }
                    else if (shosenPath == "calc")
                    {
                        CalculateD();
                        Console.WriteLine($"Sucesessful: D = {D}\n");
                    }
                    else if (shosenPath == "solve")
                    {
                        SolveEquation();
                        Console.WriteLine($"Sucesessful: A = {coeffA} B = {coeffB} C = {coeffC} D = {D}\n");
                        ShowResult();
                    }
                    else if (shosenPath == "show")
                    {
                        ShowCurentData();
                    }
                    else if (shosenPath == "menu")
                    {
                        PrintDiscriminantMenu();
                    }
                    else if (shosenPath == "exit")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Path: '{shosenPath}' do not exist.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        "Всё пропало, Шеф. ВСЁ ПРОПАЛО!\n" +
                        $"Exception: {ex}");
                }
            }
        }
    }
}
