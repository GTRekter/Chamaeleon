using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;

namespace LinqToObjects
{
	class Program
	{
		private static Random m_Random = new Random();
		private static List<string> m_FirstNames = new List<string>();
		private static List<string> m_LastNames = new List<string>();

		static Program()
		{
			Assembly asm = Assembly.GetExecutingAssembly();
			// First Names
			using (StreamReader sr = new StreamReader(asm.GetManifestResourceStream("LinqToObjects.FirstNames.txt")))
			{
				while (!sr.EndOfStream)
				{
					string name = sr.ReadLine().Trim().ToLower();
					if (!string.IsNullOrEmpty(name))
					{
						m_FirstNames.Add(string.Format("{0}{1}", char.ToUpper(name[0]), name.Substring(1)));
					}
				}
			}

			// Last Names
			using (StreamReader sr = new StreamReader(asm.GetManifestResourceStream("LinqToObjects.LastNames.txt")))
			{
				while (!sr.EndOfStream)
				{
					string name = sr.ReadLine().Trim().ToLower();
					if (!string.IsNullOrEmpty(name))
					{
						m_LastNames.Add(string.Format("{0}{1}", char.ToUpper(name[0]), name.Substring(1)));
					}
				}
			}
		}


		static void Main(string[] args)
		{
			List<Person> people = new List<Person>();

			people.Add(new Person(123456789, "Jones", "Michael", DateTime.Parse("2/5/1971")));
			people.Add(new Person(558787646, "Smith", "John", DateTime.Parse("4/12/1965")));
			people.Add(new Person(183452315, "Williams", "Sarah", DateTime.Parse("2/15/1988")));
			people.Add(new Person(984162316, "Jefferson", "Michael", DateTime.Parse("2/5/1971")));
			people.Add(new Person(381236544, "Smith", "Jane", DateTime.Parse("8/27/1994")));
			people.Add(new Person(456891542, "Doe", "Jane", DateTime.Parse("11/1/1975")));
			people.Add(new Person(735485911, "Jones", "Sarah", DateTime.Parse("3/31/1998")));

			// Any
			bool anyAdults = people.Any(p => p.GetAge() >= 21);
			int numAdults = people.Count(p => p.GetAge() >= 21);
			Console.WriteLine("Any adults? {0}  How many? {1}", anyAdults, numAdults);

			// All
			bool allFirstNames4Chars = people.All(p => p.FirstName.Length >= 4);
			Console.WriteLine("All first names >= 4 chars? {0}", allFirstNames4Chars);

			// Min & Max
			int min = people.Min(p => p.GetAge());
			int max = people.Max(p => p.GetAge());
			double avg = people.Average(p => p.GetAge());
			Console.WriteLine("Youngest: {0}  Oldest: {1}  Average: {2:0.0}", min, max, avg);

			decimal sum = people.Sum(p => p.GetAge());
			Console.WriteLine(new string('=', 20));

			// Projection using anonymous types
			var peopleQuery = from p in people
							  where p.ID < 499999999L
							  orderby p.LastName, p.FirstName
							  select new 
							  { 
								  LastName = p.LastName, 
								  p.FirstName // Automatically called FirstName
							  };
			foreach (var name in peopleQuery)
			{
				Console.WriteLine(name.GetType());
				Console.WriteLine("{0}, {1}", name.LastName, name.FirstName);
				Console.WriteLine(name);
				MyMethod(name);
				MyMethod2(name);
			}


			Console.WriteLine(new string('=', 20));
			int[] numbers = { 2, 9, 4, 3, 6, 7, 5, 1, 12, 5, 9, 8, 1, 11, 1, 2, 10, 6 };
			var smallNums = (from number in numbers
							 where number < 10
							orderby number ascending
							select number).Take(10);
			foreach (int number in smallNums)
			{
				Console.Write("{0} ", number);
			}
			Console.WriteLine();
			Console.WriteLine(new string('=', 20));

			StudentTest();
			Console.WriteLine(new string('=', 20));
			LetTest();

			Console.Write("Press <ENTER> to quit...");
			Console.ReadLine();
		}

		/// <summary>
		/// Passing anon type as object
		/// </summary>
		/// <param name="obj"></param>
		static void MyMethod(object obj)
		{
			Console.WriteLine("==> {0}", obj);
		}

		/// <summary>
		/// Passing anon type as dynamic
		/// </summary>
		/// <param name="obj"></param>
		static void MyMethod2(dynamic obj)
		{
			Console.WriteLine("==> {0}, {1}", obj.LastName, obj.FirstName);
			
		}

		static void StudentTest()
		{
			List<Student> students = new List<Student>();
			// Generate random students
			for (int i = 0; i < 250; i++)
			{
				students.Add(new Student(GetRandomID(), GetRandomLastName(), GetRandomFirstName(), m_Random.Next(0, 13)));
			}

			var studentsByGrade = from student in students
								  orderby student.LastName, student.FirstName ascending
								  group student by student.Grade into gradeGroup
								  orderby gradeGroup.Key
								  select gradeGroup;

			foreach (var grade in studentsByGrade)
			{
				Console.WriteLine("Grade {0} ====================", 
					grade.Key == 0 ? "K" : grade.Key.ToString());
				foreach (Student student in grade)
				{
					Console.WriteLine("  {0:000-00-0000} {1,-15} {2,-15}", student.ID, student.LastName, student.FirstName);
				}
			}
		}

		/// <summary>
		/// Gets a random first name
		/// </summary>
		/// <returns></returns>
		public static string GetRandomFirstName()
		{
			return m_FirstNames[m_Random.Next(0, m_FirstNames.Count)];
		}

		/// <summary>
		/// Gets a random last name
		/// </summary>
		/// <returns></returns>
		public static string GetRandomLastName()
		{
			return m_LastNames[m_Random.Next(0, m_LastNames.Count)];
		}

		public static long GetRandomID()
		{
			string id = string.Format("{0:000}{1:00}{2:0000}", m_Random.Next(0, 1000), m_Random.Next(0, 100), m_Random.Next(0, 10000));
			return long.Parse(id);
		}


		private static void LetTest()
		{
			Type dateType = typeof(DateTime);
			var query = from member in dateType.GetMembers()
						where (member as PropertyInfo != null) && ((member as PropertyInfo).Name.Length <= 5)
						select (member as PropertyInfo);
			foreach (PropertyInfo prop in query)
			{
				Console.WriteLine(prop.Name);
			}

			Console.WriteLine("---");

			var query2 = from member in dateType.GetMembers()
						 let prop = member as PropertyInfo
						 where (prop != null) && (prop.Name.Length <= 5)
						 select prop;
			foreach (PropertyInfo prop in query2)
			{
				Console.WriteLine(prop.Name);
			}
		}



	}
}
