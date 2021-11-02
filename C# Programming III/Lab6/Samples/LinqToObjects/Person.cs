using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqToObjects
{
	public class Person
	{
		public long ID { get; set; }
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public DateTime DOB { get; set; }

		public Person(long id, string lastName, string firstName, DateTime dob)
		{
			ID = id;
			LastName = lastName;
			FirstName = firstName;
			DOB = dob;
		}

		public override string ToString()
		{
			return string.Format("{0:000-00-0000} {1:-15} {2:-15} {3:MM/dd/yyyy}", 
				ID, LastName, FirstName, DOB);
		}
	}

	public static class PersonExtensions
	{
		public static int GetAge(this Person p)
		{
			DateTime now = DateTime.Now;
			int years = now.Year - p.DOB.Year;
			if ((now.Month < p.DOB.Month) || ((now.Month == p.DOB.Month)
				  && (now.Day < p.DOB.Day)))
				years--;
			return years;
		}
	}

}
