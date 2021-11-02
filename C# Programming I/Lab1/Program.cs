using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string userInput = string.Empty;
                bool parseResult = false;

                // Username (string)
                //string name = string.Empty;
                //Console.ForegroundColor = ConsoleColor.Yellow;
                //Console.Write("Please enter your name: ");
                //Console.ForegroundColor = ConsoleColor.Magenta;
                //name = Console.ReadLine();

                //Console.ForegroundColor = ConsoleColor.White;
                //Console.WriteLine("Hello, " + name);
                string name = string.Empty;
                do
                {
                    name = InputRequest("Please enter your name");
                } while (string.IsNullOrEmpty(name));
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Hello, " + name);

                // Date of birth (datetime)
                //DateTime dob = DateTime.MinValue;
                //string dobInput = string.Empty;
                //Console.ForegroundColor = ConsoleColor.Yellow;
                //Console.Write("Please enter your data of birth: ");
                //Console.ForegroundColor = ConsoleColor.Magenta;
                //dobInput = Console.ReadLine();
                //if (DateTime.TryParse(dobInput, out dob))
                //{
                //    Console.WriteLine("Your birthday is " + dob.ToShortDateString() + " which was a " + dob.DayOfWeek);
                //}
                //else
                //{
                //    Console.WriteLine("That is an invalid birthday!");
                //}
                DateTime userDataOfBirth = DateTime.MinValue;
                do
                {
                    userInput = InputRequest("Please enter your data of birth");
                    parseResult = ParseInput(userInput, "date of birth", out userDataOfBirth);
                } while (!parseResult);

                // User Id (int)
                int userId = 0;
                do
                {
                    userInput = InputRequest("Please enter your user ID");
                    parseResult = ParseInput(userInput, "user ID", out userId);
                } while (!parseResult);

                // Salary (double)
                double userSalary = 0;
                do
                {
                    userInput = InputRequest("Please enter your salary");
                    parseResult = ParseInput(userInput, "salary", out userSalary);
                } while (!parseResult);

                // Is empoyed (bool)
                bool userIsEmployee = false;
                do
                {
                    userInput = InputRequest("Please enter if you are an employee");
                    parseResult = ParseInput(userInput, "employee", out userIsEmployee);
                } while (!parseResult);

                Console.WriteLine();
                Console.ResetColor();

                //// Ractagnle area
                Console.WriteLine($"#1 {CalculateAreaRectangle(6.5, 3)}");

                // Circle area
                Console.WriteLine($"#2 {CalculateAreaCircle(7.2)}");

                // Distance
                Console.WriteLine($"#3A {CalculateDistance(2, 5, -3, 7)}");
                Console.WriteLine($"#3B {CalculateDistance(-5, 4, 1, 0)}");
                Console.WriteLine($"#3C {CalculateDistance(6, -2, -4, -6)}");
            }
            catch (Exception)
            {
                Console.Write("An Exception occurs during the execution");
            }

            Console.Write("Press <ENTER> to quit...");
            Console.ReadLine();
        }

        #region Private methods

        private static string InputRequest(string prompt)
        {
            string userInput = string.Empty;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{prompt}: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            return Console.ReadLine();
        }

        #region ParseInput
        private static bool ParseInput(string input, string propertyName, out int parsedValue)
        {
            if (int.TryParse(input, out parsedValue))
            {
                Console.WriteLine($"Your {propertyName} is {parsedValue.ToString()}");
                return true;
            }
            else
            {
                Console.WriteLine($"That is an invalid {propertyName}");
                return false;
            }
        }
        private static bool ParseInput(string input, string propertyName, out double parsedValue)
        {
            if (double.TryParse(input, out parsedValue))
            {
                Console.WriteLine($"Your {propertyName} is {parsedValue.ToString()}");
                return true;
            }
            else
            {
                Console.WriteLine($"That is an invalid {propertyName}");
                return false;
            }
        }
        private static bool ParseInput(string input, string propertyName, out bool parsedValue)
        {
            if (bool.TryParse(input, out parsedValue))
            {
                Console.WriteLine($"Your {propertyName} is set to {parsedValue.ToString()}");
                return true;
            }
            else
            {
                Console.WriteLine("That is an invalid input");
                return false;
            }
        }
        private static bool ParseInput(string input, string propertyName, out DateTime parsedValue)
        {
            if (DateTime.TryParse(input, out parsedValue))
            {
                Console.WriteLine($"Your {propertyName} is {parsedValue.ToShortDateString()} which was a {parsedValue.DayOfWeek}");
                return true;
            }
            else
            {
                Console.WriteLine($"That is an invalid {propertyName}");
                return false;
            }
        }
        #endregion ParseInout

        #region Math
        private static double CalculateAreaRectangle(double width, double height)
        {
            return (width * height);
        }
        private static double CalculateAreaCircle(double radius)
        {
            return Math.PI * Math.Pow(radius, 2);
        }
        private static double CalculateDistance(double x1, double x2, double y1, double y2)
        {
            return Math.Sqrt(Math.Pow((x1 - x2), 2) + Math.Pow((y1 - y2), 2));
        }
        #endregion

        #endregion Private methods
    }
}