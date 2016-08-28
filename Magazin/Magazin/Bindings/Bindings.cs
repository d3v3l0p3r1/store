using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Base.DAL;
using Base.Services;
using Data;
using Data.Services;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;

namespace Magazin.Bindings
{
    public static class Bindings
    {
        private static Lazy<Container> container = new Lazy<Container>(CreateContainer);

        static Bindings()
        {
            
        }

        public static Container Container => container.Value;

        public static Container CreateContainer()
        {
            var cont = new Container();
            Bind(cont);
            return cont;
        }

        public static void Bind(Container container)
        {
            container.Register<IProductService, ProductService>();
            container.RegisterSingleton<IServiceLocator>(new ServiceLocator(container));
            container.RegisterSingleton<IDependencyResolver>(new SimpleInjectorDependencyResolver(container));
            container.Register<IDataContext, DataContext>(Lifestyle.Singleton);
            container.Register<IRepository<Product>, Repository<Product>>(Lifestyle.Singleton);
        }

        private class ServiceLocator : IServiceLocator
        {
            private readonly Container _container;

            public ServiceLocator(Container container)
            {
                if (container == null)
                {
                    throw new ArgumentNullException(nameof(container));
                }

                _container = container;
            }

            public T GetService<T>() where T : class
            {
                try
                {
                    return _container.GetInstance<T>();
                }
                catch (ActivationException ex)
                {
                    throw;
                }

            }

            public object GetService(Type type)
            {
                try
                {
                    return _container.GetInstance(type);
                }
                catch (ActivationException ex)
                {
                    throw;
                }
            }

            public IEnumerable<object> GetServices(Type type)
            {
                try
                {
                    return _container.GetAllInstances(type);
                }
                catch (ActivationException ex)
                {
                    throw;
                }

            }
        }
    }


}