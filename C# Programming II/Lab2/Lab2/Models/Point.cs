using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Models
{
    public struct Point
    {
        public double X;

        public double Y;

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// √(𝑥1 − 𝑥2)2 + (𝑦1 − 𝑦2)2 
        /// </summary>
        /// <param name="other">Other point</param>
        /// <returns></returns>
        public double Distance(Point  other)
        {
            return Math.Sqrt(
                Math.Pow((this.X - other.X), 2) + 
                Math.Pow((this.Y - other.Y), 2));
        }

        public override string ToString()
        {
            return string.Format("({0},{1})", X, Y);
        }
    }
}
