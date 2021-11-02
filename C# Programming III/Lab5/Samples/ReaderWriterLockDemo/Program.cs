using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ReaderWriterLockDemo
{
	class Program
	{
		static bool m_Quit = false;
		static Random m_Rnd = new Random();

		static void Main(string[] args)
		{
			SafeStack<int> stack = new SafeStack<int>();

			Thread producer = new Thread(ProducerThread);
			producer.Start(stack);

			for (int i = 0; i < 4; i++)
			{
				Thread consumer = new Thread(ConsumerThread);
				consumer.Name = "Consumer " + i.ToString();
				consumer.Start(stack);
			}

			Thread.Sleep(1000);
			while ((stack.Count > 0) || (producer.IsAlive))
			{
				Thread.Sleep(100);
			}
			m_Quit = true;  // Signal consumers

			Console.WriteLine("Press <ENTER> to quit...");
			Console.ReadLine();
		}

		static void ProducerThread(object obj)
		{
			SafeStack<int> stack = obj as SafeStack<int>;
			for (int i = 0; i < 20; i+=4)
			{
				stack.Push(i);
				Console.WriteLine("Push: {0}", i);
				stack.Push(i + 1);
				Console.WriteLine("Push: {0}", i + 1);
				stack.Push(i + 2);
				Console.WriteLine("Push: {0}", i + 2);
				stack.Push(i + 3);
				Console.WriteLine("Push: {0}", i + 3);
				Thread.Sleep(500);
			}
		}

		static void ConsumerThread(object obj)
		{
			SafeStack<int> stack = obj as SafeStack<int>;
			while (!m_Quit)
			{
				if (stack.Count > 0)
				{
					int item = stack.Pop();
					Console.WriteLine("{0} popped: {1}", Thread.CurrentThread.Name, item);
				}
				Thread.Sleep(m_Rnd.Next(1000, 3000));
			}
			Console.WriteLine("{0} quitting", Thread.CurrentThread.Name);
		}
	}
}
