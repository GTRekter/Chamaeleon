using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Runtime.Remoting.Messaging;

namespace ThreadPoolSample
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WindowHeight = 50;
			Console.BufferHeight = 1000;

			Console.WriteLine("Press <ENTER> to start DelegateThreads demo...");
			Console.ReadLine();
			DelegateThreads();

			Console.WriteLine();
			Console.WriteLine("Press <ENTER> to start WorkerThreads demo...");
			Console.ReadLine();
			WorkerThreads();

			Console.WriteLine();
			Console.WriteLine("Press <ENTER> to quit...");
			Console.ReadLine();
		}

		/// <summary>
		/// Demonstrates delegate thread operations
		/// </summary>
		private static void DelegateThreads()
		{
			Func<uint, BigInteger> factorial = Factorial;
			uint baseNum = 50;
			Console.WriteLine("Factorial delegate started.  Press <ENTER> to continue...");
			factorial.BeginInvoke(baseNum, FactorialDone, baseNum);
			ThreadPoolInfo();
			Console.ReadLine();
			ThreadPoolInfo();

			Console.WriteLine("========== Start delegate threads ==========");
			for (int i = 0; i < 24; i++)
			{
				int workerThreads;
				int portThreads;

				Func<int, int> del = SampleThread;
				del.BeginInvoke(i, SampleThreadDone, null);

				ThreadPool.GetAvailableThreads(out workerThreads, out portThreads);
				Console.WriteLine("Thread {0} started.  {1} available threads.", i, workerThreads);
			}
			Console.WriteLine("========== Delegate threads started ==========");
		}

		/// <summary>
		/// Demonstrates ThreadPool QueueUserWorkItem calls
		/// </summary>
		private static void WorkerThreads()
		{
			Console.WriteLine("Factorial worker thread started.  Press <ENTER> to continue...");
			ThreadPool.QueueUserWorkItem(FactorialWorker, 50);
			Console.ReadLine();

			Console.WriteLine("========== Start work items ==========");
			for (int i = 0; i < 24; i++)
			{
				int workerThreads;
				int portThreads;

				ThreadPool.QueueUserWorkItem(WorkItem, i);

				ThreadPool.GetAvailableThreads(out workerThreads, out portThreads);
				Console.WriteLine("Work item {0} started.  {1} available threads.", i, workerThreads);
			}
			Console.WriteLine("========== Work items started ==========");

		}

		/// <summary>
		/// Factorial method
		/// </summary>
		/// <param name="value">Value to calculate factorial for</param>
		/// <returns>Factorial result or 0 if invalid</returns>
		static BigInteger Factorial(uint value)
		{
			if (value > 0)
			{
				BigInteger fact = 1;
				for (uint i = 1; i <= value; i++)
				{
					fact *= i;
				}
				// Artificial delay
				Thread.Sleep(4000);
				return fact;
			}
			return 0;
		}

		/// <summary>
		/// Callback method for Factorial delegate completion
		/// </summary>
		/// <param name="iar">Carries the delegate state</param>
		static void FactorialDone(IAsyncResult iar)
		{
			AsyncResult ar = (AsyncResult)iar;
			// ar.AsyncState is the last parameter passed to BeginInvoke
			uint baseNum = (uint)iar.AsyncState;
			// ar.AsyncDelegate contains the delegate that was executed
			Func<uint, BigInteger> del = (Func<uint, BigInteger>)ar.AsyncDelegate;
			// To get the delegate's result, call del.EndInvoke, passing it iar
			Console.WriteLine("[Async] {0}! = {1}", baseNum, del.EndInvoke(iar));
		}

		/// <summary>
		/// Calculates factorial of a value and outputs the result
		/// </summary>
		/// <param name="value">Value to calculate factorial for</param>
		static void FactorialWorker(object value)
		{
			int baseNum = (int)value;
			if (baseNum > 0)
			{
				BigInteger fact = 1;
				for (uint i = 1; i <= baseNum; i++)
				{
					fact *= i;
				}
				// Artificial delay
				Thread.Sleep(4000);
				Console.WriteLine("[Worker] {0}! = {1}", baseNum, fact);
			}
			else
			{
				Console.WriteLine(0);
			}
		}

		/// <summary>
		/// Sample thread process
		/// </summary>
		/// <param name="id">Given ID</param>
		/// <returns>The ID passed in</returns>
		static int SampleThread(int id)
		{
			long l = 0;
			for (int i = 0; i < 100000000; i++)
			{
				l += 1;
			}
			return id;
		}

		/// <summary>
		/// Called when SampleThread is done
		/// </summary>
		/// <param name="iar">State information</param>
		static void SampleThreadDone(IAsyncResult iar)
		{
			AsyncResult ar = (AsyncResult)iar;
			Func<int, int> del = (Func<int, int>)ar.AsyncDelegate;
			Console.WriteLine("Thread {0} is done.", del.EndInvoke(iar));
		}


		/// <summary>
		/// Method to demonstrate a worker thread
		/// </summary>
		/// <param name="state">Contains the value passed from call to ThreadPool.QueueUserWorkItem</param>
		static void WorkItem(object state)
		{
			long l = 0;
			for (int i = 0; i < 100000000; i++)
			{
				l += 1;
			}
			Console.WriteLine("Work item {0} is done.", (int)state);
		}

		/// <summary>
		/// Prints info about thread resources
		/// </summary>
		static void ThreadPoolInfo()
		{
			int workerThreads;
			int portThreads;

			ThreadPool.GetMaxThreads(out workerThreads, out portThreads);
			Console.WriteLine("\nMaximum worker threads: \t{0}" +
				"\nMaximum completion port threads: {1}",
				workerThreads, portThreads);

			ThreadPool.GetAvailableThreads(out workerThreads,
				out portThreads);
			Console.WriteLine("\nAvailable worker threads: \t{0}" +
				"\nAvailable completion port threads: {1}\n",
				workerThreads, portThreads);

		}
	}
}
