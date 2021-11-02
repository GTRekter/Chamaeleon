using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonLib
{
    public class PersonFactory
    {
        private readonly static PersonFactory _Instance = null;

        static PersonFactory()
        {
            _Instance = new PersonFactory();
        }

        private PersonFactory()
        {

        }

        public static PersonFactory Instance
        {
            get
            {
                return _Instance;
            }
        }

        public Person Create(int id, string lastName, string firstName, DateTime dob)
        {
            return new Person(id, lastName, firstName, dob);
        }
    }
}
