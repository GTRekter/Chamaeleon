using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MonitorSample
{
	public class ThreadSafeQueue<T>
	{
		private List<T> m_Items = new List<T>();

		/// <summary>
		/// Enqueues an item
		/// </summary>
		/// <param name="item">Item to enqueue</param>
		/// <remarks>This method uses the Monitor.Enter/Exit alias 'lock'</remarks>
		public void Enqueue(T item)
		{
			lock (m_Items)
			{
				m_Items.Add(item);
				// Signal the next thread in line waiting for the lock. 
				// Upon receiving the pulse, the waiting thread is moved
				// to the ready queue and can reacquire the lock.
				Monitor.Pulse(m_Items);
			} 
		}

		/// <summary>
		/// Dequeues an item
		/// </summary>
		/// <param name="timeout">Time in ms to wait to dequeue an item</param>
		/// <returns>Dequeued item</returns>
		/// <remarks>This method waits a given time for an item to dequeue</remarks>
		/// <exception cref="TimeoutException">Thrown if timeout elapsed waiting for item to be enqueued</exception>
		public T Dequeue(int timeout = 1000)
		{
			Monitor.Enter(m_Items);
			try
			{
				bool valueFound = true;
				if (m_Items.Count == 0)
				{
					// Releases the lock & blocks until it reacquires the lock.
					valueFound = Monitor.Wait(m_Items, timeout);
				}
				if (valueFound)
				{
					T item = m_Items[0];
					m_Items.RemoveAt(0);
					return item;
				}
			}
			finally
			{
				Monitor.Exit(m_Items);
			}
			throw new TimeoutException("Timeout waiting to dequeue item");
		}

		/// <summary>
		/// Gets the count of the items in the Queue
		/// </summary>
		public int Count
		{
			get
			{
				return m_Items.Count;
			}
		}
	}
}
