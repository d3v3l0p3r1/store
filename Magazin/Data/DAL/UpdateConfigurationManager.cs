using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Base.Entities;
using RefactorThis.GraphDiff;

namespace Data.DAL
{
    public class UpdateConfigurationManager : IUpdateConfigurationManager
    {
        private Dictionary<Type, object> _configs = new Dictionary<Type, object>();

        public void AddConfiguration<T>(Expression<Func<IUpdateConfiguration<T>, object>> epxrs) where T : BaseEntity, new()
        {
            _configs.Add(typeof(T), epxrs);            
        }

        public Expression<Func<IUpdateConfiguration<T>, object>> GetConfiguration<T>() where T : BaseEntity, new()
        {
            if (_configs.ContainsKey(typeof(T)))
            {
                return _configs[typeof(T)] as Expression<Func<IUpdateConfiguration<T>, object>>;
            }
            return null;
        }
    }
}