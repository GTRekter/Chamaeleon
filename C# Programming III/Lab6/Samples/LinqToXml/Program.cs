using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace LinqToXml
{
	class Program
	{
		static void Pause()
		{
			Console.WriteLine();
			Console.WriteLine(new string('=', 30));
			Console.Write("Press <ENTER> to continue...");
			Console.ReadLine();
			Console.WriteLine();
		}

		static void Main(string[] args)
		{
			XDocument doc = MakeXmlDoc();
			Console.WriteLine(doc.Declaration);
			Console.WriteLine(doc);

			Pause();

			// Select XML Records
			var honorRoll =
				from student in doc.Descendants("Student")
				where (float)student.Element("GPA") >= 3.5f
				orderby (float)student.Element("GPA") descending
				select student;

			foreach (var student in honorRoll)
			{
				Console.WriteLine(student);
			}

			Pause();

			// Select Projected Types
			var honorRoll2 =
				from student in doc.Descendants("Student")
				where (float)student.Element("GPA") >= 3.5f
				orderby (float)student.Element("GPA") descending
				select new 
				{ 
					LastName = student.Element("LastName").Value, 
					FirstName = student.Element("FirstName").Value,
					GPA = student.Element("GPA").Value,
					FullName = string.Format("{0}, {1}", student.Element("LastName").Value, student.Element("FirstName").Value)
				};

			foreach (var student in honorRoll2)
			{
				Console.WriteLine(student);
			}

			Pause();

			// Using Linq to Xml to create a new document
			XDocument newDoc = new XDocument(
				new XDeclaration("1.0", "utf-8", "yes"),
				new XComment("Students with ID's between 00000 - 499999"),
				new XElement("NewStudents",
					from student in doc.Descendants("Student")
				    where (int)student.Attribute("id") < 500000
					orderby student.Element("LastName").Value, student.Element("FirstName").Value
				    select student)
			);

			Console.WriteLine(newDoc);

			Pause();
		}

		/// <summary>
		/// Make an Xml Document
		/// </summary>
		/// <returns>Created Xml Document</returns>
		static XDocument MakeXmlDoc()
		{
			XDocument doc = new XDocument(
				new XDeclaration("1.0", "utf-8", "yes"),
				new XComment("List of students"),
				new XElement("Students",
					new XElement("Student",
						new XAttribute("id", 123456),
						new XElement("FirstName", "John"),
						new XElement("LastName", "Doe"),
						new XElement("GPA", 3.78)),
					new XElement("Student",
						new XAttribute("id", 645789),
						new XElement("FirstName", "Jane"),
						new XElement("LastName", "Smith"),
						new XElement("GPA", 4.0)),
					new XElement("Student",
						new XAttribute("id", 164875),
						new XElement("FirstName", "Brad"),
						new XElement("LastName", "Jones"),
						new XElement("GPA", 2.97))
				)  // Students
			);  // doc
			return doc;
		}
	}
}
