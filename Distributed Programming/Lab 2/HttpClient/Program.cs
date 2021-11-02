using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpClient.SchoolServiceRef;
using HttpClient.MathServiceRef;


namespace HttpClient
{
    class Program
    {
        private static SchoolServiceClient proxySchool;
        private static MathServiceClient proxyMath;

        static void Main(string[] args)
        {
            proxySchool = new SchoolServiceClient();
            proxyMath = new MathServiceClient();
            TestStudent();
            TestTeacher();
            TestPeople();
            TestMath();
            Console.WriteLine("Press Any key to exit");
            Console.ReadLine();
        }

        private static void TestMath()
        {
            
            var result = proxyMath.Add(10, 10);
            Console.WriteLine(result);
            result = proxyMath.Subtract(10, 10);
            Console.WriteLine(result);
            result = proxyMath.Divide(10, 10);
            Console.WriteLine(result);
            result = proxyMath.Multiply(10, 10);
            Console.WriteLine(result);
            result = proxyMath.CircleArea(10);
            Console.WriteLine(result);
            result = proxyMath.RectangleArea(10, 10);
            Console.WriteLine(result);
        }
        private static void TestStudent()
        {
            Student newStudent = proxySchool.AddStudent("A123456", "Smith", "Bill", DateTime.Parse("2/3/1977"), GenderEnum.Male, "Communication", 33f, 3.5f);
            newStudent = proxySchool.AddStudent("B435345", "Williams", "Bill", DateTime.Parse("1/3/1988"), GenderEnum.Male, "Computer Science", 31f, 2.5f);
            newStudent = proxySchool.AddStudent("D777666", "Francis", "Jill", DateTime.Parse("8/8/1982"), GenderEnum.Female, "Math", 22f, 3.9f);

            List<Student> students = proxySchool.GetStudents();
            foreach (Student s in students)
            {
                PrintProperties(s);
            }

            Student student = proxySchool.GetStudent("A123456");
            PrintProperties(student);

            proxySchool.DeleteStudent("A123456");
            students = proxySchool.GetStudents();
            foreach (var s in students)
            {
                PrintProperties(s);
            }
        }
        private static void TestTeacher()
        {
            Teacher newTeacher = proxySchool.AddTeacher("Luke", "Skywalker", DateTime.Parse("2/3/1977"), GenderEnum.Male, 1, DateTime.Parse("2/3/2020"), 100000);
            newTeacher = proxySchool.AddTeacher("Han", "Solo", DateTime.Parse("1/3/1988"), GenderEnum.Male, 2, DateTime.Parse("2/6/2019"), 150000);
            newTeacher = proxySchool.AddTeacher("Obi Wan", "Kenobi", DateTime.Parse("8/8/1982"), GenderEnum.Female, 3, DateTime.Parse("9/8/2010"), 120000);

            List<Teacher> teachers = proxySchool.GetTeachers();
            foreach (Teacher t in teachers)
            {
                PrintProperties(t);
            }
            Teacher teacher = proxySchool.GetTeacher(1);
            PrintProperties(teacher);

            proxySchool.DeleteTeacher(1);
            teachers = proxySchool.GetTeachers();
            foreach (Teacher t in teachers)
            {
                PrintProperties(t);
            }
        }
        private static void TestPeople()
        {
            PersonList people = proxySchool.GetPeople("Han", "Solo");
            foreach (Person p in people)
            {
                PrintProperties(p);
            }
        }
        private static void PrintProperties(object obj)
        {
            if(obj is Teacher)
            {
                Teacher t = (Teacher)obj;
                Console.WriteLine($"Teacher: {t.FirstName} {t.LastName} - {t.DOB.ToString("YYYY/mm/dd")} - {t.Gender.ToString()}");
            }
            else if (obj is Student)
            {
                Student s = (Student)obj;
                Console.WriteLine($"Student: {s.FirstName} {s.LastName} - {s.DOB.ToString("YYYY/mm/dd")} - {s.Gender.ToString()}");
            }
        }
    }
}
