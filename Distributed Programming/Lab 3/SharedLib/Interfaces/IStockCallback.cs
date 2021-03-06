using SharedLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Interfaces
{
    public interface IStockCallback
    {
        [OperationContract(IsOneWay = true)]
        void StockUpdated(StockTransaction tx);
    }
}
