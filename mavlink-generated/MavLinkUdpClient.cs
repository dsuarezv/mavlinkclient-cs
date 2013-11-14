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

        public Msg_heartbeat MsgHeartBeat;
        public Msg_sys_status MsgSysStatus;
        public Msg_local_position_ned MsgLocalPosition;
        public Msg_attitude MsgAttitude;

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
            InitializeStatusMessages();
            InitializeUdpListener(UdpListeningPort);
            InitializeUdpSender(TargetIpAddress, UdpTargetPort);
        }

        private void InitializeStatusMessages()
        { 
            MsgHeartBeat = new Msg_heartbeat
            {
                type = (byte)MAV_TYPE.MAV_TYPE_QUADROTOR,
                autopilot = (byte)MAV_AUTOPILOT.MAV_AUTOPILOT_ARDUPILOTMEGA,
                base_mode = (byte)MAV_MODE_FLAG.MAV_MODE_FLAG_AUTO_ENABLED,
                custom_mode = 0, 
                system_status = (byte)MAV_STATE.MAV_STATE_ACTIVE,
                mavlink_version = (byte)3,
            };

            MsgSysStatus = new Msg_sys_status 
            {
                onboard_control_sensors_present = 0,
                onboard_control_sensors_enabled = 0,
                onboard_control_sensors_health = 0,
                load = 500,
                voltage_battery = 11000,
                current_battery = -1,
                battery_remaining = -1,
                drop_rate_comm = 0,
                errors_comm = 0,
                errors_count1 = 0,
                errors_count2 = 0,
                errors_count3 = 0,
                errors_count4 = 0,
            };

            MsgLocalPosition = new Msg_local_position_ned
            {
                time_boot_ms = 0,
                x = 0,
                y = 0, 
                z = 0, 
                vx = 0, 
                vy = 0, 
                vz = 0,
            };

            MsgAttitude = new Msg_attitude
            {
                time_boot_ms = 0,
                roll = 0,
                pitch = 0,
                yaw = 0,
                rollspeed = 0,
                pitchspeed = 0,
                yawspeed = 0,
            };
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
                SendMessage(MsgHeartBeat);
                SendMessage(MsgSysStatus);
                SendMessage(MsgLocalPosition);
                SendMessage(MsgAttitude);

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
