using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConcurrencyCollections
{
	class Program
	{
		static Random m_Rnd = new Random();
		static ConcurrentQueue<int> m_Queue = new ConcurrentQueue<int>();
		static ConcurrentStack<int> m_Stack = new ConcurrentStack<int>();
		static ManualResetEventSlim m_Quit = new ManualResetEventSlim();

		static void Main(string[] args)
		{
			Console.Write("Press <ENTER> to start, press <ENTER> again to signal threads to quit...");
			Console.ReadLine();

			//TestQueue();
			TestStack();
			
			Console.ReadLine();
			m_Quit.Set();

			//Console.WriteLine("{0} values in the queue", m_Queue.Count);
			Console.WriteLine("{0} values in the stack", m_Stack.Count);

			Console.Write("Press <ENTER> to quit...");
			Console.ReadLine();
		}

		private static void TestQueue()
		{
			// Test Queue
			for (int i = 0; i < 2; i++)
			{
				Thread producer = new Thread(QueueProducer);
				producer.Start();
			}

			for (int i = 0; i < 3; i++)
			{
				Thread consumer = new Thread(QueueConsumer);
				consumer.Start();
			}
		}

		private static void TestStack()
		{
			// Test Stack
			for (int i = 0; i < 2; i++)
			{
				Thread producer = new Thread(StackProducer);
				producer.Start();
			}

			for (int i = 0; i < 3; i++)
			{
				Thread consumer = new Thread(StackConsumer);
				consumer.Start();
			}
		}

		private static void QueueProducer()
		{
			int counter = Thread.CurrentThread.ManagedThreadId * 1000;
			while (!m_Quit.IsSet)
			{
				string value = string.Format("==> Thread {0} enqueued value {1:N0}", 
					Thread.CurrentThread.ManagedThreadId, counter);
				m_Queue.Enqueue(counter);
				Console.WriteLine(value);
				counter++;
				Thread.Sleep(m_Rnd.Next(1000, 2000));
			}
		}

		private static void QueueConsumer()
		{
			while (!m_Quit.IsSet)
			{
				int value;
				if (m_Queue.TryDequeue(out value))
				{
					Console.WriteLine("<== Thread {0} dequeued value {1:N0}",
						Thread.CurrentThread.ManagedThreadId, value);
				}
				else
				{
					Console.WriteLine("+++ Thread {0} unable to dequeue value",
						Thread.CurrentThread.ManagedThreadId);
				}
				Thread.Sleep(m_Rnd.Next(1000, 2000));
			}
		}

		private static void StackProducer()
		{
			int counter = Thread.CurrentThread.ManagedThreadId * 1000;
			while (!m_Quit.IsSet)
			{
				string value = string.Format("==> Thread {0} pushed value {1:N0}", 
					Thread.CurrentThread.ManagedThreadId, counter);
				m_Stack.Push(counter);
				Console.WriteLine(value);
				counter++;
				Thread.Sleep(m_Rnd.Next(1000, 2000));
			}
		}

		private static void StackConsumer()
		{
			while (!m_Quit.IsSet)
			{
				int value;
				if (m_Stack.TryPop(out value))
				{
					Console.WriteLine("<== Thread {0} popped value {1:N0}", 
						Thread.CurrentThread.ManagedThreadId, value);
				}
				else
				{
					Console.WriteLine("+++ Thread {0} unable to pop value",
						Thread.CurrentThread.ManagedThreadId);
				}
				Thread.Sleep(m_Rnd.Next(1000, 2000));
			}
		}
	}
}
