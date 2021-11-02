using Lab2Service.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml;

namespace Lab2Service
{
	public class SchoolService : ISchoolService
	{
        #region Students
        public Student AddStudent(string id, string lastName, string firstName, DateTime dob, GenderEnum gender, string major, float units, float gpa)
		{
			var data = DataStore.LoadData();
			Student result = null;
			var student = GetStudent(id);
			if (student == null)
			{
				// Student doesn't exist
				result = new Student() { ID = id, LastName = lastName, FirstName = firstName,
					DOB = dob, Gender = gender, Major = major, Units = units, GPA = gpa	};
				data.Add(result);
				DataStore.SaveData();
			}
			return result;
		}
		public void DeleteStudent(string id)
		{
			var data = DataStore.LoadData();
			data.RemoveAll(person => person is Student && (person as Student).ID == id);
			DataStore.SaveData();
		}
		public Student GetStudent(string id)
		{
			var data = DataStore.LoadData();
			var query = from person in data
						let student = person as Student
						where student != null && student.ID == id
						select student;
			return query.FirstOrDefault();
		}
		public List<Student> GetStudents()
		{
			var data = DataStore.LoadData();
			var query = from person in data
						let student = person as Student
						where student != null
						select student;
			return query.ToList();
		}
		public Student UpdateStudent(string id, string lastName, string firstName, DateTime dob, GenderEnum gender, string major, float units, float gpa)
		{
			// Simplest technique is to remove then add
			DeleteStudent(id);
			return AddStudent(id, lastName, firstName, dob, gender, major, units, gpa);
		}
        #endregion
        #region teacher
        public Teacher AddTeacher(string lastName, string firstName, DateTime dob, GenderEnum gender, int id, DateTime dateOfHire, int salary)
		{
			var data = DataStore.LoadData();
			Teacher result = null;
			var teacher = GetTeacher(id);
			if (teacher == null)
			{
				// Teacher doesn't exist
				result = new Teacher()
				{
					ID = id,
					LastName = lastName,
					FirstName = firstName,
					DOB = dob,
					Gender = gender,
					DateOfHire = dateOfHire,
					Salary = salary
				};
				data.Add(result);
				DataStore.SaveData();
			}
			return result;
		}
		public void DeleteTeacher(int id)
		{
			var data = DataStore.LoadData();
			data.RemoveAll(person => person is Teacher && (person as Teacher).ID == id);
			DataStore.SaveData();
		}
		public Teacher GetTeacher(int id)
		{
			var data = DataStore.LoadData();
			var query = from person in data
						let teacher = person as Teacher
						where teacher != null && teacher.ID == id
						select teacher;
			return query.FirstOrDefault();
		}
		public List<Teacher> GetTeachers()
		{
			var data = DataStore.LoadData();
			var query = from person in data
						let teacher = person as Teacher
						where teacher != null
						select teacher;
			return query.ToList();
		}
		public Teacher UpdateTeacher(string lastName, string firstName, DateTime dob, GenderEnum gender, int id, DateTime dateOfHire, int salary)
		{
			// Simplest technique is to remove then add
			DeleteTeacher(id);
			return AddTeacher(lastName, firstName, dob, gender, id, dateOfHire, salary);
		}
		#endregion
		#region People
		public PersonList GetPeople(string lastName, string firstName)
		{
			var data = DataStore.LoadData();
			var query = data.AsQueryable();
			if (!string.IsNullOrEmpty(lastName))
			{
				query = query.Where(p => p.LastName == lastName);
			}
			if (!string.IsNullOrEmpty(firstName))
			{
				query = query.Where(p => p.FirstName == firstName);
			}
			return new PersonList(query);
		}
		#endregion
	}
}