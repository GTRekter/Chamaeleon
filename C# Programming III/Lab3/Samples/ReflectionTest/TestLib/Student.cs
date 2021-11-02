using System;

namespace TestLib
{
	public class Student
	{
		private DateTime m_DOB = DateTime.MinValue;
		private string m_FirstName = string.Empty;
		private string m_LastName = string.Empty;

		public DateTime DOB
		{
			get { return m_DOB; }
			set { m_DOB = value; }
		}

		public string FirstName
		{
			get { return m_FirstName; }
			set { m_FirstName = value; }
		}

		public string LastName
		{
			get { return m_LastName; }
			set { m_LastName = value; }
		}

		public Student(string lastName, string firstName, DateTime dob)
		{
			LastName = lastName;
			FirstName = firstName;
			DOB = dob;
		}

		public override string ToString()
		{
			return string.Format("{0:MM/dd/yyyy}: {1}, {2}", DOB, LastName, FirstName);
		}
	}
}
