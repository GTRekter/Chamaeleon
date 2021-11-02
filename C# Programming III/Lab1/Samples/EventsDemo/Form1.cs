using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventsDemo
{
	public partial class Form1 : Form
	{
		Person p = new Person("Smith", "John", DateTime.Parse("1/4/2000"));

		public Form1()
		{
			InitializeComponent();

			button0.Click += button0Clicked;

			p.Changed += Person_Changed;
			p.ChangedDetailed += Person_ChangedDetailed;
		}

		private void Person_ChangedDetailed(object sender, PersonChangedEventArgs e)
		{
			MessageBox.Show(e.Message);
		}

		private void Person_Changed(object sender, EventArgs e)
		{
			MessageBox.Show("Person object has changed!");
		}

		private void testButton1_Click(object sender, EventArgs e)
		{
			MessageBox.Show("testButton1 was clicked");
			button0.Click -= button0Clicked;
		}

		private void testButton1_MouseDown(object sender, MouseEventArgs e)
		{
			//MessageBox.Show($"testButton1 was pressed at ({e.X}, {e.Y})");
		}

		private void button0Clicked(object sender, EventArgs e)
		{
			MessageBox.Show("0");
		}

		private void buttonClicked(object sender, EventArgs e)
		{
			if (sender is Button)
			{
				numberTextBox.Text = numberTextBox.Text + (sender as Button).Text;
			}
			p.DOB = p.DOB.AddDays(1);
		}
	}
}
