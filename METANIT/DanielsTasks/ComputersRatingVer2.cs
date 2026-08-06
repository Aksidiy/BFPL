using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BFPL.METANIT.DanielsTasks
{
    internal class ComputersRatingVer2
    {
        //Мне ой как лень марать руки об ручной ввод пути, вот до GUI дойду и буду окошком запрашивать
        public const string _TEST_CLIENTS_JSON_PATH = "C:\\CodeProjects\\CSHARP\\Aksidiy\\BFPL\\TESTDATA\\ComputersRatingVer2\\Clients.json";
        public const string _TEST_COMPUTERS_JSON_PATH = "C:\\CodeProjects\\CSHARP\\Aksidiy\\BFPL\\TESTDATA\\ComputersRatingVer2\\Computers.json";
        public const string _TEST_RATING_JSON_PATH = "C:\\CodeProjects\\CSHARP\\Aksidiy\\BFPL\\TESTDATA\\ComputersRatingVer2\\Rating.json";

        /* ДАНО:
            * У каждого Клиента много ПК, каждый ПК имеет 4-ре параметра:
            * скорость ПК, скорость сканирования, скорость интернета и время распознавания
            * Данные собираются за период и делается среднее за период для каждого ПК Клиента
            * Есть мин требования correctComputers соответствено:
            * 13 - скорость компьютера <= 100 мс (computersSpeedRating)
            * 14 - скорость сканирования <= 10000 мс (computersScanSpeedRating)
            * 15 - скорость инета <= 100 мс (computersInternetSpeedRating)
            * 16 - время получения распознания <= 3000 мс (computersRecognitionSpeedRating)
            * Расчет рейтинга для каждого клиента по каждому из параметров отдельно
        */

        //ЭТАП 1 - парсинг джисона

        /*
         Формат входящего джисона Клиентов
        {
            "ClientsData": 
                [
                    {
                        "ClientId" : 12345,
                        "Organization" : "Name of organization",
                    },
                ]
        }
        */

        public class Client
        {
            [Required(ErrorMessage = "Where is ClientID?")]
            public string clientId;

            public double computersSpeedRating = 0;
            public double computersScanSpeedRating = 0;
            public double computersInternetSpeedRating = 0;
            public double computersRecognitionSpeedRating = 0;

            public Client(string clientId)
            {
                this.clientId = clientId;
            }
        }

        public List<Client> Clients = new List<Client>();

        public void ParseClientsFromJSON(string pathToClientsJSONFile = _TEST_CLIENTS_JSON_PATH) 
        {
            //Открываем файл
            StreamReader streamReader = new StreamReader(pathToClientsJSONFile);

            string? curentLineOfJSON;

            while (true)
            {
                //Читаем строку файла
                curentLineOfJSON = streamReader.ReadLine();

                //Если конец файла закругляемся
                if (curentLineOfJSON == null)
                {
                    break;
                }
                //Если нашли клиента
                else if (curentLineOfJSON.Contains("ClientId"))
                {
                    string clientIdFromJSON = curentLineOfJSON.Split(":")[1].Replace(",", "").Replace(" ", "");
                    
                    bool isClient = Clients.Exists(client => client.clientId == clientIdFromJSON);
                    //Проверка на дубликаты клиентов
                    if (!isClient)
                    {
                        //Сохраняем клиента
                        Clients.Add(new Client(clientIdFromJSON));
                    }
                    else
                    {
                        //Бросок с прогиба
                        throw new WarningException($"Find duplicate of ClientId: [{clientIdFromJSON}]!");
                    }
                }
            }
            streamReader.Close();
        }

        /*
        Формат входящего джисона Компьютеров
        {
            "ChronoData": 
            [
                {
                    "ClientId": 12345,
                    "Poste": "PC NAME",
                    "Type": 12,
                    "IsNewest": 1,
                    "Value": 1861781
                },
            ]
        }
        */

        public class Computer
        {

            [Required(ErrorMessage = "Where is ClientID?")]
            public string clientId;

            [Required(ErrorMessage = "Where is Poste?")]
            public string computerPosteName;

            public string dataType;
            public double dataValue;

            public Computer(string clientId, string computerPosteName, string dataType = "0", double dataValue = 0)
            {
                this.clientId = clientId;
                this.computerPosteName = computerPosteName;
                this.dataType = dataType;
                this.dataValue = dataValue;
            }
        }

        public List<Computer> Computers = new List<Computer>();

        public void ParseComputersFromJSON(string pathToComputersJSONFile = _TEST_COMPUTERS_JSON_PATH)
        {
            //Открываем файл
            StreamReader streamReader = new StreamReader(pathToComputersJSONFile);

            string? curentLineOfJSON;

            while (true)
            {
                curentLineOfJSON = streamReader.ReadLine();

                //Если конец файла закругляемся
                if (curentLineOfJSON == null)
                {
                    break;
                }
                //Если нашли копьютер
                else if (curentLineOfJSON.Contains("ClientId"))
                {
                    //Take clientId
                    string clientIdFromJSON = curentLineOfJSON.Split(":")[1].Replace(",", "").Replace(" ", "");

                    //Take computerPosteName
                    curentLineOfJSON = streamReader.ReadLine();
                    string computerPosteNameFromJSON = curentLineOfJSON.Split(":")[1].Replace(",", "").Replace(" ", "");

                    //Take computerTypeData
                    curentLineOfJSON = streamReader.ReadLine();
                    string computerTypeDataFromJSON = curentLineOfJSON.Split(":")[1].Replace(",", "").Replace(" ", "");

                    //Take computerIsNewest
                    curentLineOfJSON = streamReader.ReadLine();
                    string computerIsNewestFromJSON = curentLineOfJSON.Split(":")[1].Replace(",", "").Replace(" ", "");

                    //Пропускаем старые записи
                    if (computerIsNewestFromJSON == "0")
                    {
                        continue;
                    }

                    //Take computerValueData
                    curentLineOfJSON = streamReader.ReadLine();
                    double computerValueDataFromJSON = Convert.ToDouble(curentLineOfJSON.Split(":")[1].Replace(",", "").Replace(" ", ""));

                    //Сохраняем данные компьютера
                    Computers.Add(new Computer(clientIdFromJSON, computerPosteNameFromJSON, computerTypeDataFromJSON, computerValueDataFromJSON));
                }
            }
            streamReader.Close();
        }

        //ЭТАП 2 - расчет рейтинга для каждого клиента
        public void CalculateComputersRatingByClient() 
        {
            foreach (Client currentClient in Clients)
            {
                //Поиск всех копьютеров клиента
                List<Computer> clientComputers = Computers.FindAll(computer => computer.clientId == currentClient.clientId);
                if (clientComputers.Count==0)
                {
                    //Нет комьютеров - нет мультиков
                    continue;
                }
                else 
                {
                    //Поиск имён копьютеров клиента
                    //(вот тут можно использовать linq и просто запросом вырвать только список имён, но я же не ищу лёгких путей УХАХАХАХАХА)
                    List<string> clientComputersNames = new List<string>();
                    foreach (Computer computer in clientComputers)
                    {
                        if (!clientComputersNames.Contains(computer.computerPosteName))
                        {
                            clientComputersNames.Add(computer.computerPosteName);
                        }
                    }

                    //Всего копьютеров у клиента
                    int totalClientComputersCount = clientComputersNames.Count();

                    /*
                     * Мин требования correctComputers соответствено:
                        * 13 - скорость компутера 100 мс (BySpeed)
                        * 14 - скорость сканирования 10 сек (10000 мс) (ByScanSpeed)
                        * 15 - скорость инета 100 мс (ByInternetSpeed)
                        * 16 - время получения распознания 3 сек (3000 мс) (ByRecognitionSpeed)
                    */

                    // 13
                    int correctComputersBySpeedCount = 
                        clientComputers.FindAll(computer => (computer.dataType == "13" && Convert.ToDouble(computer.dataValue) <= 100)).Count;
                    currentClient.computersSpeedRating = 
                        CalculateComputersRating((uint)totalClientComputersCount, (uint)correctComputersBySpeedCount);

                    // 14
                    int correctComputersByScanSpeedCount = 
                        clientComputers.FindAll(computer => (computer.dataType == "14" && Convert.ToDouble(computer.dataValue) <= 10000)).Count;
                    currentClient.computersScanSpeedRating = 
                        CalculateComputersRating((uint)totalClientComputersCount, (uint)correctComputersByScanSpeedCount);

                    // 15
                    int correctComputersByInternetSpeedCount = 
                        clientComputers.FindAll(computer => (computer.dataType == "15" && Convert.ToDouble(computer.dataValue) <= 100)).Count;
                    currentClient.computersInternetSpeedRating = 
                        CalculateComputersRating((uint)totalClientComputersCount, (uint)correctComputersByInternetSpeedCount);

                    // 16
                    int correctComputersByRecognitionSpeedCount = 
                        clientComputers.FindAll(computer => (computer.dataType == "16" && Convert.ToDouble(computer.dataValue) <= 3000)).Count;
                    currentClient.computersRecognitionSpeedRating = 
                        CalculateComputersRating((uint)totalClientComputersCount, (uint)correctComputersByRecognitionSpeedCount);
                }
            }
        }

        //Метод расчета рейтинга
        public static double CalculateComputersRating(uint totalComputers, uint correctComputers)
        {
            double compRating;

            if (correctComputers > totalComputers)
            {
                throw new ArgumentException("Wrong arguments: correctComputers can't be more than totalComputers.");
            }
            else if (correctComputers == 0 || totalComputers == 0)
            {
                compRating = 0;
            }
            else
            {
                //TODO: я вот хз нужно ли осуществлять приведение при вычислении, но в справочнике было написано, что лучше приводить
                compRating = ((((double)correctComputers / (double)totalComputers) * 5));
            }

            return Math.Round(compRating, 1);
        }

        //ЭТАП 3 - упаковать обратно в джисон
        public string CrateRatigJSONEFile(string pathToRatigJSONEFile = _TEST_RATING_JSON_PATH)
        {
            /*
             * JSONE file tamplate:
             * 
             * {\n
             * "ClientsRatingData": [n\
             * \t{\n
             * \t\t"ClientId" : 12345,\n
             * \t\t"ComputersSpeedRating" : 2.5,\n
             * \t\t"ComputersScanSpeedRating" : 2.5,\n
             * \t\t"ComputersInternetSpeedRating" : 2.5,\n
             * \t\t"ComputersRecognitionSpeedRating" : 2.5\n
             * \t},\n
             * ]}
             * 
             */
            StreamWriter streamWriter = new StreamWriter(pathToRatigJSONEFile, false);

            streamWriter.Write(
                "{\n" +
                "\"ClientsRatingData\": [\n"
                );

            if(Clients.Count != 0)
            {
                for (int i = 0; i < Clients.Count; i++)
                {
                    string ClientId = Clients[i].clientId;
                    string ComputersSpeedRating = Clients[i].computersSpeedRating.ToString().Replace(",", ".");
                    string ComputersScanSpeedRating = Clients[i].computersScanSpeedRating.ToString().Replace(",", ".");
                    string ComputersInternetSpeedRating = Clients[i].computersInternetSpeedRating.ToString().Replace(",", ".");
                    string ComputersRecognitionSpeedRating = Clients[i].computersRecognitionSpeedRating.ToString().Replace(",", ".");

                    streamWriter.Write(
                        "\t{\n" +
                        $"\t\t\"ClientId\" : {ClientId},\n" +
                        // Сколько еще нужно веков людям для установки единого стандарта написания десятичных дробей?
                        // (ﾉಥ益ಥ）ﾉ﻿ ┻━┻
                        $"\t\t\"ComputersSpeedRating\" : {ComputersSpeedRating},\n" +
                        $"\t\t\"ComputersScanSpeedRating\" : {ComputersScanSpeedRating},\n" +
                        $"\t\t\"ComputersInternetSpeedRating\" : {ComputersInternetSpeedRating},\n" +
                        $"\t\t\"ComputersRecognitionSpeedRating\" : {ComputersRecognitionSpeedRating}\n" +
                        "\t}"
                        );
                    //Куда ж без костылей
                    if (i == (Clients.Count - 1))
                    {
                        streamWriter.Write("\n");
                    }
                    else
                    {
                        streamWriter.Write(",\n");
                    }
                }
            }

            streamWriter.Write("]}");
            streamWriter.Close();

            return pathToRatigJSONEFile;
        }
    }
}
