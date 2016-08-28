using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.DAL;
using Base.Services;

namespace Data.Services
{
    public interface IProductService : IBaseService<Product>
    {

    }


    public class ProductService : BaseService<Product>, IProductService
    {        

        public ProductService(IRepository<Product> repository) : base(repository)
        {            
        }


    }
}