using System;
using System.Collections.Generic;
using System.Text;

namespace BFPL.STEPIK.Basic_Plus_Sintax
{
    /// <summary>
    /// Класс симулирует школу.
    /// </summary>
    public class School
    {
        public string Name;
        public List<Student> Students;

        public School(string name)
        {
            Name = name;
            Students = new List<Student>();

            Console.WriteLine($"Школа {Name} успешно создана.");
        }

        /// <summary>
        /// Выводит в консоль список учеников школы.
        /// </summary>
        public void PrintStudents()
        {
            Students.Sort();
            if (Students.Count == 0)
            {
                Console.WriteLine($"В школе {Name} пока нет учеников!");
            }
            else
            {
                Console.WriteLine($"Список учеников школы {Name}:");
                for (int i = 0; i < Students.Count; i++)
                {
                    Console.WriteLine("{0, -10}. {1, -10} {2, -10} {3, -10} лет", (i + 1), Students[i].LastName, Students[i].FirstName, Students[i].Age);
                }
            }
        }

        /// <summary>
        /// Добавляет ученика в школу.
        /// </summary>
        /// <param name="student"> Ученик для добавления в школу. </param>
        public void AddNewStudent(Student student)
        {
            Students.Add(student);
            Console.WriteLine($"Студент {student.LastName} {student.FirstName} успешно зачислен в школу {Name}.");
        }

        /// <summary>
        /// Исключает ученика из школы по индексу.
        /// </summary>
        /// <param name="index"> Индекс ученика для исключения. </param>
        public void RemoveStudentByIndex(int index)
        {
            if (0 <= index && index < Students.Count)
            {
                Console.WriteLine($"Студент {Students[index].LastName} {Students[index].FirstName} успешно отчислен из школы {Name}.");
                Students.RemoveAt(index);
            }
            else
            {
                throw new ArgumentOutOfRangeException($"Can't delete student at {index} index.");
            }
        }

        /// <summary>
        /// Пример работы класса School.
        /// </summary>
        public static void StartSchoolSimulation()
        {
            Console.WriteLine("Введите название новой школы:");
            string schoolName = Console.ReadLine() ?? "Безымянная";

            School school = new School(schoolName);

            Console.WriteLine($"Хотите посмотреть список всех учеников школы {school.Name}? ДА/НЕТ");
            string listAnswer = (Console.ReadLine() ?? "НЕТ").ToUpper();
            if (listAnswer == "ДА")
            {
                school.PrintStudents();
            }

            Console.WriteLine($"Хотите зачислить нового ученика в школу {school.Name}? ДА/НЕТ");
            string addAnswer = (Console.ReadLine() ?? "НЕТ").ToUpper();
            if (addAnswer == "ДА")
            {
                Console.WriteLine($"Введите фамилию ученика");
                string lastName = Console.ReadLine() ?? "Безымянный";
                Console.WriteLine($"Введите имя ученика");
                string firstName = Console.ReadLine() ?? "Косипоша";
                Console.WriteLine($"Введите возраст ученика");
                int age = Convert.ToInt32(Console.ReadLine());

                Student student = new Student(firstName, lastName, age);
                school.AddNewStudent(student);
            }

            Console.WriteLine($"Хотите отчислить ученика из школы {school.Name}? ДА/НЕТ");
            string removeAnswer = (Console.ReadLine() ?? "НЕТ").ToUpper();
            if (removeAnswer == "ДА")
            {
                school.PrintStudents();
                Console.WriteLine($"Введите номер ученика для его отчисления из школы {school.Name}:");
                int studentNumber = Convert.ToInt32(Console.ReadLine());
                school.RemoveStudentByIndex(studentNumber - 1);
            }
        }
    }

    /// <summary>
    /// Класс симулирует ученика школы.
    /// </summary>
    public class Student
    {
        public string LastName;
        public string FirstName;
        public int Age;

        public Student(string lastName, string firstName, int age)
        {
            LastName = lastName;
            FirstName = firstName;
            if (5 > Age && Age < 25)
            {
                Age = age;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Student age must be: 5 > Age && Age < 25");
            }
        }
    }
}
