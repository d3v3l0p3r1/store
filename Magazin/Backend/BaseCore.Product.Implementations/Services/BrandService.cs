using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BaseCore.DAL.Abstractions.Repositories;
using BaseCore.DAL.Implementations.Entities;
using BaseCore.Products.Abstractions.Models;
using BaseCore.Products.Abstractions.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;

namespace BaseCore.Products.Implementations.Services
{
    public class BrandService : IBrandService
    {
        private readonly IRepository<Brand> _repository;
        private readonly IMapper _mapper;

        public BrandService(IRepository<Brand> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Brand> CreateAsync(Brand entity)
        {
            var original = await _repository.GetAll().FirstOrDefaultAsync(x => x.ExternalId == entity.ExternalId);
            if (original != null)
            {
                throw new Exception("Бренд уже существует");
            }

            await _repository.CreateAsync(entity);

            return entity;
        }

        public Task<Brand> GetByExternalIdAsync(string externalId)
        {
            return _repository.GetAll().FirstOrDefaultAsync(x => x.ExternalId == externalId);
        }

        public async Task<Brand> UpdateAsync(Brand brand)
        {
            var original = await _repository.GetAll().FirstOrDefaultAsync(x => x.ExternalId == brand.ExternalId);
            if (original == null)
            {
                throw new Exception("Бренд не найден");
            }

            original.Title = brand.Title;
            original.Description = brand.Description;
            original.UpdateTime = DateTimeOffset.UtcNow;

            await _repository.UpdateAsync(original);

            return original;
        }

        public Task<List<BrandDto>> GetAllBrands()
        {
            return _repository.GetAllAsNotracking()
                .Select(x => new BrandDto()
                {
                    Id = x.Id,
                    Title = x.Title,
                    FileId = x.FileId
                }).ToListAsync();
        }

        public async Task UpdateAsync(BrandDetailDto model)
        {
            var entity = await _repository.GetAll().FirstOrDefaultAsync(x => x.Id == model.Id);
            entity.UpdateTime = DateTimeOffset.Now;
            entity.Title = model.Title;
            entity.FileId = model.FileId;
            entity.Description = model.Description;

            await _repository.UpdateAsync(entity);
        }

        public async Task<BrandDetailDto> Get(long id)
        {
            var entity = await _repository.GetAll()
                .Include(x=>x.File)
                .FirstOrDefaultAsync(x => x.Id == id);

            var result = _mapper.Map<BrandDetailDto>(entity);

            return result;
        }
    }
}
