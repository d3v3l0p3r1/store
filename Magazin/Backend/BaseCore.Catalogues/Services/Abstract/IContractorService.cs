using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using BaseCore.DAL.Implementations.Entities;

namespace BaseCore.Catalogues.Services.Abstract
{
    public interface IContractorService
    {
        Task CreateAsync(Contractor product, IFormFile image);
        Task UpdateAsync(Contractor product, IFormFile image);
        IQueryable<Contractor> GetAllAsNotracking();
        Task<Contractor> GetAsync(long id);
        Task DeleteAsync(long id);
    }
}
