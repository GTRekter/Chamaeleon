using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MutexSample
{
	class Program
	{
		static void Main(string[] args)
		{
			BankAccount account1 = new BankAccount(5000M);
			BankAccount account2 = new BankAccount(10M);
			Console.WriteLine("Account1: {0:C}  Account2: {1:C}", account1.Balance, account2.Balance);
			account2.TransferFrom(account1, 500);
			Console.WriteLine("Account1: {0:C}  Account2: {1:C}", account1.Balance, account2.Balance);

			Console.Write("Press <ENTER> to quit...");
			Console.ReadLine();
		}
	}
}
