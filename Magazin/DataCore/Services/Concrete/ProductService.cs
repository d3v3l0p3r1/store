using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public override Product Find(int id)
        {
            return _repository.GetDbSet().Include(x => x.Category).Single(x => x.Id == id);            
        }

        
    }
}
