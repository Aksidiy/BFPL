using System;
using System.Collections.Generic;
using System.Text;

namespace BFPL.STEPIK.Basic_Plus_Sintax
{
    public class Taxi
    {
        public static void StartTaxiSimulation() 
        {
            Driver driverA = new Driver("Иван", "Иванов", 34);
            Driver driverC = new Driver("Пётр", "Петров", 25);
            Driver driverE = new Driver("Сергей", "Сергеев", 31);

            Car carA = new Car("O123MP", "Красный", driverA);
            Car carC = new Car("K789BA", "Белый", driverC);
            Car carE = new Car("C127EC", "Фиолетовый", driverE);

            Client clientA = new Client("Григорий", "Григорьев", 21, "1234123412341234");
            Client clientB = new Client("Семён", "Семёнов", 45, "1111222233334444");
            Client clientC = new Client("Владимир", "Владимиров", 29, "5678567856785678");
            Client clientD = new Client("Борис", "Борисов", 60, "0987654309876543");
            Client clientE = new Client("Аркадий", "Аркадьев", 32, "1029384756101234");

            Order orderA = new Order(clientA, 200, carA);
            Order orderB = new Order(clientB, 150, carA);
            Order orderC = new Order(clientC, 500, carC);
            Order orderD = new Order(clientD, 300, carC);
            Order orderE = new Order(clientE, 250, carE);

            Console.WriteLine($"{driverA.LastName} : {FindPayment(new Order[] { orderA, orderB, orderC, orderD, orderE }, driverA.Id)}");
        }
        public class Order
        {
            //реализуйте класс Order
            public Car Car { get; set; }

            public Client Client { get; set; }

            public decimal Price { get; set; }

            public Order(Client client, decimal price)
            {
                Client = client;
                Price = price;
            }

            public Order(Client client, decimal price, Car car)
            {
                Client = client;
                Price = price;
                Car = car;
            }
        }

        public class Client
        {
            public readonly Guid Id;
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int Age { get; set; }
            public string CardNumber { get; set; }

            public Client(string firstName, string lastName, int age, string cardNumber)
            {
                Id = Guid.NewGuid();
                FirstName = firstName;
                LastName = lastName;
                Age = age;
                CardNumber = cardNumber;
            }
        }

        public class Car
        {
            //реализуйте класс Car
            public string Number { get; set; }

            public string Color { get; set; }

            public Driver Driver { get; set; }

            public Car(string number, string color, Driver driver)
            {
                Number = number;
                Color = color;
                Driver = driver;
            }
        }

        public class Driver
        {
            //реализуйте класс Driver
            public readonly Guid Id;

            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int Age { get; set; }

            public Driver(string firstName, string lastName, int age)
            {
                Id = Guid.NewGuid();
                FirstName = firstName;
                LastName = lastName;
                Age = age;
            }
        }

        public static void ChangeColor(Car[] cars)
        {
            foreach (Car car in cars)
            {
                char buff = car.Number[0];
                if (buff == 'O') { car.Color = "Оранжевый"; }
                else if (buff == 'C') { car.Color = "Синий"; }
                else if (buff == 'K') { car.Color = "Красный"; }
                else { car.Color = "Белый"; }
            }
        }

        public static void CarsDrivers(Car[] cars)
        {
            //реализуйте метод CarsDrivers
            foreach (Car car in cars)
            {
                string buff = (car.Driver != null) ? car.Driver.LastName : "Нет водителя";
                Console.WriteLine($"{car.Number}:{buff}");
            }
        }

        public static decimal FindPayment(Order[] orders, Guid driverId)
        {
            decimal payment = 0.0m;

            foreach (Order order in orders)
            {
                if (order.Car.Driver != null && order.Car.Driver.Id == driverId)
                {
                    payment += order.Price * 0.8m;
                }
            }

            return payment;
        }
    }
}
