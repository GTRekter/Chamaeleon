using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountRegisterClient
{
    class Program
    {
        private static MathServiceClient proxyMath;
        static void Main(string[] args)
        {
            proxySchool = new SchoolServiceClient();
            TestDebit();
            TestTeacher();
            TestPeople();
            TestMath();
            Console.WriteLine("Press Any key to exit");
            Console.ReadLine();
        }
        private static void TestDebit()
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
        public static void PrintProperties(object obj)
        {
            if (obj is Teacher)
            {
                Teacher t = (Teacher)obj;
                string.Format("{0,7} {1:MM/dd/yyyy} {2,-20} {3,10:N2} {4,8} {5,10} {6,12:N2}",
 obj.CheckNo, obj.Date, obj.Description, obj.Amount, obj.Fee, string.Empty, balance);

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
