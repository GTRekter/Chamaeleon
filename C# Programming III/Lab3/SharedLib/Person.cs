using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib
{
    [SpecialClass(1)]
    public class Person
    {
        public Person()
        {
            LastName = string.Empty;
            FirstName = string.Empty;
            DOB = DateTime.Now;
            Gender = Genders.Unknown;
        }

        public Person(string lastName, string firstName, DateTime dob, Genders gender)
        {
            LastName = lastName;
            FirstName = firstName;
            DOB = dob;
            Gender = gender;
        }

        public override string ToString()
        {
            return string.Format("{0,-10}{1,-10}{2,-4}{3,-10}", LastName, FirstName, DOB, Gender);
        }

        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime DOB { get; set; }
        public Genders Gender { get; set; }

        public enum Genders
        {
            Unknown,
            Male,
            Female,
            Other
        };
    }
}
