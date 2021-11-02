using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Models
{
    public static class Utils
    {
        public static bool IsRelativelyEqual(double d1, double d2)
        {
            double difference = Math.Abs(d1 - d2);
            double marginOfError = Math.Abs((((d1 + d2) / 2) * 0.0001));

            if (difference < marginOfError)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Determines the smallest and largest values for the given list’s X and Y members
        /// </summary>
        /// <param name="points">list of Point</param>
        /// <returns>Return minx, minY, maxX, maxY</returns>
        public static Tuple<double, double, double, double> GetBoundingBox(List<Point> points)
        {
            double minX = 0;
            double minY = 0;
            double maxX = 0;
            double maxY = 0;

            for (int i = 0; i < points.Count; i++)
            {
                if(i == 0)
                {
                    minX = points[i].X;
                    minY = points[i].Y;
                    maxX = points[i].X;
                    maxY = points[i].Y;
                }

                if (points[i].X > maxX)
                {
                    maxX = points[i].X;
                }
                else if (points[i].X < minX)
                {
                    minX = points[i].X;
                }

                if (points[i].Y > maxY)
                {
                    maxY = points[i].Y;
                }
                else if (points[i].Y < minY)
                {
                    minY = points[i].Y;
                }
            }

            return new Tuple<double, double, double, double>(minX, minY, maxX, maxY);
        }
    }
}
