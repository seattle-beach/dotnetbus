using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc3;
using dotnetbus_web.Services;
using System.Net.Http;
using System;

namespace dotnetbus_web
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            var primeHttpClient = new HttpClient();
            primeHttpClient.BaseAddress = new Uri("http://weatherbus-prime-dev.cfapps.io/");
            container.RegisterInstance<HttpClient>("weatherbus-prime", primeHttpClient);
            container.RegisterType<StopService>();
            return container;
        }
    }
}