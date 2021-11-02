using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Interfaces
{
    [ServiceContract]
    public interface IMathService
    {
        [OperationContract]
        bool IsPrime(BigInteger number);
        [OperationContract]
        BigInteger Sqrt(BigInteger number);
    }
}
