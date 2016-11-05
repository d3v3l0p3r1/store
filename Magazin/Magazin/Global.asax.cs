using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Data.DAL;
using Data.Entities;
using Magazin.App_Start;
using Magazin.Helpers;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;

namespace Magazin
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var container = new Container();
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            Bindings.Bind(container);

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));            

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            MapperConfig.Init();
            UpdateConfiguration.Init(container.GetInstance<IUpdateConfigurationManager>());            
            ValueProviderFactories.Factories.Add(new JsonValueProviderFactory());

            ModelBinders.Binders.Add(typeof(Bascet), new BascetModelBinder());
        }
    }
}
