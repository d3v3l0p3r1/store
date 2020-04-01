using System;
using System.Threading.Tasks;
using BaseCore.DAL.Abstractions.Repositories;
using BaseCore.DAL.Implementations.Entities;
using BaseCore.Products.Abstractions.Services;
using Microsoft.EntityFrameworkCore;

namespace BaseCore.Products.Implementations.Services
{
    public class BrandService : IBrandService
    {
        private readonly IRepository<Brand> _repository;
        public BrandService(IRepository<Brand> repository)
        {
            _repository = repository;
        }

        public async Task<Brand> CreateAsync(Brand entity)
        {
            var original = await _repository.GetAll().FirstOrDefaultAsync(x => x.ExternalId == entity.ExternalId);
            if (original != null)
            {
                throw new Exception("Бренд уже существует");
            }

            await _repository.CreateAsync(entity);

            return entity;
        }
    }
}
