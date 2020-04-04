﻿using System.Threading.Tasks;
using BaseCore.DAL.Implementations.Entities;

namespace BaseCore.Products.Abstractions.Services
{
    public interface IBrandService
    {
        Task<Brand> CreateAsync(Brand entity);
        Task<Brand> GetByExternalIdAsync(string externalId);
        Task<Brand> UpdateAsync(Brand brand);
    }
}