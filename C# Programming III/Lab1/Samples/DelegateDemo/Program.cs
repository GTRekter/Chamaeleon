using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DelegateDemo
{
	class Program
	{
		delegate double CircleAreaDelegate(double r);

		static void Main(string[] args)
		{
			Console.WriteLine(CircleArea(12.34));

			CircleAreaDelegate del = CircleArea;

			Console.WriteLine(del(12.34));
			Console.WriteLine(del.Invoke(12.34));

			Func<double, double> del2 = CircleArea;

			Console.WriteLine(del2(12.34));

			Action<string> del3 = PrintMe;
			del3("This is a test");


			Console.ReadLine();
		}

		static double CircleArea(double radius)
		{
			return Math.PI * radius * radius;
		}

		static void PrintMe(string input)
		{
			Console.WriteLine(input);
		}
	}
}
