using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseCore.Services.Abstract;
using BaseCore.Services.Concrete;
using DataCore.Entities;
using DataCore.Models;
using DataCore.Services.Abstract;
using DataCore.Services.Abstract.Documents;
using Microsoft.EntityFrameworkCore;

namespace DataCore.Services.Concrete
{
    public class ProductService : BaseService<Product>, IProductService
    {
        private readonly IBalanceService _balanceService;

        public ProductService(IRepository<Product> repository, IBalanceService balanceService) : base(repository)
        {
            _balanceService = balanceService;
        }

        public override Task<Product> GetAsync(long id)
        {
            return _repository.GetDbSet()
                .Include(x => x.Category)
                .SingleAsync(x => x.Id == id);
        }

        public async Task<List<BalancedProductModel>> GetWithBalance(long cat, string schema, string host)
        {
            var productsQuery = GetAllAsNotracking().Where(x => x.CategoryId == cat);
            var balanceQuery = _balanceService.GetAllAsNoTracking().Include(x => x.BalanceEntries);

            var url = $"{schema}://{host}/File/GetFile/";

            var t = from product in productsQuery
                    join balance in balanceQuery on product.Id equals balance.ProductId
                    where balance.ZeroDate == null && balance.BalanceEntries.Sum(z => z.Count) > 0
                    select new BalancedProductModel()
                    {
                        Id = product.Id,
                        CateogryId = product.Category.Id,
                        Title = product.Title,
                        Description = product.Description,
                        File = product.FileID != null ? url + product.FileID : url + 1,
                        KindId = product.KindId,
                        KingTitle = product.Kind.Title,
                        Price = product.Price,
                        Count = balance.BalanceEntries.Sum(z => z.Count)
                    };

            return await t.ToListAsync();
        }
    }
}
