using System;
using System.Collections.Generic;

namespace BaseCore.Services
{
    public interface IServiceLocator
    {
        T GetService<T>() where T : class;

        object GetService(Type type);

        IEnumerable<object> GetServices(Type type);
    }
}
