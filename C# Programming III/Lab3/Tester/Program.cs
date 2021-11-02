using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            ReflectionTest();
            TestPerson();
            MathStuffTest();
            CustomAttributeTest();

            Console.WriteLine("Press <ENTER> to quit...");
            Console.ReadLine();
        }

        #region ReflectionTest
        private static void ReflectionTest()
        {
            Assembly asm = Assembly.Load("SharedLib");
            Console.WriteLine(asm.FullName);            PrintModules(asm);
        }

        private static void PrintModules(Assembly asm)
        {
            Module[] modules = asm.GetModules();
            foreach (Module module in modules)
            {
                Console.WriteLine(string.Format("{0,5}: {1}", "Module", module.Name));
                PrintTypes(module);
            }
        }

        private static void PrintTypes(Module module)
        {
            Type[] types = module.GetTypes();
            foreach (Type type in types)
            {
                Console.WriteLine(string.Format("{0,10}: {1}", "Type", type.Name));
                PrintFields(type);
                PrintMethods(type);
            }
        }

        private static void PrintFields(Type type)
        {
            FieldInfo[] fieldInfos = type.GetFields();
            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                Console.WriteLine(string.Format("{0,15}: {1}{2}", "Field", fieldInfo.FieldType.ToString(), fieldInfo.Name));
            }
        }

        private static void PrintMethods(Type type)
        {
            MethodInfo[] methodInfos = type.GetMethods();
            foreach (MethodInfo methodInfo in methodInfos)
            {
                Console.WriteLine(string.Format("{0,15}: {1} return {2}", "Method", methodInfo.Name, methodInfo.ReturnType.ToString()));
                PrintParameters(methodInfo);
            }
        }

        private static void PrintParameters(MethodInfo methodInfo)
        {
            ParameterInfo[] ParameterInfos = methodInfo.GetParameters();

            Console.WriteLine(string.Format("{0,15}","Parameters:"));          
            foreach (ParameterInfo parameterInfo in ParameterInfos)
            {
                Console.WriteLine(string.Format("{0,20}{1}", parameterInfo.ParameterType.ToString(), parameterInfo.Name));
            }
        }

        #endregion

        #region TestPerson

        private static void TestPerson()
        {
            Assembly asm = Assembly.Load("SharedLib");
            dynamic p1 = asm.CreateInstance("SharedLib.Person");            p1.LastName = "Smith";
            p1.FirstName = "Jane";
            p1.DOB = DateTime.Now;

            Type enumType = asm.GetType("SharedLib.Person+Genders");
            p1.Gender = (dynamic)Enum.Parse(enumType, "Female");

            Console.WriteLine(string.Format("{0,-10}{1,-10}{2,-10}{3,-10}", p1.LastName, p1.FirstName, p1.DOB, p1.Gender));
            
            dynamic p2 = asm.CreateInstance("SharedLib.Person", true,
                BindingFlags.Public | BindingFlags.CreateInstance | BindingFlags.Instance, null,
                new object[] { "Smith", "John", DateTime.Parse("1/1/2000"),
                (dynamic)Enum.Parse(enumType, "Male") }, null, null);

        }

        #endregion

        #region MathStuffTest

        public static void MathStuffTest()
        {
            Assembly asm = Assembly.Load("SharedLib");
            Type mathType = asm.GetType("SharedLib.MathStuff");

            var areaMethod = mathType.GetMethod("CircleArea", BindingFlags.Public | BindingFlags.Static);
            double area = (double)areaMethod.Invoke(null, new object[] { 12.34 });

            Console.WriteLine($"Area: {area}");
        }

        #endregion

        #region CustomAttributeTest

        public static void CustomAttributeTest()
        {
            Assembly asm = Assembly.Load("SharedLib");
            Type mathType = asm.GetType("SharedLib.MathStuff");
            Type personType = asm.GetType("SharedLib.Person");
            Type specialType = asm.GetType("SharedLib.SpecialClassAttribute");

            var attrs = mathType.GetCustomAttributes(specialType);
            foreach (dynamic attr in attrs)
            {
                Console.WriteLine($"{mathType.Name} has the special class ID of {attr.ID}");
            }            attrs = personType.GetCustomAttributes(specialType);
            foreach (dynamic attr in attrs)
            {
                Console.WriteLine($"{personType.Name} has the special class ID of {attr.ID}");
            }
        }

        #endregion
    }
}
