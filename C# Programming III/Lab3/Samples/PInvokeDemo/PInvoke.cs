using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace PInvokeDemo
{
	/// <summary>
	/// Flags that define appearance and behaviour of a standard message box displayed by a call to the MessageBox function.
	/// </summary>
	[Flags]
	public enum MessageBoxOptions : uint
	{
		Ok = 0x000000,
		OkCancel = 0x000001,
		AbortRetryIgnore = 0x000002,
		YesNoCancel = 0x000003,
		YesNo = 0x000004,
		RetryCancel = 0x000005,
		CancelTryContinue = 0x000006,

		IconHand = 0x000010,
		IconQuestion = 0x000020,
		IconExclamation = 0x000030,
		IconAsterisk = 0x000040,
		UserIcon = 0x000080,

		IconWarning = IconExclamation,
		IconError = IconHand,
		IconInformation = IconAsterisk,
		IconStop = IconHand,

		DefButton1 = 0x000000,
		DefButton2 = 0x000100,
		DefButton3 = 0x000200,
		DefButton4 = 0x000300,

		ApplicationModal = 0x000000,
		SystemModal = 0x001000,
		TaskModal = 0x002000,

		Help = 0x004000, //Help Button
		NoFocus = 0x008000,

		SetForeground = 0x010000,
		DefaultDesktopOnly = 0x020000,
		Topmost = 0x040000,
		Right = 0x080000,
		RTLReading = 0x100000
	}

	/// <summary>
	/// Represents possible values returned by the MessageBox function.
	/// </summary>
	public enum MessageBoxResult : uint
	{
		Ok = 1,
		Cancel,
		Abort,
		Retry,
		Ignore,
		Yes,
		No,
		Close,
		Help,
		TryAgain,
		Continue,
		Timeout = 32000
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct RECT
	{
		public int Left;        // x position of upper-left corner
		public int Top;         // y position of upper-left corner
		public int Right;       // x position of lower-right corner
		public int Bottom;      // y position of lower-right corner
		public override string ToString()
		{
			return string.Format("[({0}, {1}), ({2}, {3})]", Left, Top, Right, Bottom);
		}
		public int Width { get { return Right - Left + 1; } }
		public int Height { get { return Bottom - Top + 1; } }
	}

	static class PInvoke
	{
		// Use DllImport to import the Win32 MessageBox function.
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int MessageBox(
		  IntPtr hWnd, string text, string caption, uint type);

		// Find window by title
		[DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
		public static extern IntPtr FindWindowByCaption(
			IntPtr ZeroOnly, string lpWindowName);

		// Get the window's position and size
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

		// Move window
		[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
		public static extern void MoveWindow(IntPtr hwnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

	}
}
