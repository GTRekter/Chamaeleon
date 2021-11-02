using SharedLib.Enums;
using SharedLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Interfaces
{
    [ServiceContract]
    public interface ITransactionService
    {
        /// <summary>
        /// Adds a new Debit object to the checking register
        /// </summary>
        /// <param name="debit"></param>
        [OperationContract]
        void AddDebit(int id, decimal amount, DateTime date, string desccription, int checkNo, DebitTypeEnum debitType, decimal fee);
        /// <summary>
        /// Update a Debit object to the checking register
        /// </summary>
        /// <param name="debit"></param>
        [OperationContract] 
        void UpdateDebit(int id, decimal amount, DateTime date, string desccription, int checkNo, DebitTypeEnum debitType, decimal fee);
        /// <summary>
        /// Delete a Debit object to the checking register
        /// </summary>
        /// <param name="debit"></param>
        [OperationContract]
        void DeleteDebit(int id);
        /// <summary>
        /// Adds a new Credit object to the checking register
        /// </summary>
        /// <param name="credit"></param>
        [OperationContract] 
        void AddCredit(int id, decimal amount, CreditTypeEnum creditType, DateTime date, string desccription);
        /// <summary>
        /// Update a Credit object to the checking register
        /// </summary>
        /// <param name="credit"></param>
        [OperationContract] 
        void UpdateCredit(int id, decimal amount, CreditTypeEnum creditType, DateTime date, string desccription);
        /// <summary>
        /// Delete a Credit object to the checking register
        /// </summary>
        /// <param name="credit"></param>
        [OperationContract] 
        void DeleteCredit(int id);
        /// <summary>
        /// Returns a list of all transactions in the register in sorted order(see the Some Notes About the Data Contracts section above)
        /// </summary>
        /// <returns></returns>
        [OperationContract] 
        TransactionList GetAllTransactions();
        /// <summary>
        /// Gets a list of transactions that are between the given dates(inclusively). Items should be sorted according to defined rules.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        [OperationContract] 
        TransactionList GetTransactionsByDateRange(DateTime start, DateTime end);
        /// <summary>
        /// Gets a list of credit transactions that are of the given type.Items should be sorted according to defined rules.
        /// </summary>
        /// <param name="creditType"></param>
        /// <returns></returns>
        [OperationContract] 
        TransactionList GetCreditsByType(CreditTypeEnum creditType);
        /// <summary>
        /// Gets a list of debit transactions that are of the given type.Items should be sorted according to defined rules.
        /// </summary>
        /// <param name="debitType"></param>
        /// <returns></returns>
        [OperationContract] 
        TransactionList GetDebitsByType(DebitTypeEnum debitType);
    }
}
