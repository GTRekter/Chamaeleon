using Lab1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Program
    {
        static PointList points = new PointList();

        static Action<string> delegateMessage;
        static void Main(string[] args)
        {
            points.Changed += delegate (object sender, PointListChangedEventArgs e) 
            {
                Console.WriteLine($"Points changed: {e.Operation.ToString()}");
            };

            points.Add(new Point(-4, -7));
            points.Add(new Point(0, 0));
            points.Add(new Point(1, 2));
            points.Add(new Point(-4, 5));
            points.Insert(2, new Point(3, 1));
            points.Add(new Point(7, -2));
            points[0] = new Point(2, 1);
            points.RemoveAt(2);

            bool pointOnOrigin = points.Any(p => p.X == 0 && p.Y == 0);
            Console.WriteLine($"Points on the origin: {pointOnOrigin}");
            PrintStrings(points.Where(p => p.X == 0 && p.Y == 0).Select(p => p.ToString()));

            Console.WriteLine($"Points on the first quadrant: ");
            PrintPoints(points.Where(p => p.X > 0 && p.Y > 0));

            Console.WriteLine($"Points on the second quadrant: ");
            PrintPoints(points.Where(p => p.X > 0 && p.Y < 0));

            Console.WriteLine($"Points on the third quadrant: ");
            PrintPoints(points.Where(p => p.X > 0 && p.Y < 0));

            Console.WriteLine($"Points on the fourth quadrant: ");
            PrintPoints(points.Where(p => p.X < 0 && p.Y > 0));

            Console.WriteLine($"Points ordered by X: ");
            PrintPoints(points.OrderBy(p => p.X));

            Console.WriteLine("Press <ENTER> to quit...");
            Console.ReadLine();
        }

        private static void PrintPoints(IEnumerable<Point> pointsToPrint)
        {
            foreach (Point point in pointsToPrint)
            {
                Console.WriteLine(point.ToString());
            }
        }

        private static void PrintStrings(IEnumerable<String> stringsToPrint)
        {
            foreach (String s in stringsToPrint)
            {
                Console.WriteLine(s);
            }
        }
    }
}
