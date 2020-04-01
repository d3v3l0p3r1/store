using System.Linq;
using BaseCore.DAL.Implementations.Entities;
using System.Threading.Tasks;

namespace BaseCore.News.Services.Abstractions
{
    public interface ICarouselService
    {
        Task Create(Carousel model);
        IQueryable<Carousel> GetAllAsNotracking();
        Task<Carousel> GetAsync(long id);
        Task UpdateAsync(Carousel entity);
        Task DeleteAsync(int id);
    }
}