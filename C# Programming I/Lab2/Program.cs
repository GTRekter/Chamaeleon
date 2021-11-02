using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            ArrayTest();
            ListTest();
            StringFormatting();
            Console.Write("Press <Enter> to quit...");
            Console.ReadLine();
        }

        static void ArrayTest()
        {
            Console.WriteLine("ArrayTest()");
            Console.Write("Please enter 5 numbers separated by commas: ");
            string input = Console.ReadLine();
            string[] inputs = input.Split(',');
            double[] values = new double[5];

            for (int index = 0; index < inputs.Length; index++)
            {
                values[index] = double.Parse(inputs[index]);
            }
            Array.Sort(values);

            double min = values.Min();
            double max = values.Max();
            double sum = values.Sum();
            double average = values.Average();

            Console.WriteLine($"Values: {string.Join(" ",values)}");
            Console.WriteLine($"Min: {min}");
            Console.WriteLine($"Max: {max}");
            Console.WriteLine($"Sum: {sum}");
            Console.WriteLine($"Average: {average}");
        }

        static void ListTest()
        {
            Console.WriteLine("ListTest()");
            List<string> words = new List<string>();
            Console.Write("Continue entering one word at a time at the prompt.  ");
            Console.WriteLine("Enter 'q' to quit.");
            while (true)
            {
                // TODO: Add word reading code here...  
                Console.Write("Word: ");
                string word = Console.ReadLine();
                if (word.ToUpper() == "Q")
                {
                    break;
                }
                words.Add(word);
            }
            // TODO: Add string join code here... 

            string join = string.Join(" ", words);
            Console.WriteLine(join);
        } 

        private static void StringFormatting()
        {
            Console.WriteLine("StringFormatting()");
            int[] partNumber = new int[5];
            decimal[] price = new decimal[5];
            long[] servicePhone = new long[5];
            string[] partName = new string[5];
            DateTime[] mfgDate = new DateTime[5];

            bool parseResult = false;
            for (int i = 0; i <= 4; i++)
            {
                do
                {
                    Console.Write($"Please insert partNumber n°{i}: ");
                    parseResult = ParseInput(Console.ReadLine(), "partNumber", out partNumber[i]);
                } while (!parseResult);

                do
                {
                    Console.Write($"Please insert partName n°{i}: ");
                    partName[i] = Console.ReadLine();
                } while (!parseResult);

                do
                {
                    Console.Write($"Please insert price n°{i}: ");
                    parseResult = ParseInput(Console.ReadLine(), "partNumber", out price[i]);
                } while (!parseResult);

                do
                {
                    Console.Write($"Please insert servicePhone n°{i}: ");
                    parseResult = ParseInput(Console.ReadLine(), "servicePhone", out servicePhone[i]);
                } while (!parseResult);


                do
                {
                    Console.Write($"Please insert mfgDate n°{i}: ");
                    parseResult = ParseInput(Console.ReadLine(), "mfgDate", out mfgDate[i]);
                } while (!parseResult);
            }

            Console.WriteLine(string.Format("{0,-11}{1,-22}{2,13}{3,6}{4,18}", "Part #", "Part Name", "Price", "Phone", "MFG Date"));
            Console.WriteLine(new string('=', 70));
            Console.WriteLine(new string('=', 70));

            for (int i = 0; i <= 4; i++)
            {
                var standardizedPartNumber = string.Format("{0,2:D9}", partNumber[i]);
                Console.WriteLine(string.Format("{0,-2}-{1,-8}{2,-20}{3,15:C}{4,-16: (###)###-####}{5,-8:d}",
                    standardizedPartNumber.Substring(0, 2), standardizedPartNumber.Substring(2, 7), partName[i], price[i], servicePhone[i], mfgDate[i]));
            }

        }

        #region ParseInput
        private static bool ParseInput(string input, string propertyName, out int parsedValue)
        {
            if (int.TryParse(input, out parsedValue))
            {
                return true;
            }
            else
            {
                Console.WriteLine($"That is an invalid {propertyName}");
                return false;
            }
        }
        private static bool ParseInput(string input, string propertyName, out long parsedValue)
        {
            if (long.TryParse(input, out parsedValue))
            {
                return true;
            }
            else
            {
                Console.WriteLine($"That is an invalid {propertyName}");
                return false;
            }
        }
        private static bool ParseInput(string input, string propertyName, out decimal parsedValue)
        {
            if (decimal.TryParse(input, out parsedValue))
            {
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
                return true;
            }
            else
            {
                Console.WriteLine($"That is an invalid {propertyName}");
                return false;
            }
        }
        #endregion ParseInput

    }
}