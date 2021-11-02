using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Runtime.Serialization;
using Lab5Client.Models;
using System.Runtime.Serialization.Json;
using System.IO;

namespace Lab5Client
{
    class Program
    {
        const string BASE_ADDR = "http://localhost:44396/api/students/";
        
        static void Main()
        {
            MenuOptionsEnum choice = MenuOptionsEnum.Quit;
            do
            {
                choice = ConsoleHelpers.ReadEnum<MenuOptionsEnum>("Option: ");
                Console.Clear();

                if (choice != MenuOptionsEnum.Quit)
                {
                    DisplayOptionTitle(choice);
                }
                switch (choice)
                {
                    case MenuOptionsEnum.GetStudents:
                        GetStudents();
                        break;
                    case MenuOptionsEnum.GetStudent:
                        GetStudent();
                        break;
                    case MenuOptionsEnum.AddStudent:
                        AddStudent();
                        break;
                    case MenuOptionsEnum.UpdateStudent:
                        UpdateStudent();
                        break;
                    case MenuOptionsEnum.DeleteStudent:
                        DeleteStudent();
                        break;

                }
                Console.WriteLine();
                Console.Write("Press <ENTER> to continue...");
                Console.ReadLine();
                Console.Clear();

            } while (choice != MenuOptionsEnum.Quit);
        }

        /// <summary>
        /// Displays the screen title for the given menu choice
        /// </summary>
        /// <param name="choice">Menu choice</param>
        private static void DisplayOptionTitle(MenuOptionsEnum choice)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(choice.WordBreakMixedCase());
            ConsoleHelpers.WriteBorder('=', choice.WordBreakMixedCase().Length);
            Console.WriteLine();
            Console.ResetColor();
        }
        private static void GetStudent()
        {
            var result = ClientHelper.Get<Student>(BASE_ADDR, SerializationModesEnum.Json, "{0}", 123456789);
            Console.WriteLine(result.Result);
        }
        private static void GetStudents()
        {
            var results = ClientHelper.Get<List<Student>>(BASE_ADDR, SerializationModesEnum.Json, "?page={0}&count={1}", 5, 5);
            foreach (var s in results.Result)
            {
                Console.WriteLine(s);
            }
        }
        private static void AddStudent()
        {

            Student add = new Student
            {
                ID = 123456789,
                LastName = "Gates",
                FirstName = "Bill",
                Grade = Student.GradeEnum.College,
                DOB = new DateTime(1955, 10, 28),
                GPA = 3.5f
            };
            var addResult = ClientHelper.Post<Student, object>(BASE_ADDR, SerializationModesEnum.Json, add, string.Empty);
            if (addResult.StatusCode != System.Net.HttpStatusCode.OK) // 200 == success
            {
                Console.WriteLine("Error encountered: {0}", addResult.Error);
            }
        }
        private static void UpdateStudent()
        {
            Student update = new Student
            {
                LastName = "Gates",
                FirstName = "Bill",
                Grade = Student.GradeEnum.College,
                DOB = new DateTime(1955, 10, 28),
                GPA = 3.5f
            };
            var putResult = ClientHelper.Put<Student>(BASE_ADDR, SerializationModesEnum.Json, update, "{0}", 123456789);
            if ((int)putResult.StatusCode < 200 && (int)putResult.StatusCode >= 300) // 200 == success
            {
                Console.WriteLine("Error encountered: {0}", putResult.Error);
            }
            else
            {
                Console.WriteLine("Operation completed without results");
            }
        }
        private static void DeleteStudent()
        {
            var deleteResult = ClientHelper.Delete(BASE_ADDR, "{0}", 123456789);
            if ((int)deleteResult.StatusCode < 200 && (int)deleteResult.StatusCode >= 300) // 200 == success
            {
                Console.WriteLine("Error encountered: {0}", deleteResult.Error);
            }
            else
            {
                Console.WriteLine("Operation completed without results");
            }
        }

    }
}
