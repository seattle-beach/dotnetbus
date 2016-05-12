using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using dotnetbus_web.Controllers;
using System.Net;
using System.Web.Mvc;
using dotnetbus_web.Models;

namespace dotnetbus_web.Tests.Controllers
{
    [TestClass]
    public class StopControllerTest
    {
        [TestMethod]
        public void Index_RetrievesStop()
        {
            var handler = new FakeHttpResponseHandler();
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            string json = @"{  
               ""data"":{  
                  ""latitude"":47.599827,
                  ""longitude"":-122.328972,
                  ""stopId"":""1_619"",
                  ""departures"":[  
                     {  
                        ""temp"":63.0,
                        ""climacon_url"":""http://weatherbus-weather-dev.cfapps.io/assets/Cloud-Sun.svg"",
                        ""climacon"":""PARTLY_CLOUDY"",
                        ""routeShortName"":""40"",
                        ""headsign"":""NORTHGATE FREMONT"",
                        ""predictedTime"":0,
                        ""scheduledTime"":1463078707000
                     }
                  ]
               }
            }";
            response.Content = new FakeHttpContent(json);
            var url = new Uri("http://weatherbus-prime-dev.cfapps.io/api/v1/stops/1_619");
            handler.responses[url] = response;

            var client = new HttpClient(handler);
            var subject = new StopController(client);

            var result = (ViewResult)subject.Index("1_619");
            var viewModel = (StopResponseData)result.Model;
            Assert.IsTrue(viewModel.Latitude > 47.599826 && viewModel.Latitude < 47.599828);
            Assert.IsTrue(viewModel.Longitude > -122.328973 && viewModel.Longitude < 122.328971);
            Assert.AreEqual(viewModel.departures.Count, 1);
            var d = viewModel.departures[0];
            Assert.AreEqual(d.temp, 63.0);
            Assert.AreEqual(d.climacon_url, "http://weatherbus-weather-dev.cfapps.io/assets/Cloud-Sun.svg");
            Assert.AreEqual(d.climacon, "PARTLY_CLOUDY");
            Assert.AreEqual(d.routeShortName, "40");
            Assert.AreEqual(d.headsign, "NORTHGATE FREMONT");
            Assert.AreEqual(d.predictedTime, 0);
            Assert.AreEqual(d.scheduledTime, 1463078707000);
        }
    }
}
