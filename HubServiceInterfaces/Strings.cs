using System;
using System.Collections.Generic;
using System.Text;

namespace HubServiceInterfaces
{
    public static class Strings
    {
        public static string HubClockUrl => "http://localhost:5000/hubs/clock";
        public static string HubSpotUrl => "http://localhost:5000/hubs/spot";

        public static class ClockEvents
        {
            public static string TimeSent => nameof(IClock.ShowTime);
        }

        public static class SpotEvents
        {
            public static string SpotSent => nameof(ISpot.OnlySpot);
            public static string SetNameSent => nameof(ISpot.SetName);
        }
    }
}