

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
                "\tChapter 1: HelloWorld!\n" +
                "\t\tItem 1 - HelloWorld!\n" +
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
                try
                {
                    //Ловим ввод юзверя и на всякий в нижний регистр понижаем
                    //Формат ввода: [chapter number][space][item number] или [ключевое слово]
                    string shosenPath = Console.ReadLine().ToLower();

                    //Можно было бы сделать свитч, но он меня раздражает
                    if (shosenPath == "1 1")
                    {
                        Console.WriteLine($"You chose {shosenPath} path.\n");
                        //PATH CODE HERE!!!
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
                catch (Exception ex)
                {
                    Console.WriteLine(
                        "Всё пропало, Шеф. ВСЁ ПРОПАЛО!\n" +
                        $"Exception: {ex}\n");
                }
            }
        }
    }
}
