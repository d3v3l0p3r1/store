using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Base.DAL;
using Base.Security.Entities;
using Base.Security.Services;
using Base.Services;
using Data;
using Data.DAL;
using Data.Entities;
using Data.Services;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using SimpleInjector;

namespace Magazin.App_Start
{
    public class Bindings
    {
        public static void Bind(Container container)
        {                        
            container.Register<IUserRepository, UserRepository>();
            container.Register<IBaseService<BalanceOfProduct>, BalanceOfProductService>();
            container.Register<IProductService, ProductService>();
            container.Register<IInComeService, InComeService>();
            container.Register<IProductCategoryService, ProductCategoryService>();
            container.Register<IBalanceOfProductService, BalanceOfProductService>();

            container.Register<IUpdateConfigurationManager, UpdateConfigurationManager>(Lifestyle.Singleton);
        }
    }
}