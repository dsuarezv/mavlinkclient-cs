using System;
using System.Net;
using System.Threading;
using System.Net.Sockets;
using System.Collections.Generic;

using MavLink;

namespace mavlinkudp
{
    class MainClass
    {
        private static Queue<byte[]> mBufferQueue = new Queue<byte[]>();
        private static Mavlink mMavLink = new Mavlink();

        public static void Main(string[] args)
        {
            InitializeMavLink();
            InitializeUdp(14451);

            Console.WriteLine("Waiting for UDP...");
            Console.ReadLine();
        }

        private static void InitializeMavLink()
        {
            mMavLink.PacketReceived += HandlePacketReceived;
        }

        static void InitializeUdp(int port)
        {
            UdpState s = new UdpState();
            s.EndPoint = new IPEndPoint(IPAddress.Any, port);
            s.UdpClient = new UdpClient(s.EndPoint);
            s.UdpClient.BeginReceive(new AsyncCallback(ReceiveCallback), s);
        }

        private static void ReceiveCallback(IAsyncResult ar)
        {
            UdpState s = ar.AsyncState as UdpState;

            byte[] buffer = s.UdpClient.EndReceive(ar, ref s.EndPoint);

            lock (mBufferQueue)
            {
                mBufferQueue.Enqueue(buffer);
            }

            s.UdpClient.BeginReceive(new AsyncCallback(ReceiveCallback), ar);
            SignalQueue();
        }

        private static void SignalQueue()
        {
            lock (mBufferQueue)
            {
                while (mBufferQueue.Count > 0)
                {
                    byte[] buffer = mBufferQueue.Dequeue();
                    ThreadPool.QueueUserWorkItem(new WaitCallback(AsyncProcessBuffer), buffer);
                }
            }
        }

        private static void AsyncProcessBuffer(object state)
        {
            if (state == null) return;

            mMavLink.ParseBytes((byte[])state);
        }


        static void HandlePacketReceived (object sender, MavlinkPacket e)
        {
            Console.WriteLine("Received packet: {0}", e);
        }
    }

    class UdpState
    {
        public IPEndPoint EndPoint;
        public UdpClient UdpClient;
    }
}
