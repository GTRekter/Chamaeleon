using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FPDemo
{
	class Program
	{
		static void Main(string[] args)
		{
			int[] numbers = { 1, 2, 3, 4, 5, 6, 10, 11, 17, 19, 23 };
			foreach (int i in numbers.Where(IsOdd))
			{
				Console.WriteLine(i);
			}

			double[] doubles = { 1.23, -4.56, 7.89, -3.14, 0 };
			foreach (double d in doubles.Where(IsPositive))
			{
				Console.WriteLine(d);
			}

			Console.WriteLine(new string('=', 20));

			Func<double, double> del = delegate(double radius)
			{
				return Math.PI * radius * radius;
			};

			Console.WriteLine("Area: {0}", del(12.34));

			Console.WriteLine(new string('=', 20));

			Func<int, int> multiplyBy2 = (x) => x * 2;
			int num = 10;
			Console.WriteLine("{0} doubled = {1}", num, multiplyBy2(num));

			Func<double, double, double> multiplyTogether = (x, y) => x * y;
			double val1 = 12.34;
			double val2 = 56.78;
			Console.WriteLine("{0} * {1} = {2}", 
				val1, val2, multiplyTogether(val1, val2));

			Console.WriteLine(new string('=', 20));

			Func<string, string, int> compareTo = (string str1, string str2) =>
			{
				if (str1 == null && str2 == null) return 0;
				if (str1 == null) return -1;
				if (str2 == null) return 1;
				return str1.CompareTo(str2);
			};

			Console.WriteLine("'{0}' compared to '{1}': {2}",
				"abc", "123", compareTo("abc", "123"));


			Console.ReadLine();
		}

		static bool IsPositive(double input)
		{
			return input >= 0;
		}

		static bool IsOdd(int input)
		{
			return input % 2 == 1;
		}
	}
}
