using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ReflectionTest
{
	[Serializable]
	class Person
	{
		public event EventHandler<EventArgs> Changed;
		public event EventHandler<CustomEventArgs> CustomEvent;

		private string m_FirstName = string.Empty;
		private string m_LastName = string.Empty;
		
		public string FirstName
		{
			get { return m_FirstName; }
			set	{ m_FirstName = value; }
		}

		public string LastName
		{
			get	{ return m_LastName; }
			set	{ m_LastName = value; }
		}

		public Person(string lastName, string firstName)
		{
			LastName = lastName;
			FirstName = firstName;
		}

		public override string ToString()
		{
			return string.Format("{0}, {1}", LastName, FirstName);
		}

		public class CustomEventArgs: EventArgs
		{
		
		}

	}
}
