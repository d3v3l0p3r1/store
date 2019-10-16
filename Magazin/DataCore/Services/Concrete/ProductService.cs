using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseCore.Services.Abstract;
using BaseCore.Services.Concrete;
using DataCore.Entities;
using DataCore.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DataCore.Services.Concrete
{
    public class ProductService : BaseService<Product>, IProductService
    {
        public ProductService(IRepository<Product> repository) : base(repository)
        {
        }

        public override Task<Product> FindAsync(int id)
        {
            return _repository.GetDbSet()
                .Include(x => x.Category)
                .SingleAsync(x => x.Id == id);
        }

        
    }
}
