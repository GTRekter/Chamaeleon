using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Events
{
	public partial class Form1 : Form
	{
		Person person = new Person("Smith", "John", DateTime.Parse("1/2/33"));

		public Form1()
		{
			InitializeComponent();
			testButton3.Click += TestMethod1;
			testButton3.Click += TestMethod2;

			person.PersonChanged += Person_PersonChanged;
			person.SimpleChanged += Person_SimpleChanged;
		}

		private void Person_SimpleChanged(object sender, EventArgs e)
		{
			MessageBox.Show("The person has changed.");
		}

		private void Person_PersonChanged(object sender, Person.PersonChangedEventArgs e)
		{
			MessageBox.Show(e.Message);
		}

		private void testButton_Click(object sender, EventArgs e)
		{
			if (sender is Button)
			{
				MessageBox.Show($"{(sender as Button).Name} clicked");
			}

			person.LastName = "Jones";
		}

		private void testButton_MouseDown(object sender, MouseEventArgs e)
		{

		}

		private void TestMethod1(object sender, EventArgs e)
		{
			MessageBox.Show("1");
		}

		private void TestMethod2(object sender, EventArgs e)
		{
			MessageBox.Show("2");
		}
	}
}
