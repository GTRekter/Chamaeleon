using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace PInvokeDemo
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.Title = "PInvoke Demo";
			Console.Write("Press <Enter> to start...");
			Console.ReadLine();

			// The following command doesn't always work...
			IntPtr handle = Process.GetCurrentProcess().MainWindowHandle;
			// Use PInvoke to get the same value - works only if one window has this title
			IntPtr handle2 = PInvoke.FindWindowByCaption(IntPtr.Zero, Console.Title);

			Console.WriteLine("Handles match? {0} == {1} - {2}", handle, handle2, handle == handle2);

			RECT rect;
			PInvoke.GetWindowRect(handle2, out rect);
			Console.WriteLine("Window Position: {0}", rect);

			Console.Write("Press <Enter> to move window...");
			Console.ReadLine();
			PInvoke.MoveWindow(handle2, rect.Left + 100, rect.Top + 100, 
				rect.Width, rect.Height + 300, true);

			PInvoke.GetWindowRect(handle2, out rect);
			Console.WriteLine("New Window Position: {0}", rect);


			Console.Write("Press <Enter> to show message box...");
			Console.ReadLine();
			
			MessageBoxResult mbr = (MessageBoxResult)PInvoke.MessageBox(
				IntPtr.Zero, "Hello World!", "Hello Dialog Title", 
				(uint)(MessageBoxOptions.YesNo | MessageBoxOptions.IconQuestion));

			if (mbr == MessageBoxResult.Yes)
			{
				Console.WriteLine("You chose Yes!");
			}
			else
			{
				Console.WriteLine("You chose No!");
			}

			Console.Write("Press <Enter> to quit...");
			Console.ReadLine();
		}
	}
}
