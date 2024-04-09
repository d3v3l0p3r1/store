using System.Linq;
using BaseCore.DAL.Abstractions.Repositories;
using BaseCore.News.Services.Abstractions;

namespace BaseCore.News.Services.Implementations
{
    public class NewsService : INewsService
    {
        private readonly IRepository<DAL.Implementations.Entities.News, long> _repository;
        public NewsService(IRepository<DAL.Implementations.Entities.News, long> repository)
        {
            _repository = repository;
        }

        public IQueryable<DAL.Implementations.Entities.News> GetAllAsNotracking()
        {
            return _repository.GetAllAsNotracking();
        }
    }
}
