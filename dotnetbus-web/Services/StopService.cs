using dotnetbus_web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace dotnetbus_web.Services
{
    public class StopService
    {
        private HttpClient _httpClient;

        public StopService(HttpClient c)
        {
            _httpClient = c;
        }

        public virtual async Task<StopResponse> DeparturesForStopAsync(string stopId)
        {
            string path = String.Format("/api/v1/stops/{0}", stopId);
            var task = _httpClient.GetAsync(path);
            await task.ConfigureAwait(false);
            return await task.ContinueWith((taskWithResponse) =>
                {
                    // TODO: check status code etc
                    var bt = taskWithResponse.Result.Content.ReadAsStringAsync();
                    bt.Wait();
                    Console.WriteLine("Stop response: {0}", bt.Result);
                    return JsonConvert.DeserializeObject<StopResponse>(bt.Result);
                });
        }
    }
}