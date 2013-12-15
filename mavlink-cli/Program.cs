using System;
using System.Net;
using System.Threading;
using System.Net.Sockets;
using System.Reflection;
using System.Collections.Generic;

using MavLinkNet;

namespace mavlinkcli
{
    class MainClass
    {
        private static int mPacketCount = 0;

        public static void Main(string[] args)
        {
            if (!CheckArguments(args)) return;

            switch (args[0].ToLower())
            {
                case "udp":
                    ProcessUdpStream();
                    break;
                case "log":
                    ProcessLogFile(args[1]);
                    break;
                default:
                    PrintUsage();
                    break;
            }
        }

        private static void ProcessLogFile(string logFileName)
        {
            MavLinkLogFileTransport mav = new MavLinkLogFileTransport(logFileName);

            mav.OnPacketReceived += OnMavLinkPacketReceived;
            mav.Initialize();
        }

        private static void ProcessUdpStream()
        {
            MavLinkUdpTransport mluc = new MavLinkUdpTransport
            {
                TargetIpAddress = new IPAddress(new byte[] { 127, 0, 0, 1 }),
                MavlinkSystemId = 187
            };

            mluc.OnPacketReceived += OnMavLinkPacketReceived;
            mluc.Initialize();
            mluc.BeginHeartBeatLoop();

            Console.WriteLine("Waiting for UDP...");
            Console.ReadLine();
        }

        
        // __ Packet processing _______________________________________________


        static void OnMavLinkPacketReceived(object sender, MavLinkPacket e)
        {
            PrintPacket(e);
            //PrintMessage(e.Message);
            PrintPacketCount();
        }

        private static void PrintPacketCount()
        {
            const int PrintEveryNumPackets = 100;

            if ((mPacketCount++ % PrintEveryNumPackets) == 1)
            {
                Console.Error.Write("{0} packets processed.\r", mPacketCount);
            }
        }

        private static void PrintPacket(MavLinkPacket p)
        {
            WL("FROM {0} _______________________________________________________________", p.SystemId);

            PrintMessage(p.Message);
        }

        private static void PrintMessage(UasMessage m)
        {
            if (m is UasCommandLong)
            {
                PrintCommandLong(m as UasCommandLong);
            }
            else
            {
                PrintStandardMessage(m);
            }

            WL("");
        }

       

        private static void PrintStandardMessage(UasMessage m)
        {
            UasMessageMetadata md = m.GetMetadata();

            WL("{0}: {1}", m, md.Description);

            foreach (UasFieldMetadata f in md.Fields)
            {
                WL("  {0}: {1}  ({2})", f.Name, GetFieldValue(f.Name, m), f.Description);
            }
            
        }

        private static void PrintCommandLong(UasCommandLong m)
        {
            UasMessageMetadata md = m.GetMetadata();

            WL("{0}: {1}", m, md.Description);

            foreach (UasFieldMetadata f in md.Fields)
            {
                if (f.Name.StartsWith("Param") && (m is UasCommandLong))
                {
                    WL("    {0}: {1} ({2})", 
                        f.Name, GetFieldValue(f.Name, m),
                        GetCommandParamDescription((int)m.Command, f.Name.Substring(5)));
                    continue;
                }

                WL("  {0}: {1}  ({2})", f.Name, GetFieldValue(f.Name, m), f.Description);
            }
        }

        private static string GetCommandParamDescription(int command, string paramIndexString)
        {
            int paramIndex; 
            if (!Int32.TryParse(paramIndexString, out paramIndex)) return "";

            foreach (UasEnumEntryMetadata entry in UasSummary.GetEnumMetadata("MavCmd").Entries)
            {
                if (command != entry.Value) continue;

                return entry.Params[paramIndex - 1];
            }

            return "";
        }

        private static object GetFieldValue(string fieldName, UasMessage m)
        {
            PropertyInfo p = m.GetType().GetProperty(fieldName);

            if (p == null)
            {
                WL("MISSING FIELD: {0} on {1}", fieldName, m.GetType());
                return "";
            }

            object result = p.GetValue(m, null);

            if (result is char[]) return new String((char[])result);

            return result;
        }

        private static void WL(string msg, params object[] args)
        {
            Console.WriteLine(msg, args);
        }

        private static bool CheckArguments(string[] args)
        {
            if (args.Length < 1)
            {
                PrintUsage();
                return false;
            }

            return true;
        }

        private static void PrintUsage()
        {
            WL("Usage: mavlink-cli <command> [args]");
            WL("Available commands:");
            WL("  udp : starts a UDP conversation with a GCS on localhost.");
            WL("  log <logfile> : parses logfile and prints messages.");
        }
    }
}
