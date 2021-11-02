using Lab2Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Lab2Service
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISchoolService" in both code and config file together.
	[ServiceContract]
	public interface ISchoolService
	{
        #region Students
        [OperationContract]
		Student AddStudent(string id, string lastName, string firstName, DateTime dob, GenderEnum gender, string major, float units, float gpa);
		[OperationContract]
		void DeleteStudent(string id);
		[OperationContract]
		Student GetStudent(string id);
		[OperationContract]
		List<Student> GetStudents();
		[OperationContract]
		Student UpdateStudent(string id, string lastName, string firstName, DateTime dob, GenderEnum gender, string major, float units, float gpa);
        #endregion
        #region Teacher
        /// <summary>
        /// Accepts all necessary values to create a new Teacher object 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
		Teacher AddTeacher(string lastName, string firstName, DateTime dob, GenderEnum gender, int id, DateTime dateOfHire, int salary);
		[OperationContract]
		void DeleteTeacher(int id);
		[OperationContract]
		Teacher GetTeacher(int id);
		[OperationContract]
		List<Teacher> GetTeachers();
		/// <summary>
		/// Accepts values corresponding to all Teacher properties
		/// </summary>
		/// <returns></returns>
		[OperationContract]
		Teacher UpdateTeacher(string lastName, string firstName, DateTime dob, GenderEnum gender, int id, DateTime dateOfHire, int salary);
        #endregion
        #region People
        [OperationContract]
		PersonList GetPeople(string lastName, string firstName);
		#endregion
	}
}