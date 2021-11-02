using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SharedLib.Models
{
    [CollectionDataContract]
    public class TransactionList : List<Transaction>
    {
        public TransactionList() { }
        public TransactionList(IEnumerable<Transaction> source) : base(source) { }
        public static TransactionList GetAllTransactions()
        {
            var query = DataStore.LoadData().OrderBy(t => t.Date).ThenBy(t => t.Amount).ThenBy(t => t.Description).ToList();
            return new TransactionList(query);
        }
        public static TransactionList GetTransactionsByDateRange(DateTime start, DateTime end)
        {
            var data = DataStore.LoadData();
            var query = from transactions in data
                        where transactions.Date >= start && transactions.Date <= end
                        select transactions;
            return new TransactionList(query);
        }
    }
}
