using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLib;
using SharedLib.Interfaces;

namespace StockServiceLib.Models
{
    public class ClientContainer
    {
        #region Properties
        /// <summary>
        /// Reference to a client callback object (ie. handle to a client object)
        /// </summary>
        public IStockCallback ClientCallback { get; set; }
        /// <summary>
        /// Indicates if the given client is set to receive callback messages.
        /// </summary>
        public bool IsActive { get; set; }
        #endregion
        #region Constructors
        public ClientContainer()
        {
        }
        public ClientContainer(IStockCallback clientCallback, bool isActive)
        {
            ClientCallback = clientCallback;
            IsActive = isActive;
        }
        #endregion
    }
}