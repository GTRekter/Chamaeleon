using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MonitorSample
{
	class MonitorSample
	{
		#region Fields
		const int MAX_LOOP_TIME = 20;
		private static ThreadSafeQueue<int> m_Queue = null;
		private static bool m_Done = false;
		#endregion Fields

		/// <summary>
		/// Thread that enqueues a number of values
		/// </summary>
		private static void EnqueueThread()
		{
			for (int i = 0; i < MAX_LOOP_TIME; i++)
			{
				Console.WriteLine("NQ: {0}", i);
				m_Queue.Enqueue(i);
				Thread.Sleep(500);
			}
			m_Done = true;
		}

		/// <summary>
		/// Thread that dequeues until all items have been processed
		/// </summary>
		public static void DequeueThread()
		{
			while (!m_Done || m_Queue.Count > 0)
			{
				try
				{
					Console.WriteLine("DQ: {0}", m_Queue.Dequeue());
				}
				catch (TimeoutException ex)
				{
					Console.WriteLine("** {0}", ex.Message);
				}
			}
		}

		static void Main(string[] args)
		{
			Console.Write("Press Enter to Start...");
			Console.ReadLine();

			m_Queue = new ThreadSafeQueue<int>();

			// Create the first thread.
			Thread enqueueThread = new Thread(new ThreadStart(EnqueueThread));
			// Create the second thread.
			Thread dequeueThread = new Thread(new ThreadStart(DequeueThread));
			
			// Start threads - Start the DQ thread first to demonstrate how it waits for NQ thread to add something.
			Console.WriteLine("Start DQ Thread");
			dequeueThread.Start();
			Thread.Sleep(3000);
			Console.WriteLine("Start NQ Thread");
			enqueueThread.Start();
			
			// Wait to the end of the two threads
			Console.WriteLine("Wait for Threads");
			enqueueThread.Join();
			dequeueThread.Join();		
	
			// Print the number of queue elements.
			Console.Write("Press Enter to Quit...");
			Console.ReadLine();
		}
	}
}
