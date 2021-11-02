using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MutexSample
{
	public class BankAccount
	{
		#region Fields
		private Mutex m_Lock = new Mutex();
		private decimal m_Balance = 0;
		#endregion Fields

		#region Constructors
		/// <summary>
		/// Creates a new bank account with the given opening balance
		/// </summary>
		/// <param name="amount">Amount to initially deposit in the account</param>
		public BankAccount(decimal amount)
		{
			Deposit(amount);
		}
		#endregion Constructors

		/// <summary>
		/// Provide read-only access to balance
		/// </summary>
		public decimal Balance
		{
			get
			{
				return m_Balance;
			}
			private set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("Balance", "Balance must be >= 0");
				}
				m_Balance = value;
			}
		}

		/// <summary>
		/// Deposit the given amount into the account
		/// </summary>
		/// <param name="amount">Amount to deposit</param>
		private void Deposit(decimal amount)
		{
			if (amount <= 0)
			{
				throw new ArgumentOutOfRangeException("amount", "Deposit amount must be positive");
			}
			Balance += amount;
		}

		/// <summary>
		/// Withdraw the given amount for the account
		/// </summary>
		/// <param name="amount">Amount to withdraw - must be less-than or equal to Balance</param>
		private void Withdraw(decimal amount)
		{
			if (amount <= 0)
			{
				throw new ArgumentOutOfRangeException("amount", "Withdrawal amount must be positive");
			}
			else if (amount > Balance)
			{
				throw new ArgumentOutOfRangeException("amount", "Insufficient funds!");
			}
			Balance -= amount;
		}

		/// <summary>
		/// Transfer the given amount from the given account
		/// </summary>
		/// <param name="other">Account to transfer from</param>
		/// <param name="amount">Amount to transfer</param>
		public void TransferFrom(BankAccount other, decimal amount)
		{
			Mutex[] accountLocks = { m_Lock, other.m_Lock };
			try
			{
				// Wait for all objects to acquire lock
				WaitHandle.WaitAll(accountLocks);
				// Perform atomic actions
				try
				{
					other.Withdraw(amount);
				}
				catch (Exception)
				{
					throw;
				}
				Deposit(amount);
			}
			finally
			{
				// Release locks
				foreach (Mutex m in accountLocks)
					m.ReleaseMutex();
			}
		}
	}
}
