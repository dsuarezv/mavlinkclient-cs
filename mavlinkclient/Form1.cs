using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MavLink;

namespace mavlinkclient
{
    public partial class Form1 : Form
    {

        private MavLinkUdpClient mMavLink;

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
        }

        private void RollTrackbar_Scroll(object sender, EventArgs e)
        {
            mMavLink.MsgAttitude.roll = RollTrackbar.Value / 100f;
            UpdateAttitude();
        }

        private void PitchTrackbar_Scroll(object sender, EventArgs e)
        {
            mMavLink.MsgAttitude.pitch = PitchTrackbar.Value / 100f;
            UpdateAttitude();
        }

        private void YawTrackbar_Scroll(object sender, EventArgs e)
        {
            mMavLink.MsgAttitude.yaw = YawTrackbar.Value / 100f;
            UpdateAttitude();
        }

        private void UpdateAttitude()
        {
            mMavLink.SendMessage(mMavLink.MsgAttitude);
        }
    }
}
