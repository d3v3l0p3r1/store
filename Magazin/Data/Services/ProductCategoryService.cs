using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.DAL;
using Base.Services;
using Data.Entities;

namespace Data.Services
{
    public class ProductCategoryService : BaseService<ProductCategory>, IProductCategoryService
    {
        public ProductCategoryService(IRepository<ProductCategory> repository) : base(repository)
        {
        }
    }
}
