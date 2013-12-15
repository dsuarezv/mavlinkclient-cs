namespace GpsFeeder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.UdpListenPortTextbox = new System.Windows.Forms.TextBox();
            this.UdpConnectPortTextbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.UdpConnectIpTextbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.VehicleIdTextbox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.ComponentIdTextbox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.UbloxSvInfoPacketTextbox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.ParsedUbloxTextbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "UDP listening port";
            // 
            // UdpListenPortTextbox
            // 
            this.UdpListenPortTextbox.Location = new System.Drawing.Point(112, 13);
            this.UdpListenPortTextbox.Name = "UdpListenPortTextbox";
            this.UdpListenPortTextbox.Size = new System.Drawing.Size(75, 20);
            this.UdpListenPortTextbox.TabIndex = 0;
            // 
            // UdpConnectPortTextbox
            // 
            this.UdpConnectPortTextbox.Location = new System.Drawing.Point(112, 39);
            this.UdpConnectPortTextbox.Name = "UdpConnectPortTextbox";
            this.UdpConnectPortTextbox.Size = new System.Drawing.Size(75, 20);
            this.UdpConnectPortTextbox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "UDP connect port";
            // 
            // UdpConnectIpTextbox
            // 
            this.UdpConnectIpTextbox.Location = new System.Drawing.Point(112, 65);
            this.UdpConnectIpTextbox.Name = "UdpConnectIpTextbox";
            this.UdpConnectIpTextbox.Size = new System.Drawing.Size(75, 20);
            this.UdpConnectIpTextbox.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "UDP connect IP";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.label4.Location = new System.Drawing.Point(16, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(496, 2);
            this.label4.TabIndex = 6;
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(437, 65);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(75, 23);
            this.ConnectButton.TabIndex = 5;
            this.ConnectButton.Text = "&Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // label5
            // 
            this.label5.Image = ((System.Drawing.Image)(resources.GetObject("label5.Image")));
            this.label5.Location = new System.Drawing.Point(16, 139);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(500, 297);
            this.label5.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 121);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "GPS location";
            // 
            // VehicleIdTextbox
            // 
            this.VehicleIdTextbox.Location = new System.Drawing.Point(276, 13);
            this.VehicleIdTextbox.Name = "VehicleIdTextbox";
            this.VehicleIdTextbox.Size = new System.Drawing.Size(69, 20);
            this.VehicleIdTextbox.TabIndex = 3;
            this.VehicleIdTextbox.Text = "100";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(199, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Vehicle ID";
            // 
            // ComponentIdTextbox
            // 
            this.ComponentIdTextbox.Location = new System.Drawing.Point(276, 39);
            this.ComponentIdTextbox.Name = "ComponentIdTextbox";
            this.ComponentIdTextbox.Size = new System.Drawing.Size(69, 20);
            this.ComponentIdTextbox.TabIndex = 4;
            this.ComponentIdTextbox.Text = "1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(199, 42);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Component ID";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 453);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(218, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "U-Blox Space Vehicle Info packet (SV-INFO)";
            // 
            // UbloxSvInfoPacketTextbox
            // 
            this.UbloxSvInfoPacketTextbox.Location = new System.Drawing.Point(16, 470);
            this.UbloxSvInfoPacketTextbox.Multiline = true;
            this.UbloxSvInfoPacketTextbox.Name = "UbloxSvInfoPacketTextbox";
            this.UbloxSvInfoPacketTextbox.Size = new System.Drawing.Size(340, 227);
            this.UbloxSvInfoPacketTextbox.TabIndex = 14;
            this.UbloxSvInfoPacketTextbox.Text = resources.GetString("UbloxSvInfoPacketTextbox.Text");
            this.UbloxSvInfoPacketTextbox.WordWrap = false;
            this.UbloxSvInfoPacketTextbox.TextChanged += new System.EventHandler(this.UbloxSvInfoPacketTextbox_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(361, 453);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 13);
            this.label10.TabIndex = 13;
            this.label10.Text = "Parsed data";
            // 
            // ParsedUbloxTextbox
            // 
            this.ParsedUbloxTextbox.Location = new System.Drawing.Point(364, 469);
            this.ParsedUbloxTextbox.Multiline = true;
            this.ParsedUbloxTextbox.Name = "ParsedUbloxTextbox";
            this.ParsedUbloxTextbox.Size = new System.Drawing.Size(152, 227);
            this.ParsedUbloxTextbox.TabIndex = 14;
            this.ParsedUbloxTextbox.WordWrap = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 798);
            this.Controls.Add(this.ParsedUbloxTextbox);
            this.Controls.Add(this.UbloxSvInfoPacketTextbox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.ComponentIdTextbox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.VehicleIdTextbox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.UdpConnectIpTextbox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.UdpConnectPortTextbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.UdpListenPortTextbox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "MavLink GPS Feeder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox UdpListenPortTextbox;
        private System.Windows.Forms.TextBox UdpConnectPortTextbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox UdpConnectIpTextbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox VehicleIdTextbox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox ComponentIdTextbox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox UbloxSvInfoPacketTextbox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox ParsedUbloxTextbox;
    }
}

