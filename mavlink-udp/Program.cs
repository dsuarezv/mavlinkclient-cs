using System;
using System.Net;
using System.Threading;
using System.Net.Sockets;
using System.Reflection;
using System.Collections.Generic;

using MavLinkNet;

namespace mavlinkudp
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            if (!CheckArguments(args)) return;

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

        static void OnMavLinkPacketReceived(object sender, MavLinkPacket e)
        {
            PrintMessage(e.Message);
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
                    string paramIndex = f.Name.Substring(5);
                    WL("    {0}: {1} ({2})", f.Name, GetFieldValue(f.Name, m), GetCommandParamDescription((int)m.Command, paramIndex));
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

            return p.GetValue(m, null);
        }

        private static void WL(string msg, params object[] args)
        {
            Console.WriteLine(msg, args);
        }

        private static bool CheckArguments(string[] args)
        {
            return true;
        }
    }
}
