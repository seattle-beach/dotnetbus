using dotnetbus_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnetbus_web.Services
{
    public interface IStopService
    {
        Task<StopResponse> DeparturesForStopAsync(string stopId);
    }
}
