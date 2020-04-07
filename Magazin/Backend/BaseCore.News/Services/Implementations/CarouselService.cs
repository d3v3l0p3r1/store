using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseCore.DAL.Abstractions.Repositories;
using BaseCore.DAL.Implementations.Entities;
using BaseCore.File;
using BaseCore.News.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BaseCore.News.Services.Implementations
{
    public class CarouselService : ICarouselService
    {
        private readonly IFileService _fileService;
        private readonly IRepository<Carousel> _repository;

        public CarouselService(IRepository<Carousel> repository, IFileService fileService)
        {
            _fileService = fileService;
            _repository = repository;
        }

        public async Task<List<Carousel>> GetCarousel()
        {
            var carousel = await _repository.GetAll()
                .OrderByDescending(x => x.Id)
                .Where(x=>x.Show)
                .ToListAsync();

            return carousel;
        }

        public Task Create(Carousel model)
        {
            return _repository.CreateAsync(model);
        }

        public IQueryable<Carousel> GetAllAsNotracking()
        {
            return _repository.GetAllAsNotracking();
        }

        public Task<Carousel> GetAsync(long id)
        {
            return _repository.GetAsync(id);
        }

        public Task UpdateAsync(Carousel entity)
        {
            return _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetAsync(id);
            await _repository.DeleteAsync(entity);
        }
    }
}
