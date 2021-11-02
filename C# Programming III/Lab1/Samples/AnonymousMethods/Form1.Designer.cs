namespace AnonymousMethods
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.testButton1 = new System.Windows.Forms.Button();
			this.testButton2 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// testButton1
			// 
			this.testButton1.Location = new System.Drawing.Point(12, 12);
			this.testButton1.Name = "testButton1";
			this.testButton1.Size = new System.Drawing.Size(97, 34);
			this.testButton1.TabIndex = 0;
			this.testButton1.Text = "Test";
			this.testButton1.UseVisualStyleBackColor = true;
			this.testButton1.Click += new System.EventHandler(this.testButton1_Click);
			// 
			// testButton2
			// 
			this.testButton2.Location = new System.Drawing.Point(12, 62);
			this.testButton2.Name = "testButton2";
			this.testButton2.Size = new System.Drawing.Size(97, 34);
			this.testButton2.TabIndex = 1;
			this.testButton2.Text = "Test 2";
			this.testButton2.UseVisualStyleBackColor = true;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(282, 253);
			this.Controls.Add(this.testButton2);
			this.Controls.Add(this.testButton1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button testButton1;
		private System.Windows.Forms.Button testButton2;
	}
}

