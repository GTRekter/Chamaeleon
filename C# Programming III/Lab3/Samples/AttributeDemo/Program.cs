using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AttributeDemo
{
	class Program
	{
		static void Main(string[] args)
		{

			User.LoginUser(UserTypes.PowerUser, "Power User");

			try
			{
				ProcessData pd = new ProcessData();

				object value = pd.LoadData();
				Console.WriteLine("Data loaded...");
				pd.SaveData(value);
				Console.WriteLine("Data saved...");
				pd.DeleteData(value);
				Console.WriteLine("Data deleted...");
			}
			catch (InvalidOperationException ex)
			{
				Console.WriteLine(ex.Message);
			}

			Console.ReadLine();
		}
	}
}
