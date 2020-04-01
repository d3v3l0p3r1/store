using System.Linq;

namespace BaseCore.News.Services.Abstractions
{
    public interface INewsService
    {
        IQueryable<DAL.Implementations.Entities.News> GetAllAsNotracking();
    }
}
