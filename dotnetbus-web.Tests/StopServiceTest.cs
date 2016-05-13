using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using dotnetbus_web.Services;
using System.Net;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using System.Threading;

namespace dotnetbus_web.Tests
{
    [TestClass]
    public class StopServiceTest
    {
        private Mock<DelegatingHandler> mockHandlerWithResponse(Uri url, HttpStatusCode status, string body)
        {
            var response = new HttpResponseMessage(status);
            response.Content = new FakeHttpContent(body);
            var handler = new Mock<DelegatingHandler>();
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Returns(() => Task.FromResult(response))
                .Callback<HttpRequestMessage, CancellationToken>((req, cancel) =>
                {
                    Assert.AreEqual(url, req.RequestUri);
                });

            return handler;
        }

        [TestMethod]
        public void TestDeparturesForStopAsync_success()
        {
            string json = @"{  
               ""data"":{  
                  ""latitude"":47.599827,
                  ""longitude"":-122.328972,
                  ""stopId"":""1_619"",
                  ""departures"":[  
                     {  
                        ""temp"":63.0,
                        ""climacon_url"":""http://example.com/assets/Cloud-Sun.svg"",
                        ""climacon"":""PARTLY_CLOUDY"",
                        ""routeShortName"":""40"",
                        ""headsign"":""NORTHGATE FREMONT"",
                        ""predictedTime"":0,
                        ""scheduledTime"":1463078707000
                     }
                  ]
               }
            }";

            var handler = mockHandlerWithResponse(new Uri("http://example.com/api/v1/stops/1_619"),
                HttpStatusCode.OK, json);
            var client = new HttpClient(handler.Object);
            client.BaseAddress = new Uri("http://example.com/");
            var subject = new StopService(client);

            var task = subject.DeparturesForStopAsync("1_619");
            task.Wait();
            var data = task.Result.data;
            Assert.IsTrue(data.latitude > 47.599826 && data.latitude < 47.599828);
            Assert.IsTrue(data.longitude > -122.328973 && data.longitude < 122.328971);
            Assert.AreEqual(data.departures.Count, 1);
            var d = data.departures[0];
            Assert.AreEqual(d.temp, 63.0);
            Assert.AreEqual(d.climacon_url, "http://example.com/assets/Cloud-Sun.svg");
            Assert.AreEqual(d.climacon, "PARTLY_CLOUDY");
            Assert.AreEqual(d.routeShortName, "40");
            Assert.AreEqual(d.headsign, "NORTHGATE FREMONT");
            Assert.AreEqual(d.predictedTime, 0);
            Assert.AreEqual(d.scheduledTime, 1463078707000);
            //Assert.AreEqual(new Uri("http://example.com/api/v1/stops/1_619"), request.RequestUri);
        }

        [TestMethod]
        public async Task TestDeparturesForStopAsync_non_200()
        {
            var handler = mockHandlerWithResponse(new Uri("http://example.com/api/v1/stops/1_619"),
                HttpStatusCode.InternalServerError, "{\"nope?\": \"nope.\"}");

            var client = new HttpClient(handler.Object);
            client.BaseAddress = new Uri("http://example.com/");
            var subject = new StopService(client);
            bool thrown = false;

            try
            {
                await subject.DeparturesForStopAsync("1_619");
            }
            catch (NoSuchStopException)
            {
                thrown = true;
            }

            Assert.IsTrue(thrown);
        }
    }
}
