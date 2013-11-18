using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MavLink;

namespace mavlinkscope
{
    public partial class Form1 : Form
    {

        private MavLinkUdpClient mMavLink;
        private Msg_attitude mAttitudeState;

        public Form1()
        {
            InitializeComponent();
            InitializeMavLink();
        }

        private void InitializeMavLink()
        {
            mMavLink = new MavLinkUdpClient();
            mMavLink.Initialize();
            mMavLink.BeginHeartBeatLoop();
            mMavLink.HeartBeatUpdateRateMs = 100;

            mAttitudeState = (Msg_attitude)mMavLink.UavState.Get("ATTITUDE");
        }

        private void RollTrackbar_Scroll(object sender, EventArgs e)
        {
            mAttitudeState.roll = RollTrackbar.Value / 100f;
            UpdateAttitude();
        }

        private void PitchTrackbar_Scroll(object sender, EventArgs e)
        {
            mAttitudeState.pitch = PitchTrackbar.Value / 100f;
            UpdateAttitude();
        }

        private void YawTrackbar_Scroll(object sender, EventArgs e)
        {
            mAttitudeState.yaw = YawTrackbar.Value / 100f;
            UpdateAttitude();
        }

        private void UpdateAttitude()
        {
            //mMavLink.SendMessage(mMavLink.MsgAttitude);
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
