using dotnetbus_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dotnetbus_web.ViewModels
{
    public class StopViewModel
    {
        public double Latitude;
        public double Longitude;
        public string stopId;
        public List<StopResponseDeparture> departures;
    }
}