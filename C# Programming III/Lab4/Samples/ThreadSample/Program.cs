using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Numerics;
using System.Runtime.CompilerServices;
using st = System.Timers;

namespace ThreadSample
{
	class Program
	{
        private static object m_ConsoleLock = new object();
        private static Random m_Rnd = new Random();
		private static Timer m_Timer;
		private static ManualResetEventSlim m_Quit = new ManualResetEventSlim();

		static void Main(string[] args)
		{
			// Setup screen
            Console.WindowWidth = 140;
            Console.WindowHeight = 54;
			Console.BufferWidth = Console.WindowWidth;
            Console.BufferHeight = 999;

			// Get user parameters
            decimal baseInterest = IOFunctions.GetDecimalFromUser("Enter starting interest percentage (0 - 20): ", 0M, 20M);
            decimal interestIncrement = IOFunctions.GetDecimalFromUser("Enter interest increment (0.1 - 10): ", 0.1M, 10M);
            int interestIncrements = IOFunctions.GetIntFromUser("Enter number of interest columns (1 - 12): ", 1, 12);
            decimal basePrincipal = IOFunctions.GetDecimalFromUser("Enter starting mortgage amount (25,000 - 500,000): ", 25000M, 500000M);
            decimal principalIncrement = IOFunctions.GetDecimalFromUser("Enter mortgage increment amount (1,000 - 25,000): ", 1000M, 25000M);
            int principalIncrements = IOFunctions.GetIntFromUser("Enter number of mortgage increments (1 - 50): ", 1, 50);
            int years = IOFunctions.GetIntFromUser("Enter term length of mortgage in years (1 - 50): ", 1, 50);
            
			// Output headers
			Console.Clear();
			OutputHeader(baseInterest, interestIncrement, interestIncrements);
			OutputMortgageAmounts(basePrincipal, principalIncrement, principalIncrements);

            Console.SetCursorPosition(0, 53);
            Console.Write("Press <ENTER> to quit...");

			Console.ForegroundColor = ConsoleColor.Yellow;

			// Start timer
			m_Timer = new Timer(OnTimerFired, null, 250, 250);

            // Start calculation threads
            for (int i = 0; i < interestIncrements; i++)
            {
                Thread t = new Thread(MortgageCalculationThread);
                ThreadParams tp = new ThreadParams()
                {
                    ID = i,
                    Interest = baseInterest + (interestIncrement * i),
                    StartPrincipal = basePrincipal,
                    PrincipalIncrement = principalIncrement,
                    Steps = principalIncrements,
                    Years = years
                };
                t.Start(tp);
            }
            
			// Wait for user input
			Console.ReadLine();
			m_Quit.Set();

			// Another ReadLine after threads have quit
			Console.ReadLine();
			m_Timer.Dispose();
		}

		/// <summary>
		/// Updates the number of threads currently executing
		/// </summary>
		/// <param name="arg"></param>
		private static void OnTimerFired(object arg)
		{
			Process proc = Process.GetCurrentProcess();
			lock (m_ConsoleLock)
			{
				Console.ForegroundColor = ConsoleColor.Magenta;
				Console.SetCursorPosition(50, 53);
				Console.Write("Thread Count: {0}  ", proc.Threads.Count);
			}
		}
		
        /// <summary>
        /// Perform the mortgage calculations
        /// </summary>
        /// <param name="state">Thread parameters</param>
        private static void MortgageCalculationThread(object state)
        {
            ThreadParams tp = state as ThreadParams;
            if (tp != null)
            {
                for (int i = 0; i < tp.Steps; i++)
                {
					if (m_Quit.IsSet)
					{
						return;
					}
                    decimal amt = MonthlyPmt(tp.StartPrincipal + (tp.PrincipalIncrement * i), tp.Interest, tp.Years);
                    OutputResult(tp.ID, i, amt);
                    // Artificial delay:
                    Thread.Sleep(m_Rnd.Next(500, 1000));
                }
            }
        }

		/// <summary>
		/// Calculates the monthly P & I payment for a mortgage
		/// </summary>
		/// <param name="principal">Amount owed</param>
		/// <param name="interestRate">Interest rate between 0 and 100</param>
		/// <param name="years">Number of year to make payments</param>
		/// <returns>Montly P & I amount</returns>
		private static decimal MonthlyPmt(decimal principal, decimal interestRate, int years)
		{
			decimal monthlyInterestRate = interestRate / (12M * 100M);
			double totalMonths = 12 * years;
			decimal divisor = 1M - ((decimal)Math.Pow(1 + (double)monthlyInterestRate, -totalMonths));
			decimal pmt = principal * monthlyInterestRate / divisor;
			return Math.Round(pmt, 2);
		}

        /// <summary>
        /// Outputs the given result to the appropriate screen location
        /// </summary>
        /// <param name="id">Thread id (also corresponds to the column)</param>
        /// <param name="step">Step, or row</param>
        /// <param name="value">Amount to display</param>
        private static void OutputResult(int id, int step, decimal value)
        {
            lock (m_ConsoleLock)
            {
				if (step % 2 == 0)
				{
					Console.ForegroundColor = (id % 2 == 0) ? ConsoleColor.Yellow : ConsoleColor.Green;
				}
				else
				{
					Console.ForegroundColor = (id % 2 == 0) ? ConsoleColor.DarkYellow : ConsoleColor.DarkGreen;
				}
                int col = (id + 1) * 10;
                int row = step + 2;
                Console.SetCursorPosition(col, row);
                Console.Write("{0,10:0.00}", value);
            }
        }

		/// <summary>
		/// Output the header
		/// </summary>
		/// <param name="rates"></param>
		private static void OutputHeader(decimal start, decimal increment, int count)
		{
			Console.ForegroundColor = ConsoleColor.White;
			Console.SetCursorPosition(0, 0);
			Console.Write("Mortgage |");

			for (int i = 0; i < count; i++)
			{
				Console.SetCursorPosition((i + 1) * 10, 0);
				Console.Write("{0,9}%", start + (increment * i));
			}
			string border = new string('=', (count + 1) * 10);
			Console.SetCursorPosition(0, 1);
			Console.Write(border);
		}

        /// <summary>
		/// Output the mortgage amounts in the left margin
        /// </summary>
        /// <param name="start">Starting amount</param>
        /// <param name="increment">Amount per increment</param>
        /// <param name="count">Number of increments</param>
        private static void OutputMortgageAmounts(decimal start, decimal increment, int count)
        {
			for (int i = 0; i < count; i++)
            {
				Console.ForegroundColor = (i % 2 == 0) ? ConsoleColor.White : ConsoleColor.Gray;
                Console.SetCursorPosition(0, i + 2);
                Console.Write("{0,9:#,#}|", start + (increment * i));
            }
        }
	}

    class ThreadParams
    {
        /// <summary>
        ///  ID of the thread (also corresponds to the output column)
        /// </summary>
        public int ID { get; set; }
        
        /// <summary>
        /// Interest amount (0 - 100)
        /// </summary>
        public decimal Interest { get; set; }
        
        /// <summary>
        /// Starting principal amount
        /// </summary>
        public decimal StartPrincipal { get; set; }

        /// <summary>
        /// Increment per step
        /// </summary>
        public decimal PrincipalIncrement { get; set; }
        
        /// <summary>
        /// Number of increments to perform
        /// </summary>
        public int Steps { get; set; }

        /// <summary>
        /// Mortgage term in years
        /// </summary>
        public int Years { get; set; }
    }
}
