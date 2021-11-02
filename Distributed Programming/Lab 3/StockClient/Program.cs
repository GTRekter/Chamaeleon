using SharedLib.Interfaces;
using SharedLib.Models;
using StockClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace StockClient
{
    class Program
    {
        /// <summary>
        /// object that will contain a reference to the server proxy for access by all methods in this class
        /// </summary>
        static IStockService m_Proxy = null;
        /// <summary>
        /// object that will receive callback events from the service
        /// </summary>
        static StockMonitor m_Monitor = null;

        static void Main(string[] args)
        {
            try
            {
                MenuChoicesEnum choice = MenuChoicesEnum.Quit;
                m_Monitor = new StockMonitor();
                m_Proxy = ProxyGen.GetChannel<IStockService>(m_Monitor);
                m_Proxy.Login();
                do
                {
                    Console.Clear();
                    choice = ConsoleHelpers.ReadEnum<MenuChoicesEnum>("Enter selection: ");
                    switch (choice)
                    {
                        case MenuChoicesEnum.GetStockQuote:
                            GetStockQuote();
                            break;
                        case MenuChoicesEnum.AddStock:
                            AddStock();
                            break;
                        case MenuChoicesEnum.StartMonitoring:
                            MonitorStocks();
                            break;
                    }
                    Console.Write("Press <ENTER> to continue...");
                    Console.ReadLine();
                } while (choice != MenuChoicesEnum.Quit);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("An error occurred: {0}", ex.Message);
                Console.ResetColor();
            }
            finally
            {
                if (m_Proxy != null)
                {
                    m_Proxy.Logout();
                }
            }
            Console.Write("Press <ENTER> to quit...");
            Console.ReadLine();
        }

        #region Private methods
        private static void AddStock()
        {
            try { 
                string symbol = ConsoleHelpers.ReadString("Insert the symbol: ");
                decimal price = ConsoleHelpers.ReadDecimal("Insert the price: ");
                m_Proxy.AddNewStock(symbol, price);
            }
            catch (FaultException ex)
            {
                Console.WriteLine("Could not add stock quote: {0}", ex.Reason);
            }

        }
        private static void GetStockQuote()
        {
            try
            {
                string symbol = ConsoleHelpers.ReadString("Insert the symbol: ");
                Stock stock = m_Proxy.GetStockQuote(symbol);
                Console.WriteLine(stock.ToString());
            }
            catch (FaultException ex)
            {
                Console.WriteLine("Could not retrieve stock quote: {0}", ex.Reason);
            }
        }
        private static void MonitorStocks()
        {
            Console.WriteLine("The stock monitoring has begun. Press <ENTER> to stop");
            m_Proxy.StartTickerMonitoring();
            Console.ReadLine();
            m_Proxy.StopTickerMonitoring();
        }
        #endregion
    }
}