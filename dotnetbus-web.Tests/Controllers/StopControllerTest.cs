﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using dotnetbus_web.Controllers;
using System.Net;
using System.Web.Mvc;
using dotnetbus_web.Models;
using System.Collections.Generic;
using dotnetbus_web.Services;

namespace dotnetbus_web.Tests.Controllers
{
    [TestClass]
    public class StopControllerTest
    {
        [TestMethod]
        public void Index_RetrievesStop()
        {
            var svc = new FakeStopService();
            var stopResponse = new StopResponse { data = new StopResponseData() };
            svc.StopResponses["1_619"] = stopResponse;
            var subject = new StopController(svc);

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
            var svc = new FakeStopService();
            svc.Throws = new NoSuchStopException();
            var subject = new StopController(svc);

            var result = (ViewResult)subject.Index("1_619");
            Assert.AreEqual("nostop", result.ViewName);
        }
    }
}
