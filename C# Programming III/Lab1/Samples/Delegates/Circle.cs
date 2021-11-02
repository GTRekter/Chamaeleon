using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates
{
	public delegate void PrintAreaDelegate();

	public class Circle
	{
		public double Radius { get; set; }

		public Circle() : this(0)
		{

		}

		public Circle(double radius)
		{
			Radius = radius;
		}

		public double Area()
		{
			return Math.PI * Radius * Radius;
		}

		public void PrintArea()
		{
			Console.WriteLine($"Circle with radius {Radius} has an area of {Area()}");
		}
	}
}
