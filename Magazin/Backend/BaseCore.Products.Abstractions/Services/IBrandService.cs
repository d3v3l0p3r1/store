using System.Collections.Generic;
using System.Threading.Tasks;
using BaseCore.DAL.Implementations.Entities;
using BaseCore.Products.Abstractions.Models;

namespace BaseCore.Products.Abstractions.Services
{
    public interface IBrandService
    {
        Task<Brand> CreateAsync(Brand entity);
        Task<Brand> GetByExternalIdAsync(string externalId);
        Task<Brand> UpdateAsync(Brand brand);
        Task<List<BrandDto>> GetAllBrands();
    }
}