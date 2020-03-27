using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseCore.Services.Abstract;
using BaseCore.Services.Concrete;
using DataCore.Entities;
using DataCore.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DataCore.Services.Concrete
{
    public class CarouselService : BaseService<Carousel>, ICarouselService
    {
        private readonly IFileService _fileService;

        public CarouselService(IRepository<Carousel> repository, IFileService fileService) : base(repository)
        {
            _fileService = fileService;
        }

        public async Task<Carousel> GetCarousel()
        {
            var carousel = await _repository.GetAll().OrderByDescending(x => x.Id).FirstAsync();

            return carousel;
        }
    }
}
