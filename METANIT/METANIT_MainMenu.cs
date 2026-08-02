using BFPL.METANIT.Chapter1;
using BFPL.METANIT.Chapter2;
using BFPL.METANIT.DanielsTasks;

namespace BFPL.METANIT
{
    public class MainMenu
    {
        //Вывод в консоль меню BFPL
        public static void PrintMETANITMainMenu()
        {
            Console.WriteLine
                (
                "\nBFPL METANIT branch main menu:\n" +
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
                "\tmenu - Show METANIT branch main menu\n" +
                "\treturn - Return to BFPL main menu\n" +
                "Write numbers of chapter and item separated by a space or use keywords:\n"
                );
        }

        //Обработчик меню BFPL
        public static void CallMETANITMainMenu()
        {
            Console.WriteLine("Welcome to METANIT branch of BFPL!\n");
            PrintMETANITMainMenu();

            while (true)
            {
                //Ловим ввод юзверя и на всякий в нижний регистр понижаем
                //Формат ввода: [chapter number][space][item number] или [ключевое слово]
                string shosenPath = Console.ReadLine().ToLower();

                Console.WriteLine($"You chose {shosenPath} path.\n");

                switch (shosenPath)
                {
                    case "1 1":
                        HelloWorldPath();
                        break;
                    case "2 1":
                        DiscriminantPath();
                        break;
                    case "dd 1":
                        ComputersRatingV1Path();
                        break;
                    case "dd 2":
                        ComputersRatingV2Path();
                        break;
                    case "menu":
                        PrintMETANITMainMenu();
                        break;
                    case "clear":
                        Console.Clear();
                        PrintMETANITMainMenu();
                        break;
                    case "return":
                        //Возврат в BFPL Main menu
                        BFPL_MainMenu.PrintMainMenu();
                        return;
                    default:
                        Console.WriteLine($"Path: [{shosenPath}] do not exist.\n");
                        break;
                }
            }
        }

        //Методы для обработки пунктов меню
        static void HelloWorldPath()
        {
            HelloWorld.PrintHelloWorld();
        }

        static void DiscriminantPath()
        {
            DiscriminantClass discriminantClass = new DiscriminantClass();
            discriminantClass.DiscriminantMenu();
        }

        static void ComputersRatingV1Path()
        {
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

        static void ComputersRatingV2Path()
        {
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
    }
}
