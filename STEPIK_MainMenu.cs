using BFPL.STEPIK.Basic_Plus_Sintax;
using BFPL.STEPIK.Basic_Sintax;


namespace BFPL
{
    public class BFPL_STEPIK_MainMenu
    {
        //Вывод в консоль меню BFPL
        public static void PrintBFPL_STEPIK_MainMenu()
        {
            Console.WriteLine
                (
                "\nBFPL STEPIC branch main menu:\n" +
                "\tChapter 1: Basic Syntax.\n" +
                "\t\tItem 1 - XesAndZeros\n" +
                "\t\tItem 2 - PRGSW\n" +
                "\tChapter 2: Basic Syntax.\n" +
                "\t\tItem 1 - School Simulation\n" +
                "\t\tItem 2 - Сommon Fractions Simulation\n" +
                "\t\tItem 3 - Online Store Simulation\n" +
                "\tclear - Clear console\n" +
                "\tmenu - Show STEPIC branch main menu\n" +
                "\treturn - Return to BFPL main menu\n" +
                "Write chapter and item separated by a space or use keywords:\n"
                );
        }

        //Обработчик меню BFPL
        public void CallBFPL_STEPIK_MainMenu()
        {
            Console.WriteLine(
                "Welcome to BFPL - Big Fucking Practice Library!\n" +
                "It`s STEPIC branch."
                );
            PrintBFPL_STEPIK_MainMenu();

            //TODO: уточнить костыль ли это (да конечно блять костыль) надо узнать как красиво сделать
            while (true)
            {
                //Ловим ввод юзверя и на всякий в нижний регистр понижаем
                //Формат ввода: [chapter number][space][item number] или [ключевое слово]
                string shosenPath = Console.ReadLine().ToLower();

                //Можно было бы сделать свитч, но он меня раздражает
                if (shosenPath == "1 1")
                {
                    Console.WriteLine($"You chose {shosenPath} path.\n");
                    XesAndZeros.GoXesAndZeros();
                    PrintBFPL_STEPIK_MainMenu();
                }
                else if (shosenPath == "1 2")
                {
                    Console.WriteLine($"You chose {shosenPath} path.\n");
                    string mathExpBeforePRGSW = $"( ( - 2 + 3 ) * ( 4 - 1 ) ) ^ ( 2 + ( 1 + 1 ) )";
                    string mathExpAfterPRGSW = $"2 - 3 + 4 1 - * 2 1 1 + + ^";
                    Console.WriteLine(mathExpBeforePRGSW);
                    Console.WriteLine(PRGSW.polishReverseGripSteeringWheel(mathExpBeforePRGSW));
                    Console.WriteLine(mathExpAfterPRGSW);
                    Console.WriteLine(PRGSW.undoPolishReverseGripSteeringWheel(mathExpAfterPRGSW));
                    Console.WriteLine(mathExpBeforePRGSW);
                    Console.WriteLine(PRGSW.calculatePolishReverseGripSteeringWheel(mathExpBeforePRGSW, false));
                    Console.WriteLine(mathExpAfterPRGSW);
                    Console.WriteLine(PRGSW.calculatePolishReverseGripSteeringWheel(mathExpAfterPRGSW, true));
                    PrintBFPL_STEPIK_MainMenu();
                }
                else if (shosenPath == "2 1")
                {
                    Console.WriteLine($"You chose {shosenPath} path.\n");

                    School.StartSchoolSimulation();
                }
                else if (shosenPath == "2 2")
                {
                    Fraction first = new Fraction(3, 9);
                    first.PrintFraction();
                    Fraction second = new Fraction(63, 21);
                    second.PrintFraction();
                    int third = 5;


                    (first + second).PrintFraction();
                    (first - second).PrintFraction();
                    (first * second).PrintFraction();
                    (first / second).PrintFraction();

                    (first + third).PrintFraction();
                    (first - third).PrintFraction();
                    (first * third).PrintFraction();
                    (first / third).PrintFraction();

                    (third + first).PrintFraction();
                    (third - first).PrintFraction();
                    (third * first).PrintFraction();
                    (third / first).PrintFraction();

                    Fraction last = new Fraction(3, 0);
                }
                else if (shosenPath == "2 3")
                {
                    Console.WriteLine($"You chose {shosenPath} path.\n");

                    
                }
                else if (shosenPath == "menu")
                {
                    PrintBFPL_STEPIK_MainMenu();
                }
                else if (shosenPath == "clear")
                {
                    Console.Clear();
                    PrintBFPL_STEPIK_MainMenu();
                }
                else if (shosenPath == "return")
                {
                    //Go to BFPL main menu
                    BFPL_MainMenu.PrintBFPL_MainMenu();
                    return;
                }
                else
                {
                    Console.WriteLine($"Path: [{shosenPath}] do not exist.\n");
                }
            }
        }
    }
}
