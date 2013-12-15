using System;
using System.Collections.Generic;

namespace MavLink
{
    /// <summary>
    /// Holds message objects representing state. Meant for status messages, not communication objects
    /// </summary>
    public class UavState
    {
        private Dictionary<string, MavlinkMessage> mState = new Dictionary<string, MavlinkMessage>();
        private List<MavlinkMessage> mHeartBeatMessages = new List<MavlinkMessage>();

        public UavState()
        {
            mState.Add("HEARTBEAT", new Msg_heartbeat
            {
                type = (byte)MAV_TYPE.MAV_TYPE_QUADROTOR,
                autopilot = (byte)MAV_AUTOPILOT.MAV_AUTOPILOT_ARDUPILOTMEGA,
                base_mode = (byte)MAV_MODE_FLAG.MAV_MODE_FLAG_AUTO_ENABLED,
                custom_mode = 0,
                system_status = (byte)MAV_STATE.MAV_STATE_ACTIVE,
                mavlink_version = (byte)3,
            });

            mState.Add("SYS_STATUS", new Msg_sys_status
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
            });

            mState.Add("LOCAL_POSITION_NED", new Msg_local_position_ned
            {
                time_boot_ms = 0,
                x = 0,
                y = 0,
                z = 0,
                vx = 0,
                vy = 0,
                vz = 0,
            });

            mState.Add("ATTITUDE", new Msg_attitude
            {
                time_boot_ms = 0,
                roll = 0,
                pitch = 0,
                yaw = 0,
                rollspeed = 0,
                pitchspeed = 0,
                yawspeed = 0,
            });


        }

        public MavlinkMessage Get(string mavlinkObjectName)
        {
            return mState[mavlinkObjectName];
        }

        public List<MavlinkMessage> GetHeartBeatObjects()
        {
            if (mHeartBeatMessages.Count == 0)
            {
                mHeartBeatMessages.Add(Get("HEARTBEAT"));
                mHeartBeatMessages.Add(Get("SYS_STATUS"));
                mHeartBeatMessages.Add(Get("LOCAL_POSITION_NED"));
                mHeartBeatMessages.Add(Get("ATTITUDE"));
            }

            return mHeartBeatMessages;
        }

    }
}
