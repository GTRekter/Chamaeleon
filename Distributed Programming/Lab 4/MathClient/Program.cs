using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLib;
using Utilities;
using SharedLib.Interfaces;
using System.Numerics;


namespace MathClient
{
    class Program
    {
        static IMathService m_Proxy = null;
        static int m_Tests = 250;
        static BigInteger m_StartNum = BigInteger.Parse("1234567890123456");


        static void Main(string[] args)
        {
            m_Proxy = ProxyGen.GetChannel<IMathService>();
            m_Tests = ConsoleHelpers.ReadInt("Insert the number of test: ");
            m_StartNum = ConsoleHelpers.ReadInt("Insert the start number: ");
            Console.WriteLine($"======= SYNC =======");
            TimeSpan tsSync = TestSync();
            Console.WriteLine($"======= ASYNC =======");
            TimeSpan tsAsync = TestAsync();
            Console.WriteLine($"===== TIMESPANs ======");
            Console.WriteLine($"Sync: {tsSync.TotalSeconds}");
            Console.WriteLine($"Async: {tsAsync.TotalSeconds}");
            Console.Write("Press <ENTER> to close the client...");
            Console.ReadLine();
        }


        private static TimeSpan TestSync()
        {
            DateTime start = DateTime.Now;
            for (BigInteger i = m_StartNum; i < m_StartNum + m_Tests; i++)
            {
                bool isPrime = m_Proxy.IsPrime(i);
                if (isPrime)
                {
                    Console.WriteLine("{0} is prime", i);
                }
            }
            DateTime end = DateTime.Now;
            return end.Subtract(start);
        }
        private static TimeSpan TestAsync()
        {
            DateTime start = DateTime.Now;
            Parallel.For(0, m_Tests, i =>
            {
                bool isPrime = m_Proxy.IsPrime(m_StartNum + i);
                if (isPrime)
                {
                    Console.WriteLine("{0} is prime", m_StartNum + i);
                }
            });
            DateTime end = DateTime.Now;
            return end.Subtract(start);
        }



    }
}
