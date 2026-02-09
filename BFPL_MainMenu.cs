

namespace BFPL
{
    public class BFPL_MainMenu
    {
        //Вывод в консоль меню BFPL
        public static void PrintBFPL_MainMenu()
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
        public void CallBFPL_MainMenu()
        {
            Console.WriteLine("Welcome to BFPL - Big Fucking Practice Library!\n");
            PrintBFPL_MainMenu();

            //TODO: уточнить костыль ли это (да конечно блять костыль) надо узнать как красиво сделать
            while (true)
            {
                try
                {
                    //Ловим ввод юзверя и на всякий в нижний регистр понижаем
                    string shosenPath = Console.ReadLine().ToLower();

                    //Можно было бы сделать свитч, но он меня раздражает
                    if (shosenPath == "mn")
                    {
                        //Go to METANIT branch
                        Console.WriteLine($"You chose {shosenPath} path.\n");
                        BFPL_METANIT_MainMenu metanitMainMenu = new BFPL_METANIT_MainMenu();
                        metanitMainMenu.CallBFPL_METANIT_MainMenu();
                    }
                    else if (shosenPath == "st")
                    {
                        //Go to STEPIK branch
                        Console.WriteLine($"You chose {shosenPath} path.\n");
                        BFPL_STEPIK_MainMenu stepikMainMenu = new BFPL_STEPIK_MainMenu();
                        stepikMainMenu.CallBFPL_STEPIK_MainMenu();
                    }
                    else if (shosenPath == "mm")
                    {
                        //Call main menu
                        PrintBFPL_MainMenu();
                    }
                    else if (shosenPath == "cl")
                    {
                        //Clear console and call main menu
                        Console.Clear();
                        PrintBFPL_MainMenu();
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
