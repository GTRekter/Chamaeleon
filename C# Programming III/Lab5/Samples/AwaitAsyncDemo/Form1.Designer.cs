namespace AwaitAsyncDemo
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
			this.noThreadButton = new System.Windows.Forms.Button();
			this.threadingButton = new System.Windows.Forms.Button();
			this.outputListBox = new System.Windows.Forms.ListBox();
			this.downloadButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// noThreadButton
			// 
			this.noThreadButton.Location = new System.Drawing.Point(10, 12);
			this.noThreadButton.Name = "noThreadButton";
			this.noThreadButton.Size = new System.Drawing.Size(122, 32);
			this.noThreadButton.TabIndex = 0;
			this.noThreadButton.Text = "No Threading";
			this.noThreadButton.UseVisualStyleBackColor = true;
			this.noThreadButton.Click += new System.EventHandler(this.noThreadButton_Click);
			// 
			// threadingButton
			// 
			this.threadingButton.Location = new System.Drawing.Point(138, 12);
			this.threadingButton.Name = "threadingButton";
			this.threadingButton.Size = new System.Drawing.Size(122, 32);
			this.threadingButton.TabIndex = 1;
			this.threadingButton.Text = "Threading";
			this.threadingButton.UseVisualStyleBackColor = true;
			this.threadingButton.Click += new System.EventHandler(this.threadingButton_Click);
			// 
			// outputListBox
			// 
			this.outputListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.outputListBox.FormattingEnabled = true;
			this.outputListBox.IntegralHeight = false;
			this.outputListBox.Location = new System.Drawing.Point(12, 50);
			this.outputListBox.Name = "outputListBox";
			this.outputListBox.Size = new System.Drawing.Size(472, 254);
			this.outputListBox.TabIndex = 2;
			// 
			// downloadButton
			// 
			this.downloadButton.Location = new System.Drawing.Point(266, 12);
			this.downloadButton.Name = "downloadButton";
			this.downloadButton.Size = new System.Drawing.Size(122, 32);
			this.downloadButton.TabIndex = 3;
			this.downloadButton.Text = "Download";
			this.downloadButton.UseVisualStyleBackColor = true;
			this.downloadButton.Click += new System.EventHandler(this.downloadButton_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(496, 316);
			this.Controls.Add(this.downloadButton);
			this.Controls.Add(this.outputListBox);
			this.Controls.Add(this.threadingButton);
			this.Controls.Add(this.noThreadButton);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button noThreadButton;
		private System.Windows.Forms.Button threadingButton;
		private System.Windows.Forms.ListBox outputListBox;
		private System.Windows.Forms.Button downloadButton;
	}
}

