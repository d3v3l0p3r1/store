using System;
using System.Collections.Generic;
using System.Text;
using BaseCore.Services.Abstract;
using BaseCore.Services.Concrete;
using DataCore.Entities;
using DataCore.Services.Abstract;

namespace DataCore.Services.Concrete
{
    public class ProductCategoryService : BaseService<ProductCategory> , IProductCategoryService
    {
        public ProductCategoryService(IRepository<ProductCategory> repository) : base(repository)
        {
        }
    }
}
