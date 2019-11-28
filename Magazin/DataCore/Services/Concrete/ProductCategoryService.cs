using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BaseCore.Services.Abstract;
using BaseCore.Services.Concrete;
using DataCore.Entities;
using DataCore.Services.Abstract;

namespace DataCore.Services.Concrete
{
    public class ProductCategoryService : BaseService<ProductCategory>, IProductCategoryService
    {
        public ProductCategoryService(IRepository<ProductCategory> repository) : base(repository)
        {

        }

        public override async Task<ProductCategory> CreateAsync(ProductCategory entity)
        {
            await base.CreateAsync(entity);

            if (entity.ParentId != null)
            {
                var parent = await GetAsync(entity.ParentId.Value);
                entity.Mask = $"{parent.Mask}{entity.Id}";
            }
            else
            {
                entity.Mask = $"{entity.Id};";
            }

            await base.UpdateAsync(entity);

            return entity;
        }

        public override async Task DeleteAsync(long id)
        {
            await base.DeleteAsync(id);
        }
    }
}
