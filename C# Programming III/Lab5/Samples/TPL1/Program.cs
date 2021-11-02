using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace TPL
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.ReadLine();

			string sep = new string('=', 30);
			Console.WriteLine("{0} ParallelForEach {0}", sep);
			ParallelForEach();
			Console.WriteLine();

			Console.WriteLine("{0} ParallelFor {0}", sep);
			ParallelFor();
			Console.WriteLine();

			Console.WriteLine("{0} Parallel1 {0}", sep);
			Parallel1();
			Console.WriteLine();

			Console.WriteLine("{0} Parallel2 {0}", sep);
			Parallel2();
			Console.WriteLine();

			Console.Write("Press <ENTER> to quit...");
			Console.ReadLine();
		}

		/// <summary>
		/// Demonstrates processing multiple threads within a foreach loop
		/// </summary>
		static void ParallelForEach()
		{
			// A simple source for demonstration purposes. Modify this path as necessary.
			string[] files = System.IO.Directory.GetFiles(@"C:\Users\Public\Pictures\Sample Pictures", 
				"*.jpg");
			string newDir = @"C:\Users\Public\Pictures\Sample Pictures\Modified";
			System.IO.Directory.CreateDirectory(newDir);

			// Method signature: Parallel.ForEach(IEnumerable<TSource> source, Action<TSource> body)
			// In this case:     Parallel.ForEach(IEnumerable<string> source, Action<string> body)
			// The code following the Parallel.ForEach call will not continue until all contained threads complete.
			Parallel.ForEach(files, currentFile =>
				{
					// The more computational work you do here, the greater 
					// the speedup compared to a sequential foreach loop.
					string filename = System.IO.Path.GetFileName(currentFile);
					System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(currentFile);

					bitmap.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
					bitmap.Save(System.IO.Path.Combine(newDir, filename));

					// Peek behind the scenes to see how work is parallelized.
					// But be aware: Thread contention for the Console slows down parallel loops!!!
					Console.WriteLine("Processing {0} on thread {1}", filename,
										Thread.CurrentThread.ManagedThreadId);
				} //close lambda expression
			); //close method invocation

			Console.WriteLine("Processing complete.");
		}

		/// <summary>
		/// Demonstrates processing multiple threads within a for loop
		/// </summary>
		static void ParallelFor()
		{
			int n = 10;

			// Using a concrete method
			Parallel.For(0, n, DoubleMe);
			Console.WriteLine(new string('-', 10));

			// Using an anonymous method
			Parallel.For(0, n, delegate(int i)
			{
				Console.WriteLine("{2,2}: 2 x {0} = {1}", i, 2 * i, Thread.CurrentThread.ManagedThreadId);
				Thread.Sleep(100);
			});
			Console.WriteLine(new string('-', 10));

			// Using lambdas
			Parallel.For(0, n, i =>
			{
				Console.WriteLine("{2,2}: 2 x {0} = {1}", i, 2 * i, Thread.CurrentThread.ManagedThreadId);
				Thread.Sleep(100);
			});
		}

		/// <summary>
		/// Method that doubles the input and reports the result to the Console
		/// </summary>
		/// <param name="input">Value to double</param>
		static private void DoubleMe(int input)
		{
			Console.WriteLine("{2,2}: 2 x {0} = {1}", input, 2 * input, Thread.CurrentThread.ManagedThreadId);
			Thread.Sleep(100);
		}


		#region Parallel1 Example
		static ConcurrentQueue<int> m_Bases = new ConcurrentQueue<int>();

		/// <summary>
		///  Demonstrates Parallel.Invoke with multiple calls to a concrete method
		/// </summary>
		static void Parallel1()
		{
			m_Bases.Enqueue(2);
			m_Bases.Enqueue(3);
			m_Bases.Enqueue(4);
			// Parallel.Invoke will block until all threads are done
			Parallel.Invoke(Multiples, Multiples, Multiples);
			Console.WriteLine("Parallel methods done.");
		}

		static void Multiples()
		{
			int baseNum;
			if (m_Bases.TryDequeue(out baseNum))
			{
				for (int i = 1; i <= 20; i++)
				{
					Console.WriteLine("{0} x {1} = {2}", baseNum, i, baseNum * i);
					Thread.Sleep(100);
				}
			}
		}
		#endregion Parallel1 Example

		#region Parallel2 Example
		/// <summary>
		///  Demonstrates Parallel.Invoke with different method types
		/// </summary>
		static void Parallel2()
		{
			Parallel.Invoke(
				// Static method	  
				MethodA,
				// λ expression
				() =>
				{
					Console.WriteLine("(B) Thread ID: {0}", Thread.CurrentThread.ManagedThreadId);
				},
				// Delegate method
				delegate()
				{
					Console.WriteLine("(C) Thread ID: {0}", Thread.CurrentThread.ManagedThreadId);
				}
			);
		}

		static void MethodA()
		{
			Console.WriteLine("(A) Thread ID: {0}", Thread.CurrentThread.ManagedThreadId);
		}
		#endregion Parallel2 Example
	}
}
