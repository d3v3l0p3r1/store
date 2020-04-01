using System;
using System.Linq;
using System.Threading.Tasks;
using BaseCore.DAL.Abstractions.Repositories;
using BaseCore.DAL.Implementations.Entities;
using BaseCore.Products.Abstractions.Services;
using Microsoft.EntityFrameworkCore;

namespace BaseCore.Products.Implementations.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IRepository<ProductCategory> _repository;

        public ProductCategoryService(IRepository<ProductCategory> repository)
        {
            _repository = repository;
        }

        public async Task<ProductCategory> CreateAsync(ProductCategory entity)
        {
            await _repository.CreateAsync(entity);

            if (entity.ParentId != null)
            {
                var parent = await _repository.GetAsync(entity.ParentId.Value);
                entity.Mask = $"{parent.Mask}{entity.Id};";
            }
            else
            {
                entity.Mask = $";{entity.Id};";
            }

            await _repository.UpdateAsync(entity);

            return entity;
        }

        public async Task CreateAsync(string title, string id, string parentId)
        {
            var cat = await _repository.GetAll().FirstOrDefaultAsync(x => x.ExternalId == id);
            if (cat != null)
            {
                throw new Exception("Запись уже существует");
            }

            cat = new ProductCategory
            {
                Title = title,
                ExternalId = id
            };

            if (!string.IsNullOrEmpty(parentId))
            {
                var parentCat = await _repository.GetAll().FirstOrDefaultAsync(x => x.ExternalId == parentId);
                if (parentCat == null)
                {
                    throw new Exception($"Не удалось найти категорию {parentId}");
                }

                cat.ParentId = parentCat.Id;
            }

            await CreateAsync(cat);
        }

        public IQueryable<ProductCategory> GetAllAsNotracking()
        {
            return _repository.GetAllAsNotracking();
        }

        public IQueryable<ProductCategory> GetQuery()
        {
            return _repository.GetAll();
        }
    }
}
