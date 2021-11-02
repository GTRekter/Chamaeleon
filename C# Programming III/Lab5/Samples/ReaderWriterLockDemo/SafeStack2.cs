using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ReaderWriterLockDemo
{
	class SafeStack2<T>
	{
		private List<T> m_Items = new List<T>();
		private ReaderWriterLockSlim m_Lock = new ReaderWriterLockSlim();

		public T Pop()
		{
			T item = default(T);
			if (m_Lock.TryEnterUpgradeableReadLock(1000))
			{
				try
				{
					if (m_Items.Count > 0)
					{
						if (m_Lock.TryEnterWriteLock(1000))
						{
							try
							{
								item = m_Items.Last();
								m_Items.RemoveAt(m_Items.Count - 1);
							}
							finally
							{
								m_Lock.ExitWriteLock();
							}
						}
						else
						{
							throw new TimeoutException();
						}
					}
					else
					{
						throw new IndexOutOfRangeException();
					}
				}
				finally
				{
					m_Lock.ExitUpgradeableReadLock();
				}
			}
			return item;
		}

		public void Push(T item)
		{
			if (m_Lock.TryEnterWriteLock(1000))
			{
				try
				{
					m_Items.Add(item);
				}
				finally
				{
					m_Lock.ExitWriteLock();
				}
			}
		}

		public T Peek()
		{
			T item = default(T);
			if (m_Lock.TryEnterReadLock(1000))
			{
				try
				{
					if (m_Items.Count > 0)
					{
						item = m_Items.Last();
					}
					else
					{
						throw new IndexOutOfRangeException();
					}
				}
				finally
				{
					m_Lock.ExitReadLock();
				}
			}
			return item;
		}

		public int Count
		{
			get
			{
				if (m_Lock.TryEnterReadLock(1000))
				{
					try
					{
						return m_Items.Count;
					}
					finally
					{
						m_Lock.ExitReadLock();
					}
				}
				throw new TimeoutException();
			}
		}
	}
}
