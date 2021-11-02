using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            Statistics();

            Coordinates();

            SumOfSquares();

            Console.ReadLine();
        }

        static void Statistics()
        {
                             //1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16
                             //0  1  2  3  4  5  6  7  8  9  10 11 12 13 14 15
            int[] values = { 1, 6, 4, 7, 9, 2, 5, 7, 2, 6, 5, 7, 8, 1, 2, 8 };

            Console.WriteLine($"Values: {string.Join(",", values)}");
            Console.WriteLine($"Mean: {string.Join(", ", Mean(values))}");
            Console.WriteLine($"Median: {string.Join(", ", Median(values))}");
            Console.WriteLine($"Mode: {string.Join(", ", Mode(values))}");
        }

        /// <summary>
        /// This method must calculate the average of the values stored in the values array parameter.  
        /// The average is the sum of a set of values divided by the number of items in the set.   
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        static double Mean(int[] values)
        {
            if(values == null || values.Length == 0) return double.NaN;

            return (double)values.Sum() / values.Length;
        }

        /// <summary>
        /// The median of a set of values is the center-most value when the set is sorted.  
        /// If the set has an odd number of items then the result is item at the middle index of the array.  
        /// If the set has an even number of elements then the result is the average (mean) of the two center-most items. 
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        static double Median(int[] values)
        {
            if (values == null || values.Length == 0) return double.NaN;

            int[] sorted = new int[values.Length];
            values.CopyTo(sorted, 0);
            Array.Sort(sorted);

            if (sorted.Length % 2 != 0)
            {
                return sorted[sorted.Length / 2];
            }
            else
            {
                return Mean(new int[] { sorted[((sorted.Length -1) / 2)], sorted[((sorted.Length -1) / 2)] +1});   
            }

        }

        /// <summary>
        /// The mode of a set of numbers is value that appears with the highest frequency in that set.  
        /// In the event more than one value appears the same number of times then all of those values represent the mode. 
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        static List<int> Mode(int[] values)
        {
            int[] sorted = new int[values.Length];
            values.CopyTo(sorted, 0);
            Array.Sort(sorted);

            List<int> result = new List<int>();
            var counts = new Dictionary<int, int>();
            int max = 0;

            foreach(int key in sorted)
            {
                int count = 1;
                if (counts.ContainsKey(key))
                {
                    count = count + counts[key];
                    if (count > max)
                    {
                        max = count;
                    }
                }
                counts[key] = count;
            }

            foreach (var key in counts.Keys)
            {
                if (counts[key] == max) result.Add(key);
            }

            return result;
        }

        static void SumOfSquares()
        {
            int input = 0;
            do
            {
                Console.Write("Enter an int: ");
            } while (!int.TryParse(Console.ReadLine(), out input));

            Console.Write($"The sum of squares for {input} is {Squares(input)} ");
        }

        static long Squares(long value)
        {
            long squares = 0;
            if (value == 0) return 0;

            squares = (value * value) + Squares(value - 1);

            return squares;
        }

        static void Coordinates()
        {
            Console.Write("Enter a coordinate in the form (x, y): ");
            string input = Console.ReadLine();
            double x, y;
            if (TryParsePoint(input, out x, out y))
            {
                var polar = RectangularToPolar(x, y);
                Console.WriteLine($"r: {polar.Item1}, angle: {polar.Item2} radians");
                Console.WriteLine($"r: {polar.Item1}, angle: {RadiansToDegrees(polar.Item2)}°");
            }
        }

        static bool TryParsePoint(string input, out double x, out double y)
        {
            /*
            First, set x and y to 0 to satisfy the requirement that out parameters must be set 
            • Trim the input string of any whitespace characters. There are many techniques you can use to accomplish this: Trim() is a good choice. 
            • Determine if the trimmed input string starts with a '(', ends with a ')' and contains a ',' in between.  If it doesn’t return false. 
            • Either trim the parentheses off the input string or obtain a substring that omits the parentheses.   On this trimmed string, split it into a string array using ',' as a delimiter (see string.Split()).  If the array does not contain 2 items, return false. 
            • Attempt to parse the first string element out of the array into x using double.TryParse().  If it fails (returns false) then this method returns false. 
            • Attempt to parse the second string element out of the array into y using double.TryParse(). If it fails (returns false) then this method returns false. 
            • If the code makes it this far, return true. 
            */

            x = 0;
            y = 0;
            input = input.Trim();
            
            if (!input.StartsWith("(") || !input.EndsWith(")") || !input.Contains(",")) return false;

            input = input.Trim('(');
            input = input.Trim(')');

            string[] charachters = input.Split(',');

            if (charachters.Length < 2) return false;
            if (!double.TryParse(charachters[0], out x)) return false;
            if (!double.TryParse(charachters[1], out y)) return false;

            return true;
        }

        static Tuple<double, double> RectangularToPolar(double x, double y)
        {
            /*
            1. Point is on origin: r = 0, angle = 0 
            2. Point is on positive X axis: r = x, angle = 0 
            3. Point is on positive Y axis: r = y, angle = π 2
            4. Point is on negative X axis: r = |x|, angle = π 
            5. Point is on negative Y axis: r = |y|, angle = 3π 2

            6. Point is in Quadrant I: r = √𝑥2 + 𝑦2, angle = 𝑡𝑎𝑛−1 (𝑦 x) 
            7.  Point is in Quadrant II: r = √𝑥2 + 𝑦2, angle = π + 𝑡𝑎𝑛−1 (𝑦 𝑥) 
            8. Point is in Quadrant III: r = √𝑥2 + 𝑦2, angle = π + 𝑡𝑎𝑛−1 (𝑦 𝑥 ) 
            9. Point is in Quadrant IV: r = √𝑥2 + 𝑦2, angle = 2π + 𝑡𝑎𝑛−1 (𝑦 𝑥 ) 
            */

            if (x == 0 && y == 0) return new Tuple<double, double>(0, 0);
            else if (x > 0 && y == 0) return new Tuple<double, double>(x, 0);
            else if (y > 0 && x == 0) return new Tuple<double, double>(y, Math.PI / 2);
            else if (x < 0 && y == 0) return new Tuple<double, double>(Math.Abs(x), Math.PI);
            else if (y < 0 && x == 0) return new Tuple<double, double>(Math.Abs(y), (Math.PI *3)/2);
            else if (x > 0 && y > 0) return new Tuple<double, double>(Math.Sqrt((x * x) + (y * y)), Math.Atan(y/x));
            else if ((x < 0 && y > 0) || (x < 0 && y < 0)) return new Tuple<double, double>(Math.Sqrt((x * x) + (y * y)), Math.PI + Math.Atan(y / x));
            else return new Tuple<double, double>(Math.Sqrt((x * x) + (y * y)), (Math.PI * 2) + Math.Atan(y / x));
            /*if (x < 0 && y > 0)*/
        }

        static double RadiansToDegrees(double radians)
        {
            /*
            degrees = (radians * 180) / π
            if the result of this calculation does not fall between 0 and 360, to repeatedly add or subtract 360 from the result until it is within range. 
            */

            var degress = (radians * 180) / Math.PI;
            while(degress <= 0 && degress >= 360)
            {
                if(degress < 0)
                {
                    degress = degress + 360;
                }
                else
                {
                    degress = degress - 360;
                }
            }

            return degress;
        }
    }
}
