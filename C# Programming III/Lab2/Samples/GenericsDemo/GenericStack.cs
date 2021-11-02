using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenericsDemo
{
	public class GenericStack<T>
	{
		private List<T> m_Stack = new List<T>();

		public int Count
		{
			get
			{
				return m_Stack.Count;
			}
		}

		public void Clear()
		{
			m_Stack.Clear();
		}

		public void Push(T item)
		{
			m_Stack.Add(item);
		}

		public T Pop()
		{
			if (m_Stack.Count > 0)
			{
				T item = m_Stack.Last();
				m_Stack.RemoveAt(m_Stack.Count - 1);
				return item;
			}
			throw new IndexOutOfRangeException();
		}

		public T Peek()
		{
			if (m_Stack.Count > 0)
			{
				T item = m_Stack.Last();
				return item;
			}
			throw new IndexOutOfRangeException();
		}
	}
}
