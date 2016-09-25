using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Services;
using Data.Entities;

namespace Data.Services
{

    public interface IInComeService : IBaseService<InCome>
    {
        void ProcessIncome(IUnitOfWork uow, int id);
    }


    public class InComeService : BaseService<InCome>, IInComeService
    {
        private readonly IBalanceOfProductService _balanceService;
        public InComeService(IBalanceOfProductService balanceService)
        {
            _balanceService = balanceService;
        }

        public void ProcessIncome(IUnitOfWork uow, int id)
        {
            var income = GetAll(uow).AsNoTracking().SingleOrDefault(x => x.Id == id);

            if (income == null)
            {
                throw new Exception("Не найден");
            }

            if (income.Processed)
            {
                return;                
            }

            var incoms = income.Incoms.ToList();

            foreach (var inComeEntity in incoms)
            {
                var balance = _balanceService.GetAll(uow).SingleOrDefault(x => x.ProductId == inComeEntity.ProductId) ??
                              new BalanceOfProduct
                              {
                                  Product = inComeEntity.Product
                              };

                balance.Count += inComeEntity.Count;

                _balanceService.Update(uow, balance);
            }
            

            income.Processed = true;
            Update(uow, income);
            
        }
    }
}
