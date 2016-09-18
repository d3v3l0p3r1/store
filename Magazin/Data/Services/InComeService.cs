using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Services;
using Data.Entities;

namespace Data.Services
{

    public interface IInComeService : IBaseService<InCome>
    {

    }


    public class InComeService : BaseService<InCome>, IInComeService
    {

    }
}
