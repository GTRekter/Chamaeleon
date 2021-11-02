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
    public class Credit : Transaction
    {
        /// <summary>
        /// Type of credit being applied to the account. These enum values can be used as the description for the transaction, unless the value is “Unknown”, in which case the user should type in a value.
        /// </summary>
        [DataMember] 
        public CreditTypeEnum CreditType { get; set; }

        public static void AddCredit(int id, decimal amount, CreditTypeEnum creditType, DateTime date, string desccription)
        {
            var data = DataStore.LoadData();
            var credit = GetCreditById(id);
            if (credit == null)
            {
                // Credit doesn't exist
                Credit result = new Credit()
                {
                    Id = id,
                    Amount = amount,
                    CreditType = creditType,
                    Date = date,
                    Description = desccription
                };
                data.Add(result);
                DataStore.SaveData(data);
            }
        }
        public static void DeleteCredit(int id)
        {
            var data = DataStore.LoadData();
            data.RemoveAll(t => t is Credit && (t as Credit).Id == id);
            DataStore.SaveData(data);
        }
        public static Credit GetCreditById(int id)
        {
            var data = DataStore.LoadData();
            var query = from transactions in data
                        let credit = transactions as Credit
                        where credit != null && credit.Id == id
                        select credit;
            return query.FirstOrDefault();
        }
        public static TransactionList GetCreditsByType(CreditTypeEnum creditType)
        {
            var data = DataStore.LoadData();
            var query = from transactions in data
                        let credit = transactions as Credit
                        where credit.CreditType == creditType
                        select credit;
            return new TransactionList(query);
        }
    }
}
