using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLib;
using System.Numerics;
using System.ServiceModel;
using SharedLib.Interfaces;

namespace MathServiceLib.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Single)]
    public class MathService : IMathService
    {
        public BigInteger Sqrt(BigInteger value)
        {
            return (BigInteger)Math.Exp(BigInteger.Log(value) / 2);
        }

        public bool IsPrime(BigInteger value)
        {
            if (value <= 1)
            {
                // Eliminate 0 & 1
                return false;
            }
            else if ((value <= 3) || (value == 5))
            {
                // 2, 3 & 5 are prime
                return true;
            }
            else if (value.IsEven)
            {
                // Even numbers are not prime
                return false;
            }
            // Check for numbers divisible by 5
            if (value % 5 == 0)
            {
                // Any multiple of 5 (except 5) is not prime
                return false;
            }
            // If n is a positive composite integer, then n has a prime divisor
            // less than or equal to sqrt(n)
            BigInteger max = Sqrt(value);
            int counter = 3;
            for (BigInteger i = 3; i <= max; i += 2)
            {
                if (counter == 4)
                {
                    counter = 0;
                    continue;
                }
                counter++;
                if (value % i == 0)
                {
                    // Found a factor, not prime
                    return false;
                }
            }
            // No factors found
            return true;
        }
    }
}
