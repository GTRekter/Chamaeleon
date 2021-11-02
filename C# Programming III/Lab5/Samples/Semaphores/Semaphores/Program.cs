using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Collections.Concurrent;

namespace Semaphores
{
	class Customer
	{
		public int CustomerID { get; set; }
		public decimal Amount { get; set; }
	}

	class Program
	{
		// Total number of tellers
		private const int NUM_TELLERS = 5;
		// Number of tellers that can assist customers simultaneously
		private const int NUM_ASSIST = 3;
		// Total number of customers
		private const int NUM_CUSTOMERS = 20;

		// A semaphore that simulates a limited resource pool.
		private static Semaphore m_TellerPool;
		private static ManualResetEvent m_Quit = new ManualResetEvent(false);
		private static ConcurrentQueue<Customer> m_Customers = new ConcurrentQueue<Customer>();

		static void Main(string[] args)
		{
			Console.WriteLine("Press ENTER to start...");
			Console.ReadLine();
			// Create a teller pool that can manage up to NUM_ASSIST customers.
			// Initially, no tellers available (0).
			m_TellerPool = new Semaphore(0, NUM_ASSIST);

			// Create a queue of customers
			Random rnd = new Random();
			for (int i = 0; i < NUM_CUSTOMERS; i++)
			{
				m_Customers.Enqueue(new Customer() { CustomerID = i, Amount = rnd.Next(100, 1000) });
			}
			Console.WriteLine("{0} customers created", NUM_CUSTOMERS);

			// Create a thread for every teller
			for (int i = 0; i < NUM_TELLERS; i++)
			{
				Thread t = new Thread(new ParameterizedThreadStart(TellerThread));
				t.Start(i);
			}
			Console.WriteLine("{0} teller threads created", NUM_TELLERS);

			// Wait for half a second, to allow all the
			// threads to start and to block on the semaphore.
			Thread.Sleep(500);

			// Release NUM_ASSIST tellers to help customers
			Console.WriteLine("Release {0} tellers", NUM_ASSIST);
			m_TellerPool.Release(NUM_ASSIST);

			Console.WriteLine("Waiting for all customers to be helped...");

			while (m_Customers.Count > 0)
			{
				Thread.Sleep(100);
			}
			
			// Signal to all tellers it is break time
			Console.WriteLine("Signal to all tellers it is break time...");
			m_Quit.Set();

			Console.WriteLine("Press ENTER to exit...");
			Console.ReadLine();
		}

		private static void TellerThread(object obj)
		{
			int tellerNum = (int)obj;
			Console.WriteLine("Teller {0} ready", tellerNum);
			
			// Check to see if the signal to quit has been sent
			while (!m_Quit.WaitOne(1))
			{
				// Wait 1/2 second trying to get a customer
				if (m_TellerPool.WaitOne(500))
				{
					// Check if there is a customer ready
					Customer customer = null;
					if (m_Customers.TryDequeue(out customer))
					{
						Console.WriteLine("Teller {0} helping customer {1} deposit {2:C}", tellerNum, customer.CustomerID, customer.Amount);
						Thread.Sleep(1000);  // Take some time for the transaction
					}
					m_TellerPool.Release();
				}
			}
			Console.WriteLine("Teller {0} on break", tellerNum);
		}
	}
}
