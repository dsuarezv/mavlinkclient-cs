using System;
using System.Net;
using System.Windows.Forms;

using MavLinkNet;

namespace GpsFeeder
{
    public partial class Form1 : Form
    {
        private MavLinkUdpTransport mMavlinkClient;

        public Form1()
        {
            InitializeComponent();
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            IPAddress targetIp;

            if (!IPAddress.TryParse(UdpConnectIpTextbox.Text, out targetIp))
            {
                Error("'{0}' is not a valid IP address.", UdpConnectIpTextbox.Text);
                return;
            }

            int targetPort;

            if (!int.TryParse(UdpConnectPortTextbox.Text, out targetPort))
            {
                Error("'{0}' is not a valid target port.", UdpConnectPortTextbox.Text);
                return;
            }

            int listenPort;

            if (!int.TryParse(UdpListenPortTextbox.Text, out listenPort))
            {
                Error("'{0}' is not a valid listening port.", UdpListenPortTextbox.Text);
                return;
            }

            InitMavlink(targetIp, targetPort, listenPort);
        }

        private void InitMavlink(IPAddress targetIp, int targetPort, int listenPort)
        {
            if (mMavlinkClient != null)
            {
                mMavlinkClient.Dispose();
            }

            mMavlinkClient = new MavLinkUdpTransport()
            {
                TargetIpAddress = targetIp,
                UdpTargetPort = targetPort, 
                UdpListeningPort = listenPort
            };

            mMavlinkClient.OnPacketReceived += mMavlinkClient_OnPacketReceived;
        }

        private static void Error(string msg, params object[] args)
        {
            MessageBox.Show(string.Format(msg, args), "Error");
        }

        private void UbloxSvInfoPacketTextbox_TextChanged(object sender, EventArgs e)
        {
            // Parse the new text, generate the packets. The heartbeat process will send them.
        }

        void mMavlinkClient_OnPacketReceived(object sender, MavLinkPacket packet)
        {

        }

    }
}
