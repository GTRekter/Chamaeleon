using SharedLib;
using SharedLib.Enums;
using SharedLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CheckingAccountApi.Controllers
{
    public class TransactionController : ApiController
    {
        [HttpPost]
        public void AddCredit(int id, decimal amount, CreditTypeEnum creditType, DateTime date, string desccription)
        {
            Credit.AddCredit(id, amount, creditType, date, desccription);
        }
        [HttpPost]
        public void AddDebit(int id, decimal amount, DateTime date, string desccription, int checkNo, DebitTypeEnum debitType, decimal fee)
        {
            Debit.AddDebit(id, amount, date, desccription, checkNo, debitType, fee);
        }
        [HttpDelete]
        public void DeleteCredit(int id)
        {
            Credit.DeleteCredit(id);
        }
        [HttpDelete]
        public void DeleteDebit(int id)
        {
            Debit.DeleteDebit(id);
        }
        [HttpPut]
        public void UpdateCredit(int id, decimal amount, CreditTypeEnum creditType, DateTime date, string desccription)
        {
            Credit.DeleteCredit(id);
            Credit.AddCredit(id, amount, creditType, date, desccription);
        }
        [HttpPut]
        public void UpdateDebit(int id, decimal amount, DateTime date, string desccription, int checkNo, DebitTypeEnum debitType, decimal fee)
        {
            Debit.DeleteDebit(id);
            Debit.AddDebit(id, amount, date, desccription, checkNo, debitType, fee);
        }
        [HttpGet]
        public Credit GetCreditById(int id)
        {
            return Credit.GetCreditById(id);
        }
        [HttpGet]
        public Debit GetDebitById(int id)
        {
            return Debit.GetDebitById(id);
        }
        [HttpGet]
        public TransactionList GetCreditsByType(CreditTypeEnum creditType)
        {
            return Credit.GetCreditsByType(creditType);
        }
        [HttpGet]
        public TransactionList GetDebitsByType(DebitTypeEnum debitType)
        {
            return Debit.GetDebitsByType(debitType);
        }
        [HttpGet]
        public TransactionList GetTransactionsByDateRange(DateTime start, DateTime end)
        {
            return TransactionList.GetTransactionsByDateRange(start, end);
        }
        [HttpGet]
        public TransactionList GetAllTransactions()
        {
            return TransactionList.GetAllTransactions();
        }
    }
}
