using System.Web.Mvc;
using System.Web.Routing;

namespace Magazin.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Home",  action = "Index", id = UrlParameter.Optional },
                namespaces:new []{ "Magazin.Areas.Admin.Controllers" }
            );

            context.MapRoute("Products", "Admin/Products/", new {controller = "Product", action = "Products" });

        }
    }
}