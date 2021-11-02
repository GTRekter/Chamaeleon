using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib
{
    [SpecialClass(2)]
    public static class MathStuff
    {
        public static double CircleCircumference(double radius)
        {
            return 2 * Math.PI* radius;
        }
        public static double CircleArea(double radius)
        {
            return Math.PI * Math.Pow(radius, 2);
        }
        public static double PointDistance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow((p2.X - p1.X), 2) + Math.Pow((p2.Y - p1.Y), 2));
        }
    }
}
