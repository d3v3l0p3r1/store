using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseCore.DAL.Abstractions.Repositories;
using BaseCore.DAL.Implementations.Entities;
using BaseCore.Products.Abstractions.Models;
using BaseCore.Products.Abstractions.Services;
using Microsoft.AspNetCore.Http;

namespace OneAssIntegration.Services.Implementations
{
    public class OneAssProductService : IProductService
    {
        private readonly IRepository<Product> _repository;

        public OneAssProductService(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public IQueryable<Product> GetQuery()
        {
            return _repository.GetAll();
        }

        public IQueryable<Product> GetAllAsNotracking()
        {
            return _repository.GetAllAsNotracking();
        }

        public Task<Product> GetAsync(long id)
        {
            return _repository.GetAsync(id);
        }

        public Task<Product> UpdateAsync(Product entity)
        {
            throw new NotSupportedException("При использовании 1С операция запрещена");
        }

        public Task DeleteAsync(long id)
        {
            throw new NotSupportedException("При использовании 1С операция запрещена");
        }

        public Task CreateAsync(ProductRequestModel requestModel)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ProductRequestModel requestModel)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetByExternalId(string id)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(Product entity)
        {
            throw new NotSupportedException("При использовании 1С операция запрещена");
        }

        public Task CreateAsync(Product product, IFormFile formFile, List<IFormFile> images)
        {
            throw new NotSupportedException("При использовании 1С операция запрещена");
        }

        public Task UpdateAsync(Product product, IFormFile mainImage, List<IFormFile> images)
        {
            throw new NotSupportedException("При использовании 1С операция запрещена");
        }
    }
}
