using SharedLib;
using SharedLib.Enums;
using SharedLib.Interfaces;
using SharedLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CheckingAccountService
{
    public class TransactionService : ITransactionService
    {
        public void AddCredit(int id, decimal amount, CreditTypeEnum creditType, DateTime date, string desccription)
        {
            Credit.AddCredit(id, amount, creditType, date, desccription);
        }
        public void AddDebit(int id, decimal amount, DateTime date, string desccription, int checkNo, DebitTypeEnum debitType, decimal fee)
        {
            Debit.AddDebit(id, amount, date, desccription, checkNo, debitType, fee);
        }
        public void DeleteCredit(int id)
        {
            Credit.DeleteCredit(id);
        }
        public void DeleteDebit(int id)
        {
            Debit.DeleteDebit(id);
        }
        public void UpdateCredit(int id, decimal amount, CreditTypeEnum creditType, DateTime date, string desccription)
        {
            Credit.DeleteCredit(id);
            Credit.AddCredit(id, amount, creditType, date, desccription);
        }
        public void UpdateDebit(int id, decimal amount, DateTime date, string desccription, int checkNo, DebitTypeEnum debitType, decimal fee)
        {
            Debit.DeleteDebit(id);
            Debit.AddDebit(id, amount, date, desccription, checkNo, debitType, fee);
        }
        public Credit GetCreditById(int id)
        {
            return Credit.GetCreditById(id);
        }
        public Debit GetDebitById(int id)
        {
            return Debit.GetDebitById(id);
        }
        public TransactionList GetCreditsByType(CreditTypeEnum creditType)
        {
            return Credit.GetCreditsByType(creditType);
        }
        public TransactionList GetDebitsByType(DebitTypeEnum debitType)
        {
            return Debit.GetDebitsByType(debitType);
        }
        public TransactionList GetTransactionsByDateRange(DateTime start, DateTime end)
        {
            return TransactionList.GetTransactionsByDateRange(start, end);
        }
        public TransactionList GetAllTransactions()
        {
            return TransactionList.GetAllTransactions();
        }
    }
}
