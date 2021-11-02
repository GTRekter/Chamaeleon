using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Events
{
	public class Person
	{
		public class PersonChangedEventArgs : EventArgs
		{
			public PersonChangedEventArgs(string message)
			{
				Message = message;
			}

			public string Message { get; set; }
		}

		public delegate void PersonChangedHandler(object sender, PersonChangedEventArgs e);

		public event PersonChangedHandler PersonChanged;
		public event EventHandler SimpleChanged;

		private string _LastName;
		private string _FirstName;
		private DateTime _DOB;

		public Person(string lastName, string firstName, DateTime dob)
		{
			LastName = lastName;
			FirstName = firstName;
			DOB = dob;
		}

		public string LastName
		{
			get
			{
				return _LastName;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentNullException();
				}
				_LastName = value;
				
				PersonChanged?.Invoke(this, new PersonChangedEventArgs("LastName has changed"));
				SimpleChanged?.Invoke(this, EventArgs.Empty);
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
				_FirstName = value;
				PersonChanged?.Invoke(this, new PersonChangedEventArgs("FirstName has changed"));
				SimpleChanged?.Invoke(this, EventArgs.Empty);
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
				_DOB = value;
				PersonChanged?.Invoke(this, new PersonChangedEventArgs("DOB has changed"));
				SimpleChanged?.Invoke(this, EventArgs.Empty);
			}
		}
	}
}
