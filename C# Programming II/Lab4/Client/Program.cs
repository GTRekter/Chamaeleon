using PersonLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        public static List<Person> People = new List<Person>();

        static void Main(string[] args)
        {

            Random random = new Random();

            People.Add(PersonFactory.Instance.Create(random.Next(0, 999999999), "Porta", "Ivan", new DateTime(1989,09,23)));
            People.Add(PersonFactory.Instance.Create(random.Next(0, 999999999), "Hejlsberg", "Anders", new DateTime(1960, 12, 15)));
            People.Add(PersonFactory.Instance.Create(random.Next(0, 999999999), "Martin", "Robert C.", new DateTime(1952, 04, 04)));
            People.Add(PersonFactory.Instance.Create(random.Next(0, 999999999), "Dean", "Mark", new DateTime(1954, 07, 17)));
            People.Add(PersonFactory.Instance.Create(random.Next(0, 999999999), "Samuel", "Arthur Lee", new DateTime(1959, 11, 04)));

            Person clone = People.First().Clone() as Person;
            clone.FirstName = "Han";
            clone.LastName = "Solo";
            clone.DOB = new DateTime(1900, 01, 01);
            People.Add(clone);

            Console.WriteLine(Person.GetHeader());
            foreach (Person person in People)
            {
                Console.WriteLine(person.ToString());
            }

            Console.WriteLine("Press any ket to continue");
            Console.ReadLine();
        }
    }
}
