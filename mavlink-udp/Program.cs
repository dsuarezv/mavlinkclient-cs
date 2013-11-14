using System;
using System.Net;
using System.Threading;
using System.Net.Sockets;
using System.Collections.Concurrent;

using MavLink;

namespace mavlinkudp
{
    class MainClass
    { 
        private const int UdpListeningPort = 14551;
        private const int UdpTargetPort = 14550;
        private const int MavlinkSystemId = 200;
        private const int MavlinkComponentId = 1;


        private static ConcurrentQueue<byte[]> mReceiveQueue = new ConcurrentQueue<byte[]>();
        private static ConcurrentQueue<MavlinkMessage> mSendQueue = new ConcurrentQueue<MavlinkMessage>();
        private static AutoResetEvent mReceiveSignal = new AutoResetEvent(true);
        private static AutoResetEvent mSendSignal = new AutoResetEvent(true);
        private static Mavlink mMavLink = new Mavlink();
        private static UdpClient mUdpClient;

        private static Msg_heartbeat mMsgHeartBeat;
        private static Msg_sys_status mMsgSysStatus;
        private static Msg_local_position_ned mMsgLocalPosition;
        private static Msg_attitude mMsgAttitude;

       
        public static void Main(string[] args)
        {
            if (!CheckArguments(args)) return;

            InitializeMavLink();
            InitializeStatusMessages();
            InitializeUdpListener(UdpListeningPort);
            InitializeUdpSender(new IPAddress(new byte[] { 127, 0, 0, 1 }), UdpTargetPort);

            BeginHeartBeatLoop();

            Console.WriteLine("Waiting for UDP...");
            Console.ReadLine();
        }

        private static bool CheckArguments(string[] args)
        {
            return true;
        }

        private static void InitializeStatusMessages()
        { 
            mMsgHeartBeat = new Msg_heartbeat
            {
                type = (byte)MAV_TYPE.MAV_TYPE_QUADROTOR,
                autopilot = (byte)MAV_AUTOPILOT.MAV_AUTOPILOT_ARDUPILOTMEGA,
                base_mode = (byte)MAV_MODE_FLAG.MAV_MODE_FLAG_AUTO_ENABLED,
                custom_mode = 0, 
                system_status = (byte)MAV_STATE.MAV_STATE_ACTIVE,
                mavlink_version = (byte)3,
            };

            mMsgSysStatus = new Msg_sys_status 
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

            mMsgLocalPosition = new Msg_local_position_ned
            {
                time_boot_ms = 0,
                x = 0,
                y = 0, 
                z = 0, 
                vx = 0, 
                vy = 0, 
                vz = 0,
            };

            mMsgAttitude = new Msg_attitude
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

        private static void InitializeMavLink()
        {
            mMavLink.PacketReceived += HandlePacketReceived;
        }

        private static void InitializeUdpListener(int port)
        {
            // Create UDP listening socket on port
            IPEndPoint ep = new IPEndPoint(IPAddress.Any, port);
            mUdpClient = new UdpClient(ep);

            mUdpClient.BeginReceive(
                new AsyncCallback(ReceiveCallback), ep);

            ThreadPool.QueueUserWorkItem(
                new WaitCallback(ProcessReceiveQueue), null);
        }

        private static void InitializeUdpSender(IPAddress targetIp, int targetPort)
        {
            ThreadPool.QueueUserWorkItem(
               new WaitCallback(ProcessSendQueue), new IPEndPoint(targetIp, targetPort));
        }


        // __ Receive _________________________________________________________


        private static void ReceiveCallback(IAsyncResult ar)
        {
            IPEndPoint ep = ar.AsyncState as IPEndPoint;
            mReceiveQueue.Enqueue(mUdpClient.EndReceive(ar, ref ep));
            mUdpClient.BeginReceive(new AsyncCallback(ReceiveCallback), ar);
            
            // Signal processReceive thread
            mReceiveSignal.Set();
        }

        private static void ProcessReceiveQueue(object state)
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


        private static void ProcessSendQueue(object state)
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

        private static void SendMavlinkMessage(IPEndPoint ep, MavlinkMessage msg)
        {
            MavlinkPacket p = GetPacketFromMsg(msg);
            byte[] buffer = mMavLink.Send(p);

            mUdpClient.Send(buffer, buffer.Length, ep);
        }

        private static MavlinkPacket GetPacketFromMsg(MavlinkMessage msg)
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


        private static void BeginHeartBeatLoop()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(HeartBeatLoop), null);
        }

        private static void HeartBeatLoop(object state)
        {
            while (true)
            {
                SendMessage(mMsgHeartBeat);
                SendMessage(mMsgSysStatus);
                SendMessage(mMsgLocalPosition);
                SendMessage(mMsgAttitude);

                Thread.Sleep(1000);
            }
        }


        // __ MavLink events __________________________________________________


        private static void HandlePacketReceived(object sender, MavlinkPacket e)
        {
            Console.WriteLine("Received packet: {0}", e);
        }


        // __ API _____________________________________________________________


        public static void SendMessage(MavlinkMessage msg)
        {
            mSendQueue.Enqueue(msg);

            // Signal send thread
            mSendSignal.Set();
        }


    }

    class UdpState
    {
        public IPEndPoint EndPoint;
        public UdpClient UdpClient;
    }
}
