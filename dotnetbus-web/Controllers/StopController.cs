using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using dotnetbus_web.ViewModels;
using Newtonsoft.Json;
using dotnetbus_web.Models;

namespace dotnetbus_web.Controllers
{
    public class StopController : Controller
    {
        private HttpClient _httpClient;

        public StopController(HttpClient c)
        {
            _httpClient = c;
        }

        // GET: Stop
        public ActionResult Index(string stopId)
        {
            _httpClient.BaseAddress = new Uri("http://weatherbus-prime-dev.cfapps.io/");
            string path = String.Format("/api/v1/stops/{0}", stopId);
            StopResponse stopResponse = null;

            var task = _httpClient.GetAsync(path)
                .ContinueWith((taskWithResponse) =>
                {
                    // TODO: check status code etc
                    var bt = taskWithResponse.Result.Content.ReadAsStringAsync();
                    bt.Wait();
                    stopResponse = JsonConvert.DeserializeObject<StopResponse>(bt.Result);
                });
            task.Wait();
            // TODO: This view model is silly. Just return the response.
            return View(new StopViewModel {
                Latitude = stopResponse.data.latitude,
                Longitude = stopResponse.data.longitude,
                departures = stopResponse.data.departures
            });
        }
    }
}