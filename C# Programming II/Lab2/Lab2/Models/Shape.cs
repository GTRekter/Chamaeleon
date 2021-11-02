using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Models
{
    public abstract class Shape
    {
        /// <summary>
        /// Protected list of Point objects 
        /// </summary>
        /// <typeparam name="Point"></typeparam>
        /// <returns></returns>
        protected List<Point> _Vertices = new List<Point>();

        /// <summary>
        /// Public, virtual property encapsulating _Vertices
        /// </summary>
        /// <typeparam name="Point"></typeparam>
        /// <returns></returns>
        public virtual List<Point> Vertices
        {
            get
            {
                return _Vertices; 
            }
            set
            {
                _Vertices = value;
            }
        }

        /// <summary>
        /// Ctor Default
        /// </summary>
        public Shape()
        {
        }

        /// <summary>
        /// Ctor Parameterized
        /// </summary>
        /// <param name=""></param>
        public Shape(List<Point> vertices)
        {
            Vertices = vertices;
        }

        /// <summary>
        /// double Abstract method to calculate area of the shape
        /// </summary>
        public abstract double Area();

        /// <summary>
        /// double Virtual method to calculate perimeter of the shape
        /// </summary>
        /// <returns></returns>
        public virtual double Perimeter()
        {
            double perimeter = 0;

            for (int i = 0; i < Vertices.Count; i++)
            {
                if(i != Vertices.Count - 1)
                {
                    perimeter += Vertices[i].Distance(Vertices[i+1]);
                }
                else
                {
                    perimeter += Vertices[i].Distance(Vertices[0]);
                }
            }

            return perimeter;
        }

        /// <summary>
        /// String representation of the shape object
        /// </summary>
        public override string ToString()
        {
            return string.Join(",", Vertices.Select(v => string.Format("({0},{1})", v.X, v.Y)));
        }
    }
}
