using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Base.DAL;
using Base.Security.Entities;
using Base.Security.Services;
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
            container.Register<IUserStore<User, int>, UserRepository>();
            container.Register<IUserRepository, UserRepository>();
            container.Register<IRepository<Product>, Repository<Product>>();
            container.Register<IProductService, ProductService>();
        }
    }
}