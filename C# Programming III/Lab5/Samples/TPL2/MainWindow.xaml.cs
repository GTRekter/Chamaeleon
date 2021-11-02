using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Net;
using System.Windows.Shapes;
using System.Threading;
using System.Threading.Tasks;

namespace TplWpf
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		#region Fields
		/// <summary>
		/// This object is used by the threads to monitor a cancellation notification
		/// </summary>
		CancellationTokenSource m_TokenSource = null;
		
		private TextBox[] m_TextBoxes;
		private string[] m_Urls = new[] { 
			"http://www.amazon.com", 
			"http://www.msdn.com", 
			"http://www.microsoft.com/visualstudio/en-us", 
			"http://www.ucsd.edu", 
			"http://extension.ucsd.edu", 
			"http://www.apress.com",
			"http://www.microsoft.com", 
			"http://www.google.com", 
			"http://www.yahoo.com", 
			"http://www.bing.com", 
			"http://www.ask.com", 
			"http://www.nasa.gov/"
		};
		#endregion Fields

		#region Constructor
		public MainWindow()
		{
			InitializeComponent();
			m_TextBoxes = new TextBox[] { 
				textBox1, textBox2, textBox3, textBox4, textBox5, textBox6,
				textBox7, textBox8, textBox9, textBox10, textBox11, textBox12
			};
		}
		#endregion Constructor

		#region Methods
		/// <summary>
		/// Method to download a webpage's text source
		/// </summary>
		/// <param name="address">Address to download</param>
		/// <returns>HTML source text</returns>
		public string DownloadWebPage(Uri address)
		{
			// We will use tcs to monitor completion of the asynchronous call to 
			// WebClient.DownloadStringsAsync()
			TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();

			// Indicate that we want cancellation notification on this thread
			m_TokenSource.Token.ThrowIfCancellationRequested();

			using (WebClient client = new WebClient())
			{
				// We are pointing the DownloadStringCompleted event to the following lambda
				// expression.  This way it can utilize the tcs variable declared above.
				// Otherwise we would need to create a class level field for each of the 
				// possible threads that could be accessed by the event handler.
				client.DownloadStringCompleted += (sender, args) =>
				{
					// This lambda expression is called when the DownloadStringAsync method call
					// below is complete.  It is strange to see things in this order, but it
					// is common practice with TPL.
					//   args is DownloadStringCompletedEventArgs : AsyncCompletedEventArgs
					//   Properties: Result, Cancelled, Error, UserState
					Console.WriteLine("Done loading '{0}'", address);
					if (args.Error != null)
					{
						// Store the exception for use by the caller.
						// Signal completion with exception.
						tcs.SetException(args.Error);
					}
					else if (args.Cancelled)
					{
						// Indicates that the task was cancelled.
						// Signal completion with cancellation.
						tcs.SetCanceled();
					}
					else
					{
						// Save results.
						// Signal completion with success.
						tcs.SetResult(args.Result);
					}
				};
				
				// Start downloading the page source.
				// When done, call the lambda expression above
				client.DownloadStringAsync(address);
				
				// Wait until task completes.
				// This will block until tcs has a completion state set.
				tcs.Task.Wait();
				return tcs.Task.Result;
			}
		}
		#endregion Methods

		#region Event Handlers
		/// <summary>
		/// Start the download tasks
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void goButton_Click(object sender, RoutedEventArgs e)
		{
			// Setup screen items
			goButton.IsEnabled = false;
			cancelButton.IsEnabled = true;

			foreach (TextBox tb in m_TextBoxes)
			{
				tb.Clear();
			}

			// Create a new cancellation token
			m_TokenSource = new CancellationTokenSource();
			
			// List of tasks
			List<Task> tasks = new List<Task>();
						
			// This field will allow re-synchronization with the UI thread
			var ui = TaskScheduler.FromCurrentSynchronizationContext();
			
			// Spawn the tasks
			for (int i = 0; i < m_Urls.Length; i++)
			{
				// Create a temporary field called index.
				// We don't want to use i in this case because it will be "shared" by multiple threads.
				// Since index is declared within the for loop, each iteration will
				// have its own entry on the stack and be safe from other threads.
				int index = i;
				
				// Create a new Task using the Task.Factory.StartNew method.
				// Since I know that DownloadWebPage returns a string, I could have 
				// also used Task<string>.Factory.StartNew and made compute
				// a Task<string> object.  However, the "var" technique is common.
				var compute = Task.Factory.StartNew(() =>
					{
						// The task simply calls the DownloadWebPage method above
						return DownloadWebPage(new Uri(m_Urls[index]));
					}, 
					m_TokenSource.Token);  // Token to monitor for cancellation...

				tasks.Add(compute);  // Keep track of all tasks created

				// If all tasks complete, then execute the lambda expression on the ui thread:
				//   m_TextBoxes[index].Text = compute.Result
				Task<string> displayResults = compute.ContinueWith(
					resultTask => m_TextBoxes[index].Text = compute.Result,
					CancellationToken.None,
					TaskContinuationOptions.OnlyOnRanToCompletion,
					ui);

				// If a cancellation was detected, output the cancelled status on the ui thread
				Task<string> displayCancelledTasks = compute.ContinueWith(
					resultTask => m_TextBoxes[index].Text = "Cancelled",
					CancellationToken.None,
					TaskContinuationOptions.OnlyOnCanceled, 
					ui);
			}

			// When all tasks complete, good or bad, reset the screen, again on the ui thread
			Task.Factory.ContinueWhenAll(tasks.ToArray(),
				result =>
				{
					goButton.IsEnabled = true;
					cancelButton.IsEnabled = false;
				}, CancellationToken.None, TaskContinuationOptions.None, ui);
		}

		/// <summary>
		/// Cancel the download tasks
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cancelButton_Click(object sender, RoutedEventArgs e)
		{
			m_TokenSource.Cancel();
		}
		#endregion Event Handlers
	}
}
