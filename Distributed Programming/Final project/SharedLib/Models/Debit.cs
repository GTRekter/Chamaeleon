using SharedLib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Models
{
    [DataContract]
    public class Debit : Transaction
    {
        /// <summary>
        /// Type of debit being applied to the account.These enum values can be used as the description for the transaction, unless the value is “Unknown”, in which case the user should type in a value.
        /// </summary>
        [DataMember]
        public DebitTypeEnum DebitType { get; set; }
        /// <summary>
        /// Check number, if applicable
        /// </summary>
        [DataMember]
        public int CheckNo { get; set; }
        /// <summary>
        /// Fee amount, if any, for this transaction
        /// </summary>
        [DataMember]
        public decimal Fee { get; set; }

        public static void AddDebit(int id, decimal amount, DateTime date, string desccription, int checkNo, DebitTypeEnum debitType, decimal fee)
        {
            var data = DataStore.LoadData();
            var debit = GetDebitById(id);
            if (debit == null)
            {
                // Debit doesn't exist
                Debit result = new Debit()
                {
                    Id = id,
                    Amount = amount,
                    DebitType = debitType,
                    Date = date,
                    Description = desccription,
                    CheckNo = checkNo,
                    Fee = fee,
                };
                data.Add(result);
                DataStore.SaveData(data);
            }
        }

        public static void DeleteDebit(int id)
        {
            var data = DataStore.LoadData();
            data.RemoveAll(t => t is Debit && (t as Debit).Id == id);
            DataStore.SaveData(data);
        }
        public static Debit GetDebitById(int id)
        {
            var data = DataStore.LoadData();
            var query = from transactions in data
                        let debit = transactions as Debit
                        where debit != null && debit.Id == id
                        select debit;
            return query.FirstOrDefault();
        }
        public static TransactionList GetDebitsByType(DebitTypeEnum debitType)
        {
            var data = DataStore.LoadData();
            var query = from transactions in data
                        let debit = transactions as Debit
                        where debit.DebitType == debitType
                        select debit;
            return new TransactionList(query);
        }
    }
}