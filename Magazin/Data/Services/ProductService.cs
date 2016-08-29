using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.DAL;
using Base.Services;
using Data.DAL;
using Data.Entities;

namespace Data.Services
{
    public class ProductService : BaseService<Product>, IProductService
    {
        private readonly DataContext _dataContext;

        public ProductService(IRepository<Product> repository) : base(repository)
        {
            _dataContext = new DataContext();
        }

        public IQueryable<Product> All()
        {
            return _dataContext.Products;
        }


    }
}