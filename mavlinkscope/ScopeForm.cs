using System;
using System.Collections.Generic;
using System.Windows.Forms;

using MavLinkNet;

namespace mavlinkscope
{
    public partial class ScopeForm : Form
    {
        private MavLinkGenericTransport mMavlink;

        private List<MavLinkPacket> mPackets = new List<MavLinkPacket>();

        public ScopeForm()
        {
            InitializeComponent();
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            ParseFile(LogFileTextBox.Text);
        }


        // __ Parsing _________________________________________________________


        private void ParseFile(string fileName)
        {
            try
            {
                mMavlink = new MavLinkLogFileTransport(fileName);
                mMavlink.OnPacketReceived += ParserOnPacketReceived;
                mMavlink.Initialize();

                MessageBox.Show(string.Format("Parsed {0} packets", mPackets.Count));
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void ShowError(Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ParserOnPacketReceived(object sender, MavLinkPacket packet)
        {
            mPackets.Add(packet);
        }


        // __ Scope ___________________________________________________________



    }
}
