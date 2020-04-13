using System.Linq;
using BaseCore.DAL.Implementations.Entities;

namespace BaseCore.Products.Abstractions.Services
{
    public interface IKindService 
    {
        IQueryable<ProductKind> GetAllAsNotracking();
    }
}
