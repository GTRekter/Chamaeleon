using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            string choice = string.Empty;
            while(choice != "q" || choice != "Q")
            {
                Console.WriteLine("Please chose an activity:");
                Console.WriteLine("A. Making change");
                Console.WriteLine("B. Counting letters");
                Console.WriteLine("C. Number guessing game");
                Console.WriteLine("Q. Quit");
                Console.Write("Your choice: ");
                choice = Console.ReadLine();

                switch (choice)
                {
                    case "A":
                    case "a":
                        MakingChange();
                        break;
                    case "B":
                    case "b":
                        CountingLetters();
                        break;
                    case "C":
                    case "c":
                        NumberGuessingGame();
                        break;
                }
                Console.WriteLine();
            }
        }

        private static void MakingChange()
        {
            decimal due = 0, 
                    paid = 0;
            string input = string.Empty;
            do
            {
                Console.Write("Please enter the amount due: ");
            } while (!decimal.TryParse(Console.ReadLine(), out due));

            do
            {
                Console.Write("Please enter the amount paid: ");
            } while (!decimal.TryParse(Console.ReadLine(), out paid));

            if(paid < due)
            {
                Console.WriteLine("You hadn't pay enough.");
            }
            else if(paid == due)
            {
                Console.WriteLine("There is no charge due");
            }
            else
            {
                decimal change = paid - due;
                decimal[] denominations = { 100M, 50M, 20M, 10M, 5M, 1M, 0.50M, 0.25M, 0.10M, 0.05M, 0.01M };
                int[] counts = new int[11];

                int index = 0;
                while(change > 0)
                {
                    if(change >= denominations[index])
                    {
                        counts[index]++;
                        change = change - denominations[index];
                    }
                    else
                    {
                        index++;
                    }
                }

                Console.WriteLine(String.Format("The following change is due: {0:C}",paid - due));
                for (int i = 0; i <= counts.GetUpperBound(0); i++)
                {
                    if(counts[i] > 0)
                    {
                        Console.WriteLine(string.Format("{0,6:C}:{1,-15}", denominations[i], counts[i]));
                    }
                }
            }
        }

        private static void CountingLetters()
        {
            char[] vowels = new char[] { 'a', 'e', 'i', 'o', 'u' };
            Dictionary<string, int> counters = new Dictionary<string, int>()
            {
                {"consonants", 0 },
                {"vowels", 0 },
                {"digits", 0 },
                {"punctuation", 0 },
                {"symbols", 0 },
                {"whitespaces", 0 },
                {"uncknown charachters", 0 }
            };

            Console.Write("Please enter text to analyze: ");
            string input = Console.ReadLine();

            foreach(char c in input)
            {
                if(Char.IsDigit(c))
                {
                    counters["digits"]++;
                }
                else if(char.IsLetter(c))
                {
                    if(vowels.Contains(char.ToLower(c)))
                    {
                        counters["vowels"]++;
                    }
                    else
                    {
                        counters["consonants"]++;
                    }
                }
                else if(char.IsWhiteSpace(c))
                {
                    counters["whitespaces"]++;
                }
                else if(char.IsSymbol(c))
                {
                    counters["symbols"]++;
                }
                else if(char.IsPunctuation(c))
                {
                    counters["punctuation"]++;
                }
                else
                {
                    counters["uncknown charachters"]++;
                }
            }

            // Print results 
            foreach(var counter in counters)
            {
                Console.WriteLine($"{counter.Key} {counter.Value}");
            }

        }

        private static void NumberGuessingGame()
        {
            Random rnd = new Random();
            int target = rnd.Next(100) + 1;

            int guess = 0,
                count = 0;

            do
            {
                Console.Write("Enter a number between 1 and 100: ");
                if(int.TryParse(Console.ReadLine(), out guess))
                {
                    if (guess > target)
                    {
                        count++;
                        Console.WriteLine("Too high.");
                    }
                    else if (guess < target)
                    {
                        count++;
                        Console.WriteLine("Too low.");
                    }
                    else
                    {
                        count++;
                        Console.WriteLine($"You got it in {count} guesses!");
                    }
                }
            } while (guess != target);
        }
    }
}
