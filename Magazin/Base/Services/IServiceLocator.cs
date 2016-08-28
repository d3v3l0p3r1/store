using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Services
{
    public interface IServiceLocator
    {
        T GetService<T>() where T : class;

        object GetService(Type type);

        IEnumerable<object> GetServices(Type type);
    }
}
