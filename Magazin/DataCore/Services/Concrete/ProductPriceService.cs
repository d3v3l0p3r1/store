using BaseCore.Services.Abstract;
using BaseCore.Services.Concrete;
using DataCore.Entities;
using DataCore.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCore.Services.Concrete
{
    public class ProductPriceService : BaseService<ProductPrice>, IProductPriceService
    {
        public ProductPriceService(IRepository<ProductPrice> repository) : base(repository)
        {

        }

        public override Task<ProductPrice> GetAsync(long id)
        {
            var all = _repository.GetDbSet()
                .Include(x => x.Product);

            return all.FirstOrDefaultAsync();
        }

        public override IQueryable<ProductPrice> GetAllAsNotracking()
        {
            return base.GetAllAsNotracking().Include(x => x.Product);
        }

        public override Task<ProductPrice> CreateAsync(ProductPrice entity)
        {
            if (entity.Price <= 0)
            {
                throw new Exception("Цена не может быть меньше 1");
            }

            entity.Date = DateTime.Now;

            return base.CreateAsync(entity);
        }
    }
}
