using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using dotnetbus_web.Controllers;
using System.Net;
using System.Web.Mvc;
using dotnetbus_web.Models;
using System.Collections.Generic;
using dotnetbus_web.Services;
using Moq;
using System.Threading.Tasks;

namespace dotnetbus_web.Tests.Controllers
{
    [TestClass]
    public class StopControllerTest
    {
        [TestMethod]
        public void Index_RetrievesStop()
        {
            var svc = new Mock<IStopService>();
            var stopResponse = new StopResponse { data = new StopResponseData() };
            svc.Setup(x => x.DeparturesForStopAsync("1_619"))
                .ReturnsAsync(stopResponse);
            var subject = new StopController(svc.Object);

            var result = (ViewResult)subject.Index("1_619");
            var viewModel = (StopResponseData)result.Model;
            Assert.AreSame(stopResponse.data, viewModel);
        }

        [TestMethod]
        public void Index_EmptyStopId_Redirects()
        {
            var subject = new StopController(null);
            var result = (RedirectResult)subject.Index("");
            Assert.AreEqual("/", result.Url);
        }

        [TestMethod]
        public void Index_NoSuchStop()
        {
            var svc = new Mock<IStopService>();
            svc.Setup(x => x.DeparturesForStopAsync("1_619"))
                .ThrowsAsync(new NoSuchStopException());
            var subject = new StopController(svc.Object);

            var result = (ViewResult)subject.Index("1_619");
            Assert.AreEqual("nostop", result.ViewName);
        }
    }
}
