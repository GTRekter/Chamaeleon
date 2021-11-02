using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates
{
	class Program
	{
		public delegate double AreaDelegate(double radius);
		public delegate double CircleAreaDelegate();

		static void Main(string[] args)
		{
			double area = CircleArea(12.34);
			AreaDelegate del = CircleArea;
			area = del(12.34);

			area = del.Invoke(12.34);

			TestMe(del, 111);

			Circle c = new Circle(54.32);
			Console.WriteLine($"Area: {c.Area()}");

			CircleAreaDelegate del2 = c.Area;
			area = del2();

			Circle c2 = new Circle(11);

			PrintAreaDelegate printDel = c.PrintArea;
			printDel += c2.PrintArea;

			Circle c3 = new Circle(22);
			printDel += c3.PrintArea;

			printDel();
			Console.WriteLine();

			printDel -= c.PrintArea;
			printDel();

			Func<double> funcArea = c.Area;
			area = funcArea();

			Action actionPrintArea = c.PrintArea;
			actionPrintArea();


			Console.ReadLine();
		}

		public static double CircleArea(double radius)
		{
			return Math.PI * radius * radius;
		}

		public static void TestMe(AreaDelegate del, double radius)
		{
			Console.WriteLine($"Area: {del(radius)}");
		}
	}
}
