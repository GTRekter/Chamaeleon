using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;


namespace SharedLib.Models
{
    [DataContract]
    public class StockTransaction
    {
        #region Properties
        /// <summary>
        /// Stock object
        /// </summary>
        [DataMember]
        public Stock Stock { get; set; }
        /// <summary>
        /// Date/time of the transaction
        /// </summary>
        [DataMember]
        public DateTime Time { get; set; }
        /// <summary>
        /// Amount that the stock price changed
        /// </summary>
        [DataMember]
        public decimal Change { get; set; }
        /// <summary>
        /// Number of shares traded at the new price
        /// </summary>
        [DataMember]
        public int Shares { get; set; }
        #endregion
        #region Constructors
        public StockTransaction()
        {
        }
        public StockTransaction(Stock stock, DateTime time, decimal change, int shares)
        {
            this.Stock = stock;
            this.Time = time;
            this.Change = change;
            this.Shares = shares;
        }
        #endregion
        public override string ToString()
        {
            char direction = '=';
            if (Change < 0)
            {
                direction = '\u25BC';
            }
            else if (Change > 0)
            {
                direction = '\u25B2';
            }
            return string.Format("{0:yyyy-MM-dd HH:mm:ss} {1} {2} {3,10:N2} [{4,8:N0}]",
            Time, Stock, direction, Change, Shares);
        }
    }
}
