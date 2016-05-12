using dotnetbus_web.Controllers;
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
                return new StopController(new HttpClient());
            }

            return base.GetControllerInstance(requestContext, controllerType);
        }
    }
}