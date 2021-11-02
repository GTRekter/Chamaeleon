using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenericsDemo
{
	class Program
	{
		static void Main(string[] args)
		{
			int x = 10;
			int y = 20;

			Console.WriteLine("X={0}, Y={1}", x, y);
			Utils.Swap<int>(ref x, ref y);
			Console.WriteLine("X={0}, Y={1}", x, y);

			double a = 12.3456;
			double b = 34.564879;

			Console.WriteLine("A={0}, B={1}", a, b);
			Utils.Swap<double>(ref a, ref b);
			Console.WriteLine("A={0}, B={1}", a, b);

			Console.WriteLine(new string('=', 60));

			GenericStack<int> stack = new GenericStack<int>();
			stack.Push(2);
			stack.Push(6);
			stack.Push(9);
			Console.WriteLine(stack.Peek());
			Console.WriteLine(stack.Pop());
			Console.WriteLine(stack.Pop());
			Console.WriteLine(stack.Pop());

			

			Console.ReadLine();
		}
	}
}
