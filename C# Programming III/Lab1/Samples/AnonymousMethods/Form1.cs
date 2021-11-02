using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnonymousMethods
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();

			string test = "ABC";
			int x = 1;

			testButton2.Click += delegate (object sender, EventArgs e)
			{
				MessageBox.Show($"testButton2 was clicked: {test}, {x}");
				++x;
			};
		}

		private void testButton1_Click(object sender, EventArgs e)
		{
			MessageBox.Show("testButton1 was clicked");
		}
	}
}
