using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Models
{
    public class Triangle : Shape
    {
        /// <summary>
        ///  Override of the base Vertices property
        /// </summary>
        public override List<Point> Vertices
        {
            get
            {
                return base._Vertices;
            }
            set
            {
                if(value.Count != 3)
                {
                    throw new IndexOutOfRangeException("A triangle has just three points");
                }

                base._Vertices = value;
            }
        }

        /// <summary>
        /// Ctor Parameterized
        /// </summary>
        /// <param name=""></param>
        public Triangle(List<Point> vertices) 
            : base(vertices)
        {
        }

        /// <summary>
        /// Ctor Parameterized
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="v3"></param>
        public Triangle(Point v1, Point v2, Point v3) 
            : base(new List<Point>() { v1, v2, v3 })
        { 
        }

        /// <summary>
        /// double Overridden method to calculate area of the triangle.  √𝑠 × (𝑠 − 𝑎) × (𝑠 − 𝑏) × (𝑠 − 𝑐)
        /// </summary>
        /// <returns></returns>
        public override double Area()
        {
            double s = Perimeter() / 2;
            return Math.Sqrt(s * (s - Vertices[0].Distance(Vertices[1])) * 
                        (s - Vertices[1].Distance(Vertices[2])) * 
                        (s - Vertices[2].Distance(Vertices[0])));
        }

        /// <summary>
        /// bool Virtual method to determine if this is a scalene triangle
        /// </summary>
        /// <returns></returns>
        public bool IsScalene()
        {
            if (GetAmountEqualSides() < 2)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// bool Virtual method to determine if this is an isosceles triangle
        /// </summary>
        /// <returns></returns>
        public bool IsIsosceles()
        {
            if (GetAmountEqualSides() > 2)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// bool Virtual method to determine if this is an equilateral triangle 
        /// </summary>
        /// <returns></returns>
        public bool IsEquilateral()
        {
            if(GetAmountEqualSides() == 3)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// String representation of the shape object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Triangle: " + base.ToString();
        }

        private int GetAmountEqualSides()
        {
            int equalSides = 0;
            if (Utils.IsRelativelyEqual(
                    Vertices[0].Distance(Vertices[1]), 
                    Vertices[1].Distance(Vertices[2])))
            {
                equalSides++;
            }

            if(Utils.IsRelativelyEqual(
                    Vertices[1].Distance(Vertices[2]), 
                    Vertices[2].Distance(Vertices[0])))
            {
                equalSides++;
            }
            if(Utils.IsRelativelyEqual(
                    Vertices[2].Distance(Vertices[0]), 
                    Vertices[0].Distance(Vertices[1])))
            {
                equalSides++;
            }
            return equalSides;
        }
    }
}
