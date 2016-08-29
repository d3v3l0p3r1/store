using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Data.DAL;
using Magazin.App_Start;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;

namespace Magazin
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var container = new Container();

            using (var context = new DataContext())
            {
                context.Database.Initialize(true);
            }
            
            Bindings.Bind(container);
            
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));            

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
