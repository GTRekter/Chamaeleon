using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwaitAsyncDemo
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void UpdateListBox(string text)
		{
			if (outputListBox.InvokeRequired)
			{
				var act = new Action<string>(UpdateListBox);
				outputListBox.BeginInvoke(act, text);
			}
			else
			{
				outputListBox.Items.Add(text);
			}
		}

		private void DoWorkSync()
		{
			UpdateListBox(string.Format("Thread ID of DoWorkSync: {0}", Thread.CurrentThread.ManagedThreadId));
			UpdateListBox(string.Format("[{0:HH:mm:ss.fff}] DoWorkSync start", DateTime.Now));
			Thread.Sleep(5000);
			UpdateListBox(string.Format("[{0:HH:mm:ss.fff}] DoWorkSync end", DateTime.Now));
		}

		private async void DoWorkAsync()
		{
			UpdateListBox(string.Format("[{0:HH:mm:ss.fff}] DoWorkAsync start", DateTime.Now));
			UpdateListBox(string.Format("Thread ID of DoWorkAsync: {0}", Thread.CurrentThread.ManagedThreadId));
			await Task.Run(() => 
				{
					UpdateListBox(string.Format("Thread ID of DoWorkAsync task: {0}", Thread.CurrentThread.ManagedThreadId));
					Thread.Sleep(5000);
				}
			);
			UpdateListBox(string.Format("[{0:HH:mm:ss.fff}] DoWorkAsync end", DateTime.Now));
		}
		
		private async Task<string> DownloadWebPageAsync(string url)
		{
			UpdateListBox(string.Format("Thread ID of DownloadWebPageAsync: {0}", Thread.CurrentThread.ManagedThreadId));
			return await Task.Run(() =>
			{
				UpdateListBox(string.Format("Thread ID of DownloadWebPageAsync task: {0}", Thread.CurrentThread.ManagedThreadId));
				using (WebClient client = new WebClient())
				{
					Thread.Sleep(5000);  // Artificial delay
					return client.DownloadString(url);
				}   // using
			});  // Task.Run()
		}

		#region Event Handlers
		private void noThreadButton_Click(object sender, EventArgs e)
		{
			UpdateListBox(string.Format("Thread ID of noThreadButton_Click: {0}", Thread.CurrentThread.ManagedThreadId));
			DoWorkSync();
		}

		private void threadingButton_Click(object sender, EventArgs e)
		{
			UpdateListBox(string.Format("Thread ID of threadingButton_Click: {0}", Thread.CurrentThread.ManagedThreadId));
			DoWorkAsync();
		}

		private async void downloadButton_Click(object sender, EventArgs e)
		{
			UpdateListBox(string.Format("Thread ID of downloadButton_Click: {0}", Thread.CurrentThread.ManagedThreadId));
			UpdateListBox(string.Format("[{0:HH:mm:ss.fff}] Download start", DateTime.Now));
			string source = await DownloadWebPageAsync("http://msdn.microsoft.com");
			// Do something with source...
			UpdateListBox(string.Format("Webpage length: {0}", source.Length));
			UpdateListBox(string.Format("[{0:HH:mm:ss.fff}] Download end", DateTime.Now));
		}
		#endregion Event Handlers
	}
}
