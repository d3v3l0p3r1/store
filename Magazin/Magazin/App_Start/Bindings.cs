using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Base.DAL;
using Base.Security.Services;
using Data;
using Data.DAL;
using Data.Entities;
using Data.Services;
using SimpleInjector;

namespace Magazin.App_Start
{
    public class Bindings
    {
        public static void Bind(Container container)
        {
            container.Register<IUserRepository, UserRepository>(Lifestyle.Singleton);
            container.Register<IRepository<Product>, Repository<Product>>(Lifestyle.Singleton);
            container.Register<IProductService, ProductService>(Lifestyle.Singleton);
        }
    }
}