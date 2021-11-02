using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsDemo
{
	public class PersonChangedEventArgs : EventArgs
	{
		public PersonChangedEventArgs(string message)
		{
			Message = message;
		}

		public string Message { get; set; }
	}

	public class Person
	{
		public event EventHandler<EventArgs> Changed;
		public event EventHandler<PersonChangedEventArgs> ChangedDetailed;

		private string _LastName;
		private string _FirstName;
		private DateTime _DOB;

		public Person(string lastName, string firstName, DateTime dOB)
		{
			LastName = lastName;
			FirstName = firstName;
			DOB = dOB;
		}

		public string LastName
		{
			get
			{
				return _LastName;
			}
			set
			{
				string msg = $"LastName changed from '{_LastName ?? ""}' to '{value ?? ""}'";
				_LastName = value;
				Changed?.Invoke(this, EventArgs.Empty);
				ChangedDetailed?.Invoke(this, new PersonChangedEventArgs(msg));
			}
		}

		public string FirstName
		{
			get
			{
				return _FirstName;
			}
			set
			{
				string msg = $"FirstName changed from '{_FirstName ?? ""}' to '{value ?? ""}'";
				_FirstName = value;
				Changed?.Invoke(this, EventArgs.Empty);
				ChangedDetailed?.Invoke(this, new PersonChangedEventArgs(msg));
			}
		}

		public DateTime DOB
		{
			get
			{
				return _DOB;
			}
			set
			{
				string msg = $"DOB changed from '{_DOB}' to '{value}'";
				_DOB = value;
				Changed?.Invoke(this, EventArgs.Empty);
				ChangedDetailed?.Invoke(this, new PersonChangedEventArgs(msg));
			}
		}
	}
}
