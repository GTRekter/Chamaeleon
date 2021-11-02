using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;

namespace MemoryConcepts
{
	public class Person : IDisposable
	{
		#region Fields
		private bool m_Disposed = false;
		#endregion Fields

		#region Properties
		public long ID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime DOB { get; set; }
		// Unmanaged Resources
		public IntPtr AllocatedMemory { get; set; } 
		// Disposable resources
		public Image Photo { get; set; }
		#endregion Properties

		#region Constructors
		public Person(long id, string lastName, string firstName, DateTime dob)
		{
			ID = id;
			LastName = lastName;
			FirstName = firstName;
			DOB = dob;
			
			// Allocate memory, unmanaged
			IntPtr hglobal = Marshal.AllocHGlobal(100);
			
			// Allocate managed resource
			string fileName = string.Format("{0}.jpg", ID);
			if (File.Exists(fileName))
			{
				Photo = Image.FromFile(fileName);
			}
			else
			{
				Photo = new Bitmap(64, 64, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
			}
		}
		#endregion Constructors

		#region Overrides
		public override string ToString()
		{
			 return string.Format("{0:000-00-0000} {1,-15} {2,-15} {3:MM/dd/yyyy}", ID, LastName, FirstName, DOB);
		}
		#endregion Overrides

		#region Disposing Methods
		private void Dispose(bool disposing)
		{
			Console.WriteLine();
			Console.WriteLine("Dispose({0}) Person ID {1} on thread ID: {2}", disposing, ID, Thread.CurrentThread.ManagedThreadId);
			if (!m_Disposed)
			{
				// If disposing equals true, dispose all managed resources.
				// If disposing is false, this is already being called by the garbage collector
				// and the managed resources may already have been disposed of.
				if (disposing)
				{
					// Dispose managed resources here...
					if (Photo != null)
					{
						Console.WriteLine("  - Disposing Person ID {0} managed objects on thread: {1}", ID, Thread.CurrentThread.ManagedThreadId);
						Photo.Dispose();
						Photo = null;
					}
				}

				// Call the appropriate methods to clean up unmanaged resources here...
				Console.WriteLine("  - Disposing Person ID {0} unmanaged objects on thread: {1}", ID, Thread.CurrentThread.ManagedThreadId);
				Marshal.FreeHGlobal(AllocatedMemory);
				AllocatedMemory = IntPtr.Zero;
			}
			m_Disposed = true;
			Console.WriteLine();
		}

		public void Dispose()
		{
			Dispose(true);
			// This object will be cleaned up by the Dispose method.  Therefore, you should
			// call GC.SupressFinalize to take this object off the finalization queue 
			// and prevent finalization code for this object from executing a second time.
			GC.SuppressFinalize(this);
		}

		~Person()  // Finalizer
		{
			// Do not re-create Dispose clean-up code here.  Calling Dispose(false) is 
			// optimal in terms of readability and maintainability.
			Dispose(false);
		}
		#endregion Disposing Methods
	}
}
