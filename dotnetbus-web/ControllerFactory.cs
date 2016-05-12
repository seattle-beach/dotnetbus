using dotnetbus_web.Controllers;
using dotnetbus_web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

namespace dotnetbus_web
{
    public class ControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                return null;
            }

            if (controllerType == typeof(StopController))
            {
                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri("http://weatherbus-prime-dev.cfapps.io/");
                return new StopController(new StopService(httpClient));
            }

            return base.GetControllerInstance(requestContext, controllerType);
        }
    }
}