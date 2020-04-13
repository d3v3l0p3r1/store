using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseCore.DAL.Implementations.Entities;
using BaseCore.DAL.Implementations.Models;
using BaseCore.Products.Abstractions.Models;
using Microsoft.AspNetCore.Http;

namespace BaseCore.Products.Abstractions.Services
{
    public interface IProductService
    {
        Task CreateAsync(Product product, IFormFile formFile, List<IFormFile> images);
        Task UpdateAsync(Product product, IFormFile mainImage, List<IFormFile> images);
        IQueryable<Product> GetAllAsNotracking();
        IQueryable<Product> GetQuery();
        Task<Product> GetAsync(long id);
        Task DeleteAsync(long id);
        Task CreateAsync(ProductRequestModel requestModel);
        Task UpdateAsync(ProductRequestModel requestModel);
        Task<Product> GetByExternalId(string id);
    }
}
