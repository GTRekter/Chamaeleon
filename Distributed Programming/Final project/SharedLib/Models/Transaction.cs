using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Models
{
    [DataContract]
    [KnownType(typeof(Debit))]
    [KnownType(typeof(Credit))]
    public class Transaction
    {
        /// <summary>
        /// Id of the transaction
        /// </summary>
        [DataMember]
        public int Id { get; set; }
        /// <summary>
        /// Date of the transaction
        /// </summary>
        [DataMember] 
        public DateTime Date { get; set; }
        /// <summary>
        /// Transaction description
        /// </summary>
        [DataMember]
        public string Description { get; set; }
        /// <summary>
        /// Transaction amount(positive value for deposits, negative for withdrawals)
        /// </summary>
        [DataMember]
        public decimal Amount { get; set; }
    }
}
