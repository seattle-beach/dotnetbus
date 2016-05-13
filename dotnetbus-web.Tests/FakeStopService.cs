using dotnetbus_web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotnetbus_web.Models;

namespace dotnetbus_web.Tests
{
    class FakeStopService : StopService
    {
        public Dictionary<String, StopResponse> StopResponses { get; private set; }

        public Exception Throws { get; set; }

        public FakeStopService() : base(null)
        {
            StopResponses = new Dictionary<string, StopResponse>();
        }

        public override async Task<StopResponse> DeparturesForStopAsync(string stopId)
        {
            if (Throws != null)
            {
                throw Throws;
            }

            if (!StopResponses.ContainsKey(stopId))
            {
                throw new Exception(String.Format("No response configured for stop ID {0}", stopId));
            }

            return StopResponses[stopId];
        }
    }
}
