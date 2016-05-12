using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using dotnetbus_web.Models;
using dotnetbus_web.Services;

namespace dotnetbus_web.Controllers
{
    public class StopController : Controller
    {
        private StopService _stopService;

        public StopController(StopService c)
        {
            _stopService = c;
        }

        // GET: Stop
        public ActionResult Index(string stopId)
        {
            if (String.IsNullOrEmpty(stopId))
            {
                return new RedirectResult("/");
            }

            var task = _stopService.DeparturesForStopAsync(stopId);
            task.Wait();
            return View(task.Result.data);
        }
    }
}