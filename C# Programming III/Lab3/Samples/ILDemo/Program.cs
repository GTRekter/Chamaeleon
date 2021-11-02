using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ILDemo
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("x + y = {0}", Add(5, 6));

			Console.ReadLine();
		}

		static int Add(int x, int y)
		{
			return x + y;
		}
	}
}
