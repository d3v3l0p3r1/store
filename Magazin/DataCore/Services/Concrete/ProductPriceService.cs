using BaseCore.Services.Abstract;
using BaseCore.Services.Concrete;
using DataCore.Entities;
using DataCore.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataCore.Services.Concrete
{
    public class ProductPriceService : BaseService<ProductPrice>, IProductPriceService
    {
        public ProductPriceService(IRepository<ProductPrice> repository) : base(repository)
        {

        }
    }
}
