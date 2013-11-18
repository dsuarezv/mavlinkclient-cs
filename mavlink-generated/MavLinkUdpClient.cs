using System;
using System.Net;
using System.Threading;
using System.Net.Sockets;
using System.Collections.Concurrent;

namespace MavLink
{
    public class MavLinkUdpClient
    {
        public int UdpListeningPort = 14551;
        public int UdpTargetPort = 14550;
        public int MavlinkSystemId = 200;
        public int MavlinkComponentId = 1;
        public IPAddress TargetIpAddress = new IPAddress(new byte[] { 127, 0, 0, 1 });
        public int HeartBeatUpdateRateMs = 1000;
        public UavState UavState = new UavState();

        public event PacketReceivedEventHandler OnPacketReceived;
        
        private ConcurrentQueue<byte[]> mReceiveQueue = new ConcurrentQueue<byte[]>();
        private ConcurrentQueue<MavlinkMessage> mSendQueue = new ConcurrentQueue<MavlinkMessage>();
        private AutoResetEvent mReceiveSignal = new AutoResetEvent(true);
        private AutoResetEvent mSendSignal = new AutoResetEvent(true);
        private Mavlink mMavLink = new Mavlink();
        private UdpClient mUdpClient;


        public void Initialize()
        {
            InitializeMavLink();
            InitializeUdpListener(UdpListeningPort);
            InitializeUdpSender(TargetIpAddress, UdpTargetPort);
        }

        private void InitializeMavLink()
        {
            mMavLink.PacketReceived += HandlePacketReceived;
        }

        private void InitializeUdpListener(int port)
        {
            // Create UDP listening socket on port
            IPEndPoint ep = new IPEndPoint(IPAddress.Any, port);
            mUdpClient = new UdpClient(ep);

            mUdpClient.BeginReceive(
                new AsyncCallback(ReceiveCallback), ep);

            ThreadPool.QueueUserWorkItem(
                new WaitCallback(ProcessReceiveQueue), null);
        }

        private void InitializeUdpSender(IPAddress targetIp, int targetPort)
        {
            ThreadPool.QueueUserWorkItem(
               new WaitCallback(ProcessSendQueue), new IPEndPoint(targetIp, targetPort));
        }


        // __ Receive _________________________________________________________


        private void ReceiveCallback(IAsyncResult ar)
        {
            IPEndPoint ep = ar.AsyncState as IPEndPoint;
            mReceiveQueue.Enqueue(mUdpClient.EndReceive(ar, ref ep));
            mUdpClient.BeginReceive(new AsyncCallback(ReceiveCallback), ar);
            
            // Signal processReceive thread
            mReceiveSignal.Set();
        }

        private void ProcessReceiveQueue(object state)
        {
            while (true)
            {
                byte[] buffer;

                if (mReceiveQueue.TryDequeue(out buffer))
                {
                    mMavLink.ParseBytes(buffer);
                }
                else
                {
                    // Empty queue, sleep until signalled
                    mReceiveSignal.WaitOne();
                }
            }
        }


        // __ Send ____________________________________________________________


        private void ProcessSendQueue(object state)
        {
            while (true)
            {
                MavlinkMessage msg;

                if (mSendQueue.TryDequeue(out msg))
                {
                    SendMavlinkMessage(state as IPEndPoint, msg);
                }
                else
                {
                    // Empty queue, sleep until signalled
                    mSendSignal.WaitOne();
                }
            }
        }

        private void SendMavlinkMessage(IPEndPoint ep, MavlinkMessage msg)
        {
            MavlinkPacket p = GetPacketFromMsg(msg);
            byte[] buffer = mMavLink.Send(p);

            mUdpClient.Send(buffer, buffer.Length, ep);
        }

        private MavlinkPacket GetPacketFromMsg(MavlinkMessage msg)
        {
            MavlinkPacket p = new MavlinkPacket();
            p.Message = msg;
            p.TimeStamp = DateTime.Now;
            p.SequenceNumber = 1;
            p.SystemId = MavlinkSystemId;
            p.ComponentId = MavlinkComponentId;
            
            return p;
        }


        // __ Heartbeat _______________________________________________________


        public void BeginHeartBeatLoop()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(HeartBeatLoop), null);
        }

        private void HeartBeatLoop(object state)
        {
            while (true)
            {
                foreach (MavlinkMessage m in UavState.GetHeartBeatObjects())
                {
                    SendMessage(m);
                } 

                Thread.Sleep(HeartBeatUpdateRateMs);
            }
        }


        // __ MavLink events __________________________________________________


        private void HandlePacketReceived(object sender, MavlinkPacket e)
        {
            if (OnPacketReceived != null) OnPacketReceived(sender, e);
        }


        // __ API _____________________________________________________________


        public void SendMessage(MavlinkMessage msg)
        {
            mSendQueue.Enqueue(msg);

            // Signal send thread
            mSendSignal.Set();
        }
    }
}
