using System;
using System.Linq.Expressions;
using Base.Entities;
using RefactorThis.GraphDiff;

namespace Data.DAL
{
    public interface IUpdateConfigurationManager
    {
        void AddConfiguration<T>(Expression<Func<IUpdateConfiguration<T>, object>> epxrs) where T : BaseEntity, new();
        Expression<Func<IUpdateConfiguration<T>, object>> GetConfiguration<T>() where T : BaseEntity, new();
    }
}