using System;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using Utilities;
using SharedLib.Enums;
using SharedLib.Models;
using CheckingAccountClient.Models;
using CheckingAccountClient.TransactionServiceRef;

namespace CheckingAccountClient
{
	class Program
	{
		private static TransactionServiceClient proxyTransaction;

		const string SERVICE = "https://localhost:44310/api/";
		const string CONTROLLER = "transaction";
		static readonly Client m_Client = new Client(SERVICE, SerializationModesEnum.Json);

		static void Main()
		{
			proxyTransaction = new TransactionServiceClient();

			proxyTransaction.AddCredit(1, 100, CreditTypeEnum.Check, new DateTime(2019, 10, 1), "UCSD Course I");
			proxyTransaction.AddCredit(2, 350, CreditTypeEnum.Check, new DateTime(2020, 01, 1), "UCSD Course II");
			proxyTransaction.AddCredit(3, 900, CreditTypeEnum.Check, new DateTime(2020, 02, 1), "UCSD Course III");
			proxyTransaction.AddDebit(1, 100, new DateTime(2019, 10, 1), "UCSD Course I", 1, DebitTypeEnum.Check, 15);
			proxyTransaction.AddDebit(2, 350, new DateTime(2020, 01, 1), "UCSD Course II", 2, DebitTypeEnum.Cash, 27);
			proxyTransaction.AddDebit(3, 900, new DateTime(2020, 02, 1), "UCSD Course III", 3, DebitTypeEnum.Check, 15);

			MenuOptionsEnum choice = MenuOptionsEnum.Quit;
			do
			{
				choice = ConsoleHelpers.ReadEnum<MenuOptionsEnum>("Option: ", true);
				Console.Clear();
				
				if (choice != MenuOptionsEnum.Quit)
				{
					DisplayOptionTitle(choice);
				}
				switch (choice)
				{
					case MenuOptionsEnum.GetAllTransactionsWcf:
						GetAllTransactionsWcf();
						break;
					case MenuOptionsEnum.GetCreditsByTypeWcf:
						GetCreditsByTypeWcf();
						break;
					case MenuOptionsEnum.GetDebitsByTypeWcf:
						GetDebitsByTypeWcf();
						break;
					case MenuOptionsEnum.GetTransactionsByDateRangeWcf:
						GetTransactionsByDateRangeWcf();
						break;
					case MenuOptionsEnum.AddCreditWcf:
						AddCreditWcf();
						break;
					case MenuOptionsEnum.AddDebitWcf:
						AddDebitWcf();
						break;
					case MenuOptionsEnum.GetAllTransactionsApi:
						GetAllTransactionsApi();
						break;
				}
				Console.WriteLine();
				Console.Write("Press <ENTER> to continue...");
				Console.ReadLine();
				Console.Clear();

			} while (choice != MenuOptionsEnum.Quit);
		}
		#region WCF
		private static void AddDebitWcf()
		{
			try
			{
				int id = ConsoleHelpers.ReadInt("Insert id: ");
				decimal amount = ConsoleHelpers.ReadDecimal("Insert amount: ");
				DateTime date = ConsoleHelpers.ReadDate("Insert date: ");
				string description = ConsoleHelpers.ReadString("Insert description: ");
				int checkNo = ConsoleHelpers.ReadInt("Insert check number: ");
				DebitTypeEnum debitType = ConsoleHelpers.ReadEnum<DebitTypeEnum>("Insert debit type: ");
				decimal fee = ConsoleHelpers.ReadDecimal("Insert fee: ");
				proxyTransaction.AddDebit(id, amount, date, description, checkNo, debitType, fee);
			}
			catch (Exception ex)
			{
				System.Reflection.MethodBase mb = System.Reflection.MethodBase.GetCurrentMethod();
				System.Diagnostics.Debug.WriteLine(ex.Message, string.Format("{0}.{1}.{2}", mb.DeclaringType.Namespace, mb.DeclaringType.Name, mb.Name));
			}
		}
		private static void AddCreditWcf()
		{
			try
			{
				int id = ConsoleHelpers.ReadInt("Insert id: ");
				decimal amount = ConsoleHelpers.ReadDecimal("Insert amount: ");
				DateTime date = ConsoleHelpers.ReadDate("Insert date: ");
				string description = ConsoleHelpers.ReadString("Insert description: ");
				CreditTypeEnum creditType = ConsoleHelpers.ReadEnum<CreditTypeEnum>("Insert credit type: ");
				proxyTransaction.AddCredit(id, amount, creditType, date, description);
			}
			catch (Exception ex)
			{
				System.Reflection.MethodBase mb = System.Reflection.MethodBase.GetCurrentMethod();
				System.Diagnostics.Debug.WriteLine(ex.Message, string.Format("{0}.{1}.{2}", mb.DeclaringType.Namespace, mb.DeclaringType.Name, mb.Name));
			}
		}
		private static void GetTransactionsByDateRangeWcf()
		{
			try
			{
				DateTime startDate = ConsoleHelpers.ReadDate("Insert start date: ");
				DateTime endDate = ConsoleHelpers.ReadDate("Insert end date: ");
				List<Transaction> transactions = proxyTransaction.GetTransactionsByDateRange(startDate, endDate).OrderBy(t => t.Date).ToList();
				decimal balance = 0;
				PrintHeader();
				foreach (var transaction in transactions)
				{
					if (transaction is Debit)
					{
						balance -= transaction.Amount;
						Debit debit = (Debit)transaction;
						PrintTransaction(debit.CheckNo, debit.Date, debit.Description, debit.Amount, debit.Fee, null, balance);
					}
					if (transaction is Credit)
					{
						balance += transaction.Amount;
						Credit credit = (Credit)transaction;
						PrintTransaction(null, credit.Date, credit.Description, null, null, credit.Amount, balance);
					}
				}
			}
			catch (Exception ex)
			{
				System.Reflection.MethodBase mb = System.Reflection.MethodBase.GetCurrentMethod();
				System.Diagnostics.Debug.WriteLine(ex.Message, string.Format("{0}.{1}.{2}", mb.DeclaringType.Namespace, mb.DeclaringType.Name, mb.Name));
			}
		}
		private static void GetDebitsByTypeWcf()
		{
			try
			{
				DebitTypeEnum debitType = ConsoleHelpers.ReadEnum<DebitTypeEnum>("Insert debit type: ");
				List<Transaction> transactions = proxyTransaction.GetDebitsByType(debitType).OrderBy(t => t.Date).ToList();
				decimal balance = 0;
				PrintHeader();
				foreach (var transaction in transactions)
				{
					if (transaction is Debit)
					{
						balance -= transaction.Amount;
						Debit debit = (Debit)transaction;
						PrintTransaction(debit.CheckNo, debit.Date, debit.Description, debit.Amount, debit.Fee, null, balance);
					}
					if (transaction is Credit)
					{
						balance += transaction.Amount;
						Credit credit = (Credit)transaction;
						PrintTransaction(null, credit.Date, credit.Description, null, null, credit.Amount, balance);
					}
				}
			}
			catch (Exception ex)
			{
				System.Reflection.MethodBase mb = System.Reflection.MethodBase.GetCurrentMethod();
				System.Diagnostics.Debug.WriteLine(ex.Message, string.Format("{0}.{1}.{2}", mb.DeclaringType.Namespace, mb.DeclaringType.Name, mb.Name));
			}
		}
		private static void GetCreditsByTypeWcf()
		{
			try
			{
				CreditTypeEnum creditType = ConsoleHelpers.ReadEnum<CreditTypeEnum>("Insert credit type: ");
				List<Transaction> transactions = proxyTransaction.GetCreditsByType(creditType).OrderBy(t => t.Date).ToList();
				decimal balance = 0;
				PrintHeader();
				foreach (var transaction in transactions)
				{
					if (transaction is Debit)
					{
						balance -= transaction.Amount;
						Debit debit = (Debit)transaction;
						PrintTransaction(debit.CheckNo, debit.Date, debit.Description, debit.Amount, debit.Fee, null, balance);
					}
					if (transaction is Credit)
					{
						balance += transaction.Amount;
						Credit credit = (Credit)transaction;
						PrintTransaction(null, credit.Date, credit.Description, null, null, credit.Amount, balance);
					}
				}
			}
			catch (Exception ex)
			{
				System.Reflection.MethodBase mb = System.Reflection.MethodBase.GetCurrentMethod();
				System.Diagnostics.Debug.WriteLine(ex.Message, string.Format("{0}.{1}.{2}", mb.DeclaringType.Namespace, mb.DeclaringType.Name, mb.Name));
			}
		}
		private static void GetAllTransactionsWcf()
		{
			try
			{
				var transactions = proxyTransaction.GetAllTransactions();
				decimal balance = 0;
				PrintHeader();
				foreach (var transaction in transactions)
				{
					if (transaction is Debit)
					{
						Debit debit = (Debit)transaction;
						balance -= debit.Amount;
						balance -= debit.Fee;
						PrintTransaction(debit.CheckNo, debit.Date, debit.Description, debit.Amount, debit.Fee, null, balance);
					}
					if (transaction is Credit)
					{
						Credit credit = (Credit)transaction;
						balance += credit.Amount;
						PrintTransaction(null, credit.Date, credit.Description, null, null, credit.Amount, balance);
					}
				}
			}
			catch (Exception ex)
			{
				System.Reflection.MethodBase mb = System.Reflection.MethodBase.GetCurrentMethod();
				System.Diagnostics.Debug.WriteLine(ex.Message, string.Format("{0}.{1}.{2}", mb.DeclaringType.Namespace, mb.DeclaringType.Name, mb.Name));
			}
		}
		#endregion
		#region API
		private static void GetAllTransactionsApi()
		{
			try
			{
				var result = m_Client.Get<SharedLib.Models.TransactionList>(CONTROLLER, string.Empty);
				if (!result.IsSuccessStatusCode)
				{
					Console.WriteLine("Error encountered: {0}", result.Error);
					return;
				}
				decimal balance = 0;
				PrintHeader();
				foreach (var transaction in result.Result)
				{
					if (transaction is Debit)
					{
						balance -= transaction.Amount;
						Debit debit = (Debit)transaction;
						PrintTransaction(debit.CheckNo, debit.Date, debit.Description, debit.Amount, debit.Fee, null, balance);
					}
					if (transaction is Credit)
					{
						balance += transaction.Amount;
						Credit credit = (Credit)transaction;
						PrintTransaction(null, credit.Date, credit.Description, null, null, credit.Amount, balance);
					}
				}
			}
			catch (Exception ex)
			{
				System.Reflection.MethodBase mb = System.Reflection.MethodBase.GetCurrentMethod();
				System.Diagnostics.Debug.WriteLine(ex.Message, string.Format("{0}.{1}.{2}", mb.DeclaringType.Namespace, mb.DeclaringType.Name, mb.Name));
			}
		}
		#endregion
		/// <summary>
		/// Displays the screen title for the given menu choice
		/// </summary>
		/// <param name="choice">Menu choice</param>
		private static void DisplayOptionTitle(MenuOptionsEnum choice)
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(choice.WordBreakMixedCase());
			ConsoleHelpers.WriteBorder('=', choice.WordBreakMixedCase().Length);
			Console.WriteLine();
			Console.ResetColor();
		}	
		private static void PrintHeader()
		{
			Console.WriteLine(string.Format("{1,7} {1,10} {2,-20} {3,10} {4,8} {5,10} {6,12}", "Check #", "Date", "Description", "Debit", "Fee", "Credit", "Balance"));
			Console.WriteLine(new String('=', 83));
		}
		private static void PrintTransaction(int? checkNo, DateTime date, string description, decimal? debitAmount, decimal? fee, decimal? creditAmount, decimal? balance)
		{
			Console.WriteLine(string.Format("{0,7} {1:MM/dd/yyyy} {2,-20} {3,10:N2} {4,8} {5,10} {6,12:N2}",
							checkNo, date, description, debitAmount, fee, creditAmount, balance));
		}
	}
}