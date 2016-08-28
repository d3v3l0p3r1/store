using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DAL
{
    public class EntityConfiguration
    {
        private readonly List<Type> _types = new List<Type>();

        public EntityConfiguration()
        {

        }

        public void Register<T>() where T : BaseEntity
        {
            if (!_types.Contains(typeof(T)))
                _types.Add(typeof(T));
        }

        public List<Type> GetTypes()
        {
            return _types;
        }
    }
}
