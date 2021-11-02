using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestClient.MathServiceRef;

namespace TestClient
{
    class Program
    {
        private static MathServiceClient proxy;
        static void Main()
        {
            proxy = new MathServiceClient();

            TestMathProxy();
        }

        private static void TestMathProxy() {
            string choice = string.Empty;
            do
            {
                PrintMenu();
                choice = Console.ReadLine();
            } while (!ValidateInput(choice, 1, 7));
            if (!choice.Equals("q", StringComparison.InvariantCultureIgnoreCase))
            {
                string result = ExecuteOperation(choice);
                Console.WriteLine(result);
                do
                {
                    Console.Write("Do you wanna execute another operation? (Y/N):");
                    choice = Console.ReadLine();
                } while (!ValidateInput(choice));
                if (choice.Equals("y", StringComparison.InvariantCultureIgnoreCase))
                {
                    TestMathProxy();
                }
            }
        }
        private static void PrintMenu()
        {
            Console.Clear();
            Console.WriteLine("==============================================");
            Console.WriteLine("   Distributed Programming - Laboratory 1");
            Console.WriteLine("==============================================");
            Console.WriteLine("Mathematical operations");
            Console.WriteLine("1. Add");
            Console.WriteLine("2. Subtract");
            Console.WriteLine("3. Multiply");
            Console.WriteLine("4. Divide");
            Console.WriteLine("Geometrical operations");
            Console.WriteLine("5. CircleArea");
            Console.WriteLine("6. RectangleArea");
            Console.WriteLine("General operations");
            Console.WriteLine("7. Get service informations");
            Console.WriteLine("Q. Quit");
            Console.Write("Operation: ");
        }
        private static bool ValidateInput(string choiceString)
        {
            if (choiceString.Equals("y", StringComparison.InvariantCultureIgnoreCase)
                || choiceString.Equals("n", StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }
            return false;
        }
        private static bool ValidateInput(string choiceString, int min, int max)
        {
            int choice;
            if (choiceString.Equals("q", StringComparison.InvariantCultureIgnoreCase) 
                || (int.TryParse(choiceString, out choice) && choice >= min && choice <= max))
            {
                return true;
            }
            return false;
        }
        private static bool ValidateIntegerInput(string choiceString, out int choice)
        {
            if (int.TryParse(choiceString, out choice))
            {
                return true;
            }
            return false;
        }
        private static bool ValidateIntegerDouble(string choiceString, out double choice)
        {
            if (double.TryParse(choiceString, out choice))
            {
                return true;
            }
            return false;
        }
        private static string ExecuteOperation(string choiceString)
        {
            double? result = null;
            int choice = int.Parse(choiceString);
            switch (choice)
            {
                case 1:
                    result = Add();
                    break;
                case 2:
                    result = Subtract();
                    break;
                case 3:
                    result = Multiply();
                    break;
                case 4:
                    result = Divide();
                    break;
                case 5:
                    result = CircleArea();
                    break;
                case 6:
                    result = RectangleArea();
                    break;
                case 7:
                    return PrintServiceInformations();
                default:
                    break;
            }
            return $"Result {result.ToString()}";
        }

        private static double Add()
        {
            int number;
            string numberString = string.Empty;
            do
            {
                Console.Write("Insert the number of double with you want to sum: ");
                numberString = Console.ReadLine();
            } while (!ValidateIntegerInput(numberString, out number));

            bool isValid;
            double numberToAdd;
            double[] numbers = new double[number];
            for (int i = 0; i < number; i++)
            {
                do
                {
                    Console.Write("Insert a number (double): ");
                    numberString = Console.ReadLine();
                    isValid = ValidateIntegerDouble(numberString, out numberToAdd);
                    if (!isValid)
                    {
                        Console.WriteLine("Invalid number");
                    }
                } while (!isValid);
                numbers[i] = numberToAdd;
            }
            if (number == 2) {
                return proxy.Add(numbers[0], numbers[1]);
            }
            return proxy.AddMultiple(numbers);
        }
        private static double Subtract()
        {
            bool isValid;
            double numberToAdd;
            double[] numbers = new double[2];
            string numberString = string.Empty;

            for (int i = 0; i < 2; i++)
            {
                do
                {
                    Console.Write("Insert a number (double): ");
                    numberString = Console.ReadLine();
                    isValid = ValidateIntegerDouble(numberString, out numberToAdd);
                    if (isValid)
                    {
                        Console.WriteLine("Invalid number");
                    }
                } while (!isValid);
                numbers[i] = numberToAdd;
            }

            return proxy.Subtract(numbers[0], numbers[1]);
        }
        private static double Multiply()
        {
            bool isValid;
            double numberToAdd;
            double[] numbers = new double[2];
            string numberString = string.Empty;

            for (int i = 0; i < 2; i++)
            {
                do
                {
                    Console.Write("Insert a number (double): ");
                    numberString = Console.ReadLine();
                    isValid = ValidateIntegerDouble(numberString, out numberToAdd);
                    if (!isValid)
                    {
                        Console.WriteLine("Invalid number");
                    }
                } while (!isValid);
                numbers[i] = numberToAdd;
            }

            return proxy.Multiply(numbers[0], numbers[1]);
        }
        private static double Divide()
        {
            bool isValid;
            double numerator;
            double denominator;
            string numberString = string.Empty;

            do
            {
                Console.Write("Insert the numerator (double): ");
                numberString = Console.ReadLine();
                isValid = ValidateIntegerDouble(numberString, out numerator);
                if (!isValid)
                {
                    Console.WriteLine("Invalid number");
                }
            } while (!isValid);
            do
            {
                Console.Write("Insert the denominator (double): ");
                numberString = Console.ReadLine();
                isValid = ValidateIntegerDouble(numberString, out denominator);
                if (!isValid)
                {
                    Console.WriteLine("Invalid number");
                }
            } while (!isValid);

            return proxy.Divide(numerator, denominator);
        }
        private static double CircleArea()
        {
            bool isValid;
            double radius;
            string numberString = string.Empty;

            do
            {
                Console.Write("Insert the radius (double): ");
                numberString = Console.ReadLine();
                isValid = ValidateIntegerDouble(numberString, out radius);
                if (!isValid)
                {
                    Console.WriteLine("Invalid number");
                }
            } while (!isValid);

            return proxy.CircleArea(radius);
        }
        private static double RectangleArea()
        {
            bool isValid;
            double width;
            double length;
            string numberString = string.Empty;

            do
            {
                Console.Write("Insert the width (double): ");
                numberString = Console.ReadLine();
                isValid = ValidateIntegerDouble(numberString, out width);
                if (!isValid)
                {
                    Console.WriteLine("Invalid number");
                }
            } while (!isValid);
            do
            {
                Console.Write("Insert the length (double): ");
                numberString = Console.ReadLine();
                isValid = ValidateIntegerDouble(numberString, out length);
                if (!isValid)
                {
                    Console.WriteLine("Invalid number");
                }
            } while (!isValid);

            return proxy.RectangleArea(width, length);
        }
        private static string PrintServiceInformations()
        {
            return PrintProperties(proxy);
        }
        private static string PrintProperties(object obj)
        {
            StringBuilder builder = new StringBuilder();
            Type objType = obj.GetType();
            PropertyInfo[] properties = objType.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                object propValue = property.GetValue(obj);
                builder.AppendLine(String.Format(" {0}: {1}", property.Name, propValue));
            }
            return builder.ToString();
        }

    }
}
