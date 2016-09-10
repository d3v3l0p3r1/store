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

        public ProductService()
        {
        }

        private void CheckBeforeSave(IUnitOfWork uow, Product entity)
        {
            var duplicates = GetAll(uow).Where(x => x.Title.ToUpper() == entity.Title.ToUpper());

            if (entity.Id != 0)
            {
                duplicates = duplicates.Where(x => x.Id != entity.Id);
            }

            if (duplicates.Any())
            {
                throw new Exception("Продукт с таким именем уже существет");
            }

            if (entity.Price < 1)
            {
                throw new Exception("Цена не может быть меньше 1");
            }
        }

        public override Product Update(IUnitOfWork uow, Product entity)
        {
            CheckBeforeSave(uow, entity);

            return base.Update(uow, entity);
        }
    }
}