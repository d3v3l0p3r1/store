﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BaseCore.Services.Abstract
{

    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> CreateAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteAsync(IEnumerable<T> entities);
        Task<T> UpdateAsync(T entity);
        Task<T> GetAsync(long id);
        IQueryable<T> GetAllAsNotracking();
        DbSet<T> GetDbSet();
    }
}
