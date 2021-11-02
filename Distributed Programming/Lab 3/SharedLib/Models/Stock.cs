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
    public class Stock
    {
        #region Properties
        /// <summary>
        /// Stock symbol, like "MSFT" for Microsoft
        /// </summary>
        [DataMember]
        public string Symbol { get; set; }
        /// <summary>
        /// Current trading price
        /// </summary>
        [DataMember]
        public decimal Price { get; set; }
        #endregion
        #region Constructors
        public Stock()
        {
        }
        public Stock(string symbol, decimal price)
        {
            this.Symbol = symbol;
            this.Price = price;
        }
        #endregion
        public override string ToString()
        {
            return string.Format("{0,-6} {1,10:N2}", Symbol, Price);
        }
    }
}
