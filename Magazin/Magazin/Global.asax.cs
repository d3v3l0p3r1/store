using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Base.Services;
using SimpleInjector;

namespace Magazin
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private Container _container = Bindings.Bindings.Container;

        protected void Application_Start()
        {

            var locator = _container.GetInstance<IServiceLocator>();

            DependencyResolver.SetResolver(locator.GetService<IDependencyResolver>());

       
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
