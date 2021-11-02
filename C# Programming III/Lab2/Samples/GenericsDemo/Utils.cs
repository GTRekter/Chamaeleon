using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenericsDemo
{
	public static class Utils
	{
		//public static void Swap(ref int left, ref int right)
		//{
		//    int temp = left;
		//    left = right;
		//    right = temp;
		//}

		public static void Swap<T>(ref T left, ref T right)
		{
			T temp = left;
			left = right;
			right = temp;
		}


	}
}
