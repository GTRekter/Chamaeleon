namespace Events
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
			this.testButton = new System.Windows.Forms.Button();
			this.testButton2 = new System.Windows.Forms.Button();
			this.testButton3 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// testButton
			// 
			this.testButton.Location = new System.Drawing.Point(35, 28);
			this.testButton.Name = "testButton";
			this.testButton.Size = new System.Drawing.Size(82, 34);
			this.testButton.TabIndex = 0;
			this.testButton.Text = "Test";
			this.testButton.UseVisualStyleBackColor = true;
			this.testButton.Click += new System.EventHandler(this.testButton_Click);
			this.testButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.testButton_MouseDown);
			// 
			// testButton2
			// 
			this.testButton2.Location = new System.Drawing.Point(35, 82);
			this.testButton2.Name = "testButton2";
			this.testButton2.Size = new System.Drawing.Size(82, 35);
			this.testButton2.TabIndex = 1;
			this.testButton2.Text = "Test 2";
			this.testButton2.UseVisualStyleBackColor = true;
			this.testButton2.Click += new System.EventHandler(this.testButton_Click);
			// 
			// testButton3
			// 
			this.testButton3.Location = new System.Drawing.Point(35, 133);
			this.testButton3.Name = "testButton3";
			this.testButton3.Size = new System.Drawing.Size(82, 35);
			this.testButton3.TabIndex = 2;
			this.testButton3.Text = "Test 3";
			this.testButton3.UseVisualStyleBackColor = true;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(773, 494);
			this.Controls.Add(this.testButton3);
			this.Controls.Add(this.testButton2);
			this.Controls.Add(this.testButton);
			this.Name = "Form1";
			this.Text = "Event Tests";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button testButton;
		private System.Windows.Forms.Button testButton2;
		private System.Windows.Forms.Button testButton3;
	}
}

