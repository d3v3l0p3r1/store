using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BaseCore.Entities;
using BaseCore.Services.Abstract;
using DataCore.Entities;
using DataCore.Models;
using Microsoft.AspNetCore.Http;

namespace DataCore.Services.Abstract
{
    public interface IProductService : IBaseService<Product>
    {
        Task<List<BalancedProductModel>> GetWithBalance(long cat, string schema, string host);
        Task CreateAsync(Product product, IFormFile formFile, List<IFormFile> images);
        Task UpdateAsync(Product product, IFormFile mainImage, List<IFormFile> images);
    }
}
