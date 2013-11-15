namespace mavlinkscope
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
            this.RollTrackbar = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PitchTrackbar = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.YawTrackbar = new System.Windows.Forms.TrackBar();
            this.RollValueLabel = new System.Windows.Forms.Label();
            this.PitchValueLabel = new System.Windows.Forms.Label();
            this.YawValueLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.RollTrackbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PitchTrackbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YawTrackbar)).BeginInit();
            this.SuspendLayout();
            // 
            // RollTrackbar
            // 
            this.RollTrackbar.LargeChange = 100;
            this.RollTrackbar.Location = new System.Drawing.Point(68, 31);
            this.RollTrackbar.Maximum = 314;
            this.RollTrackbar.Minimum = -314;
            this.RollTrackbar.Name = "RollTrackbar";
            this.RollTrackbar.Size = new System.Drawing.Size(203, 45);
            this.RollTrackbar.TabIndex = 0;
            this.RollTrackbar.Scroll += new System.EventHandler(this.RollTrackbar_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Roll";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Pitch";
            // 
            // PitchTrackbar
            // 
            this.PitchTrackbar.LargeChange = 100;
            this.PitchTrackbar.Location = new System.Drawing.Point(68, 78);
            this.PitchTrackbar.Maximum = 314;
            this.PitchTrackbar.Minimum = -314;
            this.PitchTrackbar.Name = "PitchTrackbar";
            this.PitchTrackbar.Size = new System.Drawing.Size(203, 45);
            this.PitchTrackbar.TabIndex = 2;
            this.PitchTrackbar.Scroll += new System.EventHandler(this.PitchTrackbar_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Yaw";
            // 
            // YawTrackbar
            // 
            this.YawTrackbar.LargeChange = 100;
            this.YawTrackbar.Location = new System.Drawing.Point(68, 120);
            this.YawTrackbar.Maximum = 314;
            this.YawTrackbar.Minimum = -314;
            this.YawTrackbar.Name = "YawTrackbar";
            this.YawTrackbar.Size = new System.Drawing.Size(203, 45);
            this.YawTrackbar.TabIndex = 4;
            this.YawTrackbar.Scroll += new System.EventHandler(this.YawTrackbar_Scroll);
            // 
            // RollValueLabel
            // 
            this.RollValueLabel.AutoSize = true;
            this.RollValueLabel.Location = new System.Drawing.Point(278, 37);
            this.RollValueLabel.Name = "RollValueLabel";
            this.RollValueLabel.Size = new System.Drawing.Size(13, 13);
            this.RollValueLabel.TabIndex = 6;
            this.RollValueLabel.Text = "0";
            // 
            // PitchValueLabel
            // 
            this.PitchValueLabel.AutoSize = true;
            this.PitchValueLabel.Location = new System.Drawing.Point(278, 84);
            this.PitchValueLabel.Name = "PitchValueLabel";
            this.PitchValueLabel.Size = new System.Drawing.Size(13, 13);
            this.PitchValueLabel.TabIndex = 7;
            this.PitchValueLabel.Text = "0";
            // 
            // YawValueLabel
            // 
            this.YawValueLabel.AutoSize = true;
            this.YawValueLabel.Location = new System.Drawing.Point(278, 126);
            this.YawValueLabel.Name = "YawValueLabel";
            this.YawValueLabel.Size = new System.Drawing.Size(13, 13);
            this.YawValueLabel.TabIndex = 8;
            this.YawValueLabel.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 396);
            this.Controls.Add(this.YawValueLabel);
            this.Controls.Add(this.PitchValueLabel);
            this.Controls.Add(this.RollValueLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.YawTrackbar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PitchTrackbar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RollTrackbar);
            this.Name = "Form1";
            this.Text = "Mavlink Scope";
            ((System.ComponentModel.ISupportInitialize)(this.RollTrackbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PitchTrackbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YawTrackbar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar RollTrackbar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar PitchTrackbar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar YawTrackbar;
        private System.Windows.Forms.Label RollValueLabel;
        private System.Windows.Forms.Label PitchValueLabel;
        private System.Windows.Forms.Label YawValueLabel;
    }
}

