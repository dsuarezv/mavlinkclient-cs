namespace mavlinkscope
{
    partial class ScopeForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.LogFileTextBox = new System.Windows.Forms.TextBox();
            this.OpenButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Log file";
            // 
            // LogFileTextBox
            // 
            this.LogFileTextBox.Location = new System.Drawing.Point(61, 10);
            this.LogFileTextBox.Name = "LogFileTextBox";
            this.LogFileTextBox.Size = new System.Drawing.Size(474, 20);
            this.LogFileTextBox.TabIndex = 1;
            this.LogFileTextBox.Text = "C:\\Users\\David\\Desktop\\1.mavlink";
            // 
            // OpenButton
            // 
            this.OpenButton.Location = new System.Drawing.Point(542, 9);
            this.OpenButton.Name = "OpenButton";
            this.OpenButton.Size = new System.Drawing.Size(75, 23);
            this.OpenButton.TabIndex = 2;
            this.OpenButton.Text = "Open";
            this.OpenButton.UseVisualStyleBackColor = true;
            this.OpenButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // ScopeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 530);
            this.Controls.Add(this.OpenButton);
            this.Controls.Add(this.LogFileTextBox);
            this.Controls.Add(this.label1);
            this.Name = "ScopeForm";
            this.Text = "MavLink Scope";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox LogFileTextBox;
        private System.Windows.Forms.Button OpenButton;
    }
}