using SharedLib;
using SharedLib.Interfaces;
using SharedLib.Models;
using StockServiceLib.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading;

namespace StockServiceLib.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class StockService : IStockService
    {
        #region Properties
        /// <summary>
        /// Maintains a list of clients connected to the service, keyed by the client session ID
        /// </summary>
        private ConcurrentDictionary<string, ClientContainer> m_Clients { get; set; }
        /// <summary>
        /// List of Stock objects, keyed by the stock’s ticker symbol
        /// </summary>
        private ConcurrentDictionary<string, Stock> m_Stocks { get; set; }
        /// <summary>
        /// Random number generator
        /// </summary>
        private Random m_Rnd { get; set; }
        /// <summary>
        /// Timer that will fire periodically, changing a stock’s value and notifying clients
        /// </summary>
        public Timer m_Timer { get; set; }
        #endregion
        #region Constructors
        public StockService()
        {
            this.m_Clients = new ConcurrentDictionary<string, ClientContainer>();
            this.m_Stocks = new ConcurrentDictionary<string, Stock>();
            this.m_Rnd = new Random();

            string[] symbols = { "MSFT", "IBM", "AAPL", "GOOG", "YHOO", "INTC" };
            foreach (string symbol in symbols)
            {
                AddNewStock(symbol, m_Rnd.Next(10, 30)); // AddNewStock will be added later...
            }
            m_Timer = new Timer(StockTimerCallback, null, 2000, 2000); // StockTimerCallback later...
        }
        #endregion
        #region Public methods
        public void AddNewStock(string symbol, decimal price)
        {
            try
            {
                symbol = symbol.ToUpper().Trim();
                if (!m_Stocks.ContainsKey(symbol))
                {
                    Stock stock = new Stock(symbol, price);
                    m_Stocks.TryAdd(symbol, stock);
                }
                else
                {
                    string msg = string.Format("Stock symbol '{0}' already exists.", symbol);
                    Console.WriteLine(msg);
                    throw new Exception(msg);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred adding stock info: {0}", ex.Message);
                throw new FaultException(ex.Message);
            }
        }
        public Stock GetStockQuote(string symbol)
        {
            var stock = m_Stocks.FirstOrDefault(s => s.Key.Equals(symbol));
            if (!stock.Equals(default(KeyValuePair<string, Stock>)))
            {
                return stock.Value;
            }
            throw new FaultException("Symbol not found");
        }
        public void Login()
        {
            string sessionID = OperationContext.Current.SessionId;
            IStockCallback client = OperationContext.Current.GetCallbackChannel<IStockCallback>();
            if (!m_Clients.ContainsKey(sessionID))
            {
                Console.WriteLine("Client '{0}' logged in.", sessionID);
                // Add new ClientContainer that stores the client callback object and sets
                // IsActive to false
                m_Clients.TryAdd(sessionID, new ClientContainer(client, false));
            }
            else
            {
                var msg = string.Format("A client with the token '{0}' has already logged in!",
                 sessionID);
                Console.WriteLine(msg);
                throw new FaultException(msg);
            }
        }
        public void Logout(string sessionID = null)
        {
            try
            {
                if (string.IsNullOrEmpty(sessionID))
                {
                    sessionID = OperationContext.Current.SessionId;
                }
                if (m_Clients.ContainsKey(sessionID))
                {
                    Console.WriteLine("Client '{0}' logged off.", sessionID);
                    ClientContainer removedItem;
                    if (!m_Clients.TryRemove(sessionID, out removedItem))
                    {
                        var msg = string.Format("Unable to remove client '{0}'", sessionID);
                    }
                }
            }
            catch (Exception ex)
            {
                var msg = string.Format("An error occurred removing client '{0}': {1}",
                 sessionID, ex.Message);
                Console.WriteLine(msg);
                throw new FaultException(msg);
            }
        }
        public void StartTickerMonitoring()
        {
            string sessionID = OperationContext.Current.SessionId;
            if (m_Clients.ContainsKey(sessionID))
            {
                m_Clients[sessionID].IsActive = true;
            }
        }
        public void StopTickerMonitoring()
        {
            string sessionID = OperationContext.Current.SessionId;
            if (m_Clients.ContainsKey(sessionID))
            {
                m_Clients[sessionID].IsActive = false;
            }
        }
        #endregion
        #region Private methods
        private void StockTimerCallback(object state)
        {
            // Create a new random stock transaction
            Stock stock = m_Stocks[m_Stocks.Keys.ElementAt(m_Rnd.Next(m_Stocks.Count))];
            // Get a random value between -1.00 and 1.00
            decimal change = ((decimal)m_Rnd.Next(-100, 100)) / 100M;
            // Make sure share price cannot go negative
            if (stock.Price + change < 0)
            {
                change = -change;
            }
            // Update stock price
            stock.Price += change;
            var tx = new StockTransaction(stock, DateTime.Now, change, m_Rnd.Next(1, 1000));
            // Notify subscribed clients
            foreach (string key in m_Clients.Keys.ToList())
            {
                try
                {
                    if (m_Clients[key].IsActive)
                    {
                        m_Clients[key].ClientCallback.StockUpdated(tx);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error contacting client '{0}': {1}", key, ex.Message);
                    Logout(key);
                }
            }
        }
        #endregion
    }
}