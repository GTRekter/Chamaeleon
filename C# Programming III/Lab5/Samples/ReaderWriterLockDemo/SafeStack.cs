using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ReaderWriterLockDemo
{
	class SafeStack<T>
	{
		private List<T> m_Items = new List<T>();
		private ReaderWriterLockSlim m_Lock = new ReaderWriterLockSlim();

		public T Pop()
		{
			T item = default(T);
			m_Lock.EnterUpgradeableReadLock();
			if (m_Items.Count > 0)
			{
				m_Lock.EnterWriteLock();
				item = m_Items.Last();
				m_Items.RemoveAt(m_Items.Count - 1);
				m_Lock.ExitWriteLock();
			}
			m_Lock.ExitUpgradeableReadLock();
			return item;
		}

		public void Push(T item)
		{
			m_Lock.EnterWriteLock();
			m_Items.Add(item);
			m_Lock.ExitWriteLock();
		}

		public T Peek()
		{
			T item = default(T);
			m_Lock.EnterReadLock();
			if (m_Items.Count > 0)
			{
				item = m_Items.Last();
			}
			m_Lock.ExitReadLock();
			return item;
		}

		public int Count
		{
			get
			{
				try
				{
					m_Lock.EnterReadLock();
					return m_Items.Count;
				}
				finally
				{
					m_Lock.ExitReadLock();
				}
			}
		}
	}
}
