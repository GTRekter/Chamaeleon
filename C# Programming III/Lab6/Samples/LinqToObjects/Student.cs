using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqToObjects
{
	public class Student
	{
		public long ID { get; set; }
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public int Grade { get; set; }

		public Student(long id, string lastName, string firstName, int grade)
		{
			ID = id;
			LastName = lastName;
			FirstName = firstName;
			Grade = grade;
		}
	}
}
