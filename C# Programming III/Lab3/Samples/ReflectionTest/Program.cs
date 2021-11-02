using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Globalization;
using System.Runtime.InteropServices;

namespace ReflectionTest
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.ReadLine();

			LateBinding();
			Console.WriteLine(new string('=', 20));

			MethodInvoke();
			Console.WriteLine(new string('=', 20));

			PrintAssemblyInfo();
			Console.WriteLine(new string('=', 20));

			GetSetProperty();
			Console.ReadLine();
		}

		/// <summary>
		/// Prints information about the currently running assembly
		/// </summary>
		static void PrintAssemblyInfo()
		{
			Console.WriteLine();
			Console.WriteLine("{0} PrintAssemblyInfo {0}", new string('=', 20));
			Console.WriteLine();

			Assembly a = Assembly.GetExecutingAssembly();
			Console.WriteLine("Assembly: {0}", a.FullName);
			foreach (Type t in a.GetTypes())
			{
				Console.WriteLine("  Type: {0}", t.Name);

				foreach (ConstructorInfo ci in t.GetConstructors())
				{
					Console.WriteLine("    Ctor: {0}", ci.Name);
					foreach (ParameterInfo pi in ci.GetParameters())
					{
						Console.WriteLine("      Params: {0} {1}", pi.ParameterType, pi.Name);
					}
				}

				foreach (EventInfo ei in t.GetEvents())
				{
					Console.WriteLine("    Event: {0} {1}", ei.EventHandlerType, ei.Name);
				}

				foreach (FieldInfo fi in t.GetFields())
				{
					Console.WriteLine("    Field: {0} {1}", fi.FieldType, fi.Name);
				}

				foreach (PropertyInfo pi in t.GetProperties())
				{
					Console.WriteLine("    Property: {0} {1}", pi.PropertyType, pi.Name);
				}

				foreach (MethodInfo mi in t.GetMethods())
				{
					Console.WriteLine("    Method: {0}", mi.Name);
					foreach (ParameterInfo pi in mi.GetParameters())
					{
						Console.WriteLine("      Params: {0} {1}", pi.ParameterType, pi.Name);
					}
				}
			}
		}

		/// <summary>
		/// Gets and sets the value of a property
		/// </summary>
		static void GetSetProperty()
		{
			Console.WriteLine();
			Console.WriteLine("{0} GetSetProperty {0}", new string('=', 20));
			Console.WriteLine();

			Type t = typeof(Person);
			Person p = new Person("Gates", "Bill");
			Console.WriteLine("Original Person: {0}", p);

			// Get the property
			PropertyInfo pi = t.GetProperty("LastName",
				BindingFlags.GetField | BindingFlags.Public | BindingFlags.Instance);

			// 1st parameter is the instance to get proeprty from (null for static)
			// 2nd parameter is the index for indexed properties (null for standard props)
			Console.WriteLine("GetValue: LastName: {0}", pi.GetValue(p, null));
			
			// Set the property
			pi = t.GetProperty("LastName", 
				BindingFlags.SetField | BindingFlags.Public | BindingFlags.Instance);
			pi.SetValue(p, "Jones", null);
			Console.WriteLine("After SetValue: LastName: {0}", p.LastName);
		}

		/// <summary>
		/// Invoke a method using reflection
		/// </summary>
		static void MethodInvoke()
		{
			Console.WriteLine();
			Console.WriteLine("{0} MethodInvoke {0}", new string('=', 20));
			Console.WriteLine();

			Type t = typeof(MathStuff);
			
			// Call static method
			MethodInfo miAdd = t.GetMethod("Add", BindingFlags.Public | BindingFlags.Static);
			double sum = (double)miAdd.Invoke(null, new object[] { 6, 9 });
			Console.WriteLine("Sum of 6 + 9 is {0}", sum);

			// Call instance method
			MethodInfo miMultiply = t.GetMethod("Multiply", BindingFlags.Public | BindingFlags.Instance);
			MathStuff ms = new MathStuff();
			double product = (double)miMultiply.Invoke(ms, new object[] { 6, 9 });
			Console.WriteLine("Product of 6 x 9 is {0}", product);
		}

		/// <summary>
		/// Demonstration of late binding
		/// </summary>
		static void LateBinding()
		{
			Console.WriteLine();
			Console.WriteLine("{0} LateBinding {0}", new string('=', 20));
			Console.WriteLine();

			try
			{
				Assembly a = Assembly.Load("TestLib");
				dynamic student = a.CreateInstance("TestLib.Student", false, BindingFlags.CreateInstance, null, 
					new object[] { "Gates", "Bill", DateTime.Parse("10/28/1955") }, CultureInfo.CurrentCulture, null);
				Console.WriteLine(student);
				student.LastName = "Yates";
				Console.WriteLine(student);
			}
			catch (Exception ex)
			{
				System.Reflection.MethodBase mb = System.Reflection.MethodBase.GetCurrentMethod();
				System.Diagnostics.Debug.WriteLine(ex.Message, string.Format("{0}.{1}.{2}", mb.DeclaringType.Namespace, mb.DeclaringType.Name, mb.Name));
				// OR
				System.Diagnostics.EventLog.WriteEntry(mb.DeclaringType.Assembly.FullName,
					string.Format("{0}.{1}.{2}: {3}", 
						mb.DeclaringType.Namespace, mb.DeclaringType.Name, mb.Name, ex.Message));
			}
		}
	}
}
