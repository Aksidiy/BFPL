using BFPL.Chapter1;
using BFPL.Chapter2;
using BFPL.DanielsTasks;

namespace BFPL
{
    public class BFPLMainMenu
    {
        //Вывод в консоль меню BFPL
        private void PrintBFPLMainMenu()
        {
            Console.WriteLine
                (
                "\nBFPL main menu:\n" +
                "\tChapter 1: HelloWorld!\n" +
                "\t\tItem 1 - HelloWorld!\n" +
                "\tChapter 2: Basic sintax of C#\n" +
                "\t\tItem 1 - Discriminant (basic math and if else)\n" +
                "\t\tItem 2 - Jordan-Gauss method (basic arrays and cycles)\n" +
                "\tChapter 3: Basic about Classes in C#\n" +
                "\t\tItem 1 - List of Money\n" +
                "\tDD - Tasks from DeadDaniel:\n" +
                "\t\tTask 1 - ComputersRating\n" +
                "\t\tTask 2 - ComputersRatingVer2\n" +
                "\tclear - Clear console\n" +
                "\tmenu - Show BFPL main menu\n" +
                "\texit - Exit and close BFPL\n" +
                "Write chapter and item separated by a space or use keywords:\n"
                );
        }

        //Обработчик меню BFPL
        public void CallBFPLMainMenu()
        {
            Console.WriteLine("Welcome to BFPL - Big Fucking Practice Library!\n");
            PrintBFPLMainMenu();

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
                        HelloWorld helloWorld = new HelloWorld();
                        helloWorld.PrintHelloWorld();
                    }
                    else if (shosenPath == "2 1")
                    {
                        Console.WriteLine($"You chose {shosenPath} path.\n");
                        DiscriminantClass discriminantClass = new DiscriminantClass();
                        discriminantClass.DiscriminantMenu();
                    }
                    else if (shosenPath == "dd 1")
                    {
                        Console.WriteLine($"You chose {shosenPath} path.\n");

                        Console.WriteLine("Try Total 0 Correct 0:");
                        Console.WriteLine($"Result: {ComputersRating.CalculateCompRating(0, 0)}");

                        Console.WriteLine("Try Total 666 Correct 666:");
                        Console.WriteLine($"Result: {ComputersRating.CalculateCompRating(666, 666)}");

                        Console.WriteLine("Try Total 666 Correct 333:");
                        Console.WriteLine($"Result: {ComputersRating.CalculateCompRating(666, 333)}");

                        Console.WriteLine("Try Total 666 Correct 66:");
                        Console.WriteLine($"Result: {ComputersRating.CalculateCompRating(666, 66)}");

                        Console.WriteLine("Try Total 66 Correct 666:");
                        Console.WriteLine($"Result: {ComputersRating.CalculateCompRating(66, 666)}");
                    }
                    else if (shosenPath == "dd 2")
                    {
                        Console.WriteLine($"You chose {shosenPath} path.\n");

                        ComputersRatingVer2 CRV2 = new ComputersRatingVer2();
                        Console.WriteLine("Try to parse clients from JSON:\n");
                        Console.WriteLine("Result:\n");
                        CRV2.ParseClientsFromJSON();
                        for (int i = 0; i < CRV2.Clients.Count; i++)
                        {
                            Console.WriteLine($"[{CRV2.Clients[i].clientId}]\t");
                        }
                        Console.WriteLine("Try to parse computers from JSON:\n");
                        Console.WriteLine("Result:\n");
                        CRV2.ParseComputersFromJSON();
                        for (int i = 0; i < CRV2.Computers.Count; i++)
                        {
                            Console.WriteLine(
                                $"[{CRV2.Computers[i].clientId}\t" +
                                $"{CRV2.Computers[i].computerPosteName}\t" +
                                $"{CRV2.Computers[i].dataType}\t" +
                                $"{CRV2.Computers[i].dataValue}]\n"
                                );
                        }

                        Console.WriteLine("Try to calculate computers rating by client:\n");
                        Console.WriteLine("Result:\n");
                        CRV2.CalculateComputersRatingByClient();
                        for (int i = 0; i < CRV2.Clients.Count; i++)
                        {
                            Console.WriteLine(
                                $"[{CRV2.Clients[i].clientId}\t" +
                                $"{CRV2.Clients[i].computersSpeedRating}\t" +
                                $"{CRV2.Clients[i].computersScanSpeedRating}\t" +
                                $"{CRV2.Clients[i].computersInternetSpeedRating}\t" +
                                $"{CRV2.Clients[i].computersRecognitionSpeedRating}]\n"
                            );
                        }
                        Console.WriteLine("Try to create client computers rating JSON file:\n");
                        Console.WriteLine("Result:\n");
                        string filePath = CRV2.CrateRatigJSONEFile();
                        Console.WriteLine($"Client computers rating JSON file path:\n \t{filePath}\n");
                    }
                    else if (shosenPath == "menu")
                    {
                        PrintBFPLMainMenu();
                    }
                    else if (shosenPath == "clear")
                    {
                        Console.Clear();
                        PrintBFPLMainMenu();
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
