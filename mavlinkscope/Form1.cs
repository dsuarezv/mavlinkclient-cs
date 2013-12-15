using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MavLinkNet;

namespace mavlinkscope
{
    public partial class Form1 : Form
    {

        private MavLinkUdpTransport mMavLink;
        private UasAttitude mAttitudeState;

        public Form1()
        {
            InitializeComponent();
        }

        private void InitializeMavLink()
        {
            mMavLink = new MavLinkUdpTransport();
            mMavLink.MavlinkSystemId = GetSystemId();
            mMavLink.Initialize();
            mMavLink.BeginHeartBeatLoop();
            mMavLink.HeartBeatUpdateRateMs = 100;

            mAttitudeState = (UasAttitude)mMavLink.UavState.Get("Attitude");
        }

        private byte GetSystemId()
        {
            byte result;

            if (!byte.TryParse(SystemIdTextBox.Text, out result)) return 180;

            return result;
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            InitializeMavLink();
            ConnectButton.Enabled = false;
        }

        private void RollTrackbar_Scroll(object sender, EventArgs e)
        {
            mAttitudeState.Roll = RollTrackbar.Value / 100f;
            UpdateAttitude();
        }

        private void PitchTrackbar_Scroll(object sender, EventArgs e)
        {
            mAttitudeState.Pitch = PitchTrackbar.Value / 100f;
            UpdateAttitude();
        }

        private void YawTrackbar_Scroll(object sender, EventArgs e)
        {
            mAttitudeState.Yaw = YawTrackbar.Value / 100f;
            UpdateAttitude();
        }

        private void UpdateAttitude()
        {
            //mMavLink.SendMessage(mAttitudeState);
            RollValueLabel.Text = GetAttitudeDegrees(RollTrackbar.Value);
            PitchValueLabel.Text = GetAttitudeDegrees(PitchTrackbar.Value);
            YawValueLabel.Text = GetAttitudeDegrees(YawTrackbar.Value);
        }

        private string GetAttitudeDegrees(int val)
        {
            return string.Format("{0:0.}º", val / 100f * 180f / Math.PI);
        }

    }
}
