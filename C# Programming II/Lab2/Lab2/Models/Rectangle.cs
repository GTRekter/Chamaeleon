using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Models
{
    public class Rectangle : Shape
    {
        private double height;

        private double width;

        /// <summary>
        /// The Height property is similar to Width except we are interested in different values 
        /// from Utils.GetBoundingBox().  
        /// In this case, the minimum Y value is bounds.Item2 and the maximum Y value is bounds.Item4. 
        /// </summary>
        public double Height
        {
            get
            {
                return height;
            }
        }

        /// <summary>
        /// The Width property contains only the get accessor which returns the width of the rectangle.  
        /// Since we do not know the order of the points in the Vertices list 
        /// we can use Utils.GetBoundingBox() to determine the extents of the Rectangle.  
        /// Pass Vertices to Utils.GetBoundingBox() and the result is a Tuple containing four 
        /// doubles (for simplicity you may want to declare the result variable as var).  
        /// Assuming the result variable is called bounds, the minimum X value of the Rectangle 
        /// is bounds.Item1 and the maximum X value is bounds.Item3.  
        /// Use these two values to calculate the width. 
        /// </summary>
        public double Width
        {
            get
            {
                return width;
            }
        }

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
                base._Vertices = value;
            }
        }

        /// <summary>
        /// Ctor Parameterized
        /// </summary>
        /// <param name=""></param>
        public Rectangle(List<Point> vertices) 
        {
            Vertices = Normalize(vertices);

            var bounds = Utils.GetBoundingBox(Vertices);
            width = bounds.Item3 - bounds.Item1;
            height = bounds.Item4 - bounds.Item2;
        }

        /// <summary>
        /// Ctor Parameterized
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        public Rectangle(Point v1, Point v2)
        {
            Vertices = Normalize(new List<Point>() { v1, v2 });

            var bounds = Utils.GetBoundingBox(Vertices);
            width = bounds.Item3 - bounds.Item1;
            height = bounds.Item4 - bounds.Item2;
        }

        /// <summary>
        /// Ctor Parameterized
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="v3"></param>
        /// <param name="v4"></param>
        public Rectangle(Point v1, Point v2, Point v3, Point v4) 
            : base(new List<Point>() { v1, v2, v3, v4 })
        { 
            Vertices = Normalize(new List<Point>() { v1, v2, v3, v4 });

            var bounds = Utils.GetBoundingBox(Vertices);
            width = bounds.Item3 - bounds.Item1;
            height = bounds.Item4 - bounds.Item2;
        }

        public override double Area()
        {
            return (Width * Height);
        }

        public virtual bool IsSquare()
        {
            if(Width == Height)
            {
                return true;
            }
            return false;
        }

        public List<Point> Normalize(List<Point> points)
        {
            List<Point> verticle = new List<Point>();

            // minX, minY, maxX, maxY
            var bounds = Utils.GetBoundingBox(points);
            verticle.Add(new Point(bounds.Item1, bounds.Item2));
            verticle.Add(new Point(bounds.Item1, bounds.Item4));
            verticle.Add(new Point(bounds.Item3, bounds.Item4));
            verticle.Add(new Point(bounds.Item3, bounds.Item2));

            return verticle;
        }

        /// <summary>
        ///  This can be split into two triangles with vertices (a, b, c) and (c, d, a).
        /// </summary>
        /// <returns></returns>
        public List<Triangle> ToTriangles()
        {
            List<Triangle> triangles = new List<Triangle>();
            triangles.Add(new Triangle(Vertices[0], Vertices[1], Vertices[2]));
            triangles.Add(new Triangle(Vertices[2], Vertices[3], Vertices[0]));

            return triangles;
        }

        /// <summary>
        /// String representation of the shape object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Rectangle: " + base.ToString();
        }
    }
}
