

namespace BFPL
{
    public class BFPL_StepikCharp_MainMenu
    {
        //Вывод в консоль меню BFPL
        private void PrintBFPL_StepikCharp_MainMenu()
        {
            Console.WriteLine
                (
                "\nBFPL StepikCharp branch main menu:\n" +
                "\tChapter 1: HelloWorld!\n" +
                "\t\tItem 1 - HelloWorld!\n" +
                "\tclear - Clear console\n" +
                "\tmenu - Show BFPL main menu\n" +
                "\texit - Exit and close BFPL\n" +
                "Write chapter and item separated by a space or use keywords:\n"
                );
        }

        //Обработчик меню BFPL
        public void CallBFPL_StepikCharp_MainMenu()
        {
            Console.WriteLine(
                "Welcome to BFPL - Big Fucking Practice Library!\n" +
                "It`s StepikCharp branch."
                );
            PrintBFPL_StepikCharp_MainMenu();

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
                        PrintBFPL_StepikCharp_MainMenu();
                    }
                    else if (shosenPath == "clear")
                    {
                        Console.Clear();
                        PrintBFPL_StepikCharp_MainMenu();
                    }
                    else if (shosenPath == "exit")
                    {
                        break;
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
