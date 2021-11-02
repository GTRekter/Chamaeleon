using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MemoryConcepts
{
	class Program
	{
		static void Main(string[] args)
		{
			// Since p1 is not explicitly disposed, it will be cleaned up 
			// by the garbage collector when the program terminates
			Person p1 = new Person(1, "Smith", "Jim", DateTime.Parse("5/15/1968"));
			p1 = null;  // Null does not force immediate disposal

			// p2 is explicitly disposed so it will be cleaned up on the main thread
			Person p2 = new Person(2, "Jones", "Jane", DateTime.Parse("2/12/1955"));
			p2.Dispose();

			// p3 is also explicitly disposed when the using clause terminates
			using (Person p3 = new Person(3, "Davis", "Patrick", DateTime.Parse("8/5/1988")))
			{
				Console.WriteLine(p3);
			}  // p3.Dispose();

			Console.WriteLine("{0}Program ending...{0}", Environment.NewLine);
		}
	}
}
