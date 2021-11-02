using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonLib
{
    public class Person : ICloneable
    {
        public int ID { get; private set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public DateTime DOB { get; set; }

        /// <summary>
        /// The fully-parameterized constructor which accepts parameters for each property must be marked 
        /// internal to prevent creation of objects outside this project. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="dob"></param>
        internal Person(int id, string lastName, string firstName, DateTime dob)
        {
            ID = id;
            LastName = lastName;
            FirstName = firstName;
            DOB = dob;
        }

        /// <summary>
        /// A public copy constructor. 
        /// </summary>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// method that returns the age of the person in years.
        /// </summary>
        /// <returns></returns>
        public int GetAge()
        {
            return DateTime.Now.Year - DOB.Year;
        }

        /// <summary>
        /// returns the person’s ID(last 4 digits only) in the following format: XXX-XX-1234 
        /// </summary>
        /// <returns></returns>
        public string GetFormattedID()
        {
            string IdString = ID.ToString();

            // Another possibility was to check if the integer was highter than 999
            if(IdString.Length > 4)
            {
                return string.Format("XXX-XX-{0:F2}", IdString.Substring(IdString.Length - 4, 4));
            }
            else
            {
                return string.Format("XXX-XX-{0:F2}", IdString.Substring(IdString.Length - 4, IdString.Length).PadLeft(4, '#'));              
            }
            
        }

        /// <summary>
        /// override that displays the person information in a consistent, tabular format.
        /// The ID displayed should only show the last 4 digits(see GetFormattedID()).  
        /// The output should also contain the age of the person in close proximity to the DOB field.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0,-12} {1,-10} {2,-10} {3,-11} {4,-4}", GetFormattedID(), LastName, FirstName, DOB.ToString("yyyy-MM-dd"), GetAge());
        }

        /// <summary>
        /// A static method called GetHeader() that displays a header appropriate for the output of ToString(). 
        /// </summary>
        /// <returns></returns>
        public static string GetHeader()
        {
            return string.Format("{0,-12} {1,-10} {2,-10} {3,-11} {4,-4}", "ID", "Last Name", "First Name", "DOB", "Age");
        }
    }
}
