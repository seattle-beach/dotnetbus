using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dotnetbus_web.Models
{
    public class StopResponse
    {
        public StopResponseData data;
    }

    public class StopResponseData
    {
        public double latitude;
        public double longitude;
        public string stopId;
        public List<StopResponseDeparture> departures;
    }

    public class StopResponseDeparture
    {
        public float temp;
        public string climacon_url;
        public string climacon;
        public string routeShortName;
        public string headsign;
        public Int64 predictedTime;
        public Int64 scheduledTime;
    }
}
 