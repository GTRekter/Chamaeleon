using Lab5.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lab5
{
    class Program
    {
        private const string _jsonFileName = "stats.json";

        private static string filePath;

        private static string[] unwantedCharacters = new string[] { ".", ",", ";", "(", ")" };

        static void Main(string[] args)
        {
            try
            {
                //GetValidPath();
                
                filePath = "C:\\Users\\ivanp\\Desktop\\Lab5\\Documents";

                DocumentStatistics documentStatistic = new DocumentStatistics();
                ProcessFiles(filePath, documentStatistic);
                SerializeStats(filePath, documentStatistic);
            }
            catch(Exception exc)
            {
                string message = string.Format("Error: {0}", exc.Message);
                Console.WriteLine(message);
            }
            
            Console.Write("Press <ENTER> to quit...");
            Console.ReadLine();
        }

        #region Private Methods

        private static void GetValidPath()
        {
            bool isValid;
            do
            {
                isValid = true;
                Console.Write("Insert the document folder path: ");
                string filePath = Console.ReadLine();

                if (!Directory.Exists(filePath))
                {
                    isValid = false;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: Directory not found. Try again.");
                }
                else if (Directory.GetFiles(filePath).Count() == 0)
                {
                    isValid = false;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: Empty directory. Try again.");
                }
                Console.ResetColor();

            } while (isValid == false);
        }

        private static void ProcessFiles(string filepath, DocumentStatistics stats)
        {
            string[] files = Directory.GetFiles(filepath);
            foreach (string file in files)
            {
                // Output the file being processed to the console window 
                string message = string.Format("Analysing '{0}'", file);
                Console.WriteLine(message);

                // Add the file name to the stats.Documents property 
                stats.Documents.Add(file);

                // Add one to the stats.DocumentCount property 
                stats.DocumentCount += 1;

                // Use a StreamReader to open the current file 
                using (StreamReader reader = new StreamReader(file))
                {
                    // Read in the text in whatever way you see fit 
                    string[] lines = File.ReadAllLines(file);

                    foreach(string line in lines)
                    {

                        /* 
                         * Parse the text into individual words
                         * You can use System.Text.RegularExpressions.Regex.Split() 
                         * For the word splitting, all you need to use is the "\s+" regular expression pattern in 
                         * conjunction with Regex.Split(). 
                         * It does not need to be anything more sophisticated than that.  
                         * Unwanted characters, like '.', ';', '(' and ')' can be filtered out after the split by 
                         * using the string's Replace() method.
                         */

                        string[] words = Regex.Split(line, "\\s+");
                        for (int i = 0; i < words.Length; i++)
                        {
                            /*
                                * Update the stats.WordCounts dictionary to show the counts for each word processed 
                                * This dictionary uses the word as the key and the number of times that word was 
                                * encountered as its value
                                * 1. This should be case-insensitive
                                * 2. You can eliminate symbols like: ( ) : ; . 
                                */

                            string wordToAnalyze = words[i];
                            for (int k = 0; k < unwantedCharacters.Length; k++)
                            {
                                wordToAnalyze = wordToAnalyze.Replace(unwantedCharacters[k], string.Empty);
                            }

                            var previousKey = stats.WordCounts.Keys.FirstOrDefault(k => k == wordToAnalyze.ToLower());
                            if (previousKey != null)
                            {
                                stats.WordCounts[previousKey] = stats.WordCounts[previousKey] + 1;
                            }
                            else
                            {
                                stats.WordCounts.Add(wordToAnalyze.ToLower(), 1);
                            }              
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This method will create a file called “stats.json” in the filepath folder. 
        /// Utilize the DataContractJsonSerializer to create this file.  
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="stats"></param>
        private static void SerializeStats(string filepath, DocumentStatistics stats)
        {
            string serializationFile = Path.Combine(filePath, _jsonFileName);
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(DocumentStatistics));
            using (Stream stream = File.Open(serializationFile, FileMode.Create))
            {
                serializer.WriteObject(stream, stats);
            }
        }

        #endregion
    }
}
