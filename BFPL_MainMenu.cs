using BFPL.METANIT;
using BFPL.STEPIK;

namespace BFPL
{
    public static class BFPL_MainMenu
    {
        //Вывод в консоль меню BFPL
        public static void PrintMainMenu()
        {
            Console.WriteLine
                (
                "\nBFPL main menu:\n" +
                "\tMN : To METANIT branch.\n" +
                "\tST : To STEPIK branch.\n" +
                "\tCL : Clear console\n" +
                "\tMM : Show BFPL main menu\n" +
                "\tEX : Exit and close BFPL\n" +
                "Write keywords:\n"
                );
        }

        //Обработчик меню BFPL
        public static void CallBFPLMainMenu()
        {
            Console.WriteLine("Welcome to BFPL - Big Fucking Practice Library!\n");
            PrintMainMenu();

            while (true)
            {
                try
                {
                    //Ловим ввод юзверя и на всякий в нижний регистр понижаем
                    string shosenPath = Console.ReadLine().ToLower();

                    //Тут сделал через ифы, варианты со switch глубже
                    if (shosenPath == "mn")
                    {
                        //Go to METANIT branch
                        Console.WriteLine($"You chose {shosenPath} path.\n");
                        METANIT.MainMenu.CallMETANITMainMenu();
                    }
                    else if (shosenPath == "st")
                    {
                        //Go to STEPIK branch
                        Console.WriteLine($"You chose {shosenPath} path.\n");
                        STEPIK.MainMenu.CallSTEPIKMainMenu();
                    }
                    else if (shosenPath == "mm")
                    {
                        //Call main menu
                        PrintMainMenu();
                    }
                    else if (shosenPath == "cl")
                    {
                        //Clear console and call main menu
                        Console.Clear();
                        PrintMainMenu();
                    }
                    else if (shosenPath == "ex")
                    {
                        //Close BFPL
                        break;
                    }
                    else
                    {
                        //Catch wrong UZWER input
                        Console.WriteLine($"Path: [{shosenPath}] do not exist.\n");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        "Всё пропало, Шеф. ВСЁ ПРОПАЛО!\n" +
                        $"Exception from BFPL:\n {ex}\n");
                }
            }
        }
    }
}
