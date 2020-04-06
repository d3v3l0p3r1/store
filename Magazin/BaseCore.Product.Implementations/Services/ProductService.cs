using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseCore.DAL.Abstractions.Repositories;
using BaseCore.DAL.Implementations.Entities;
using BaseCore.DAL.Implementations.Models;
using BaseCore.Documents.Implementations.Services.Abstractions;
using BaseCore.File;
using BaseCore.Products.Abstractions.Models;
using BaseCore.Products.Abstractions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BaseCore.Products.Implementations.Services
{
    public class ProductService : IProductService
    {
        private readonly IBalanceService _balanceService;
        private readonly IFileService _fileService;
        private readonly IRepository<ProductImage> _productImageRepository;
        private readonly IRepository<Product> _repository;
        private readonly IBrandService _brandService;
        private readonly IProductCategoryService _categoryService;

        public ProductService(IRepository<Product> repository, 
            IBalanceService balanceService, 
            IFileService fileService, 
            IRepository<ProductImage> productImageRepository, 
            IBrandService brandService, 
            IProductCategoryService categoryService)
        {
            _balanceService = balanceService;
            _fileService = fileService;
            _productImageRepository = productImageRepository;
            _brandService = brandService;
            _categoryService = categoryService;
            _repository = repository;
        }

        public async Task CreateAsync(Product product, IFormFile mainFile, List<IFormFile> images)
        {
            using var ms = mainFile.OpenReadStream();
            var mainImage = await _fileService.SaveFile(mainFile.FileName, ms);

            product.FileId = mainImage.Id;

            await _repository.CreateAsync(product);

            foreach (var image in images)
            {
                using var fs = image.OpenReadStream();
                var fd = await _fileService.SaveFile(image.FileName, fs);

                var productImage = new ProductImage
                {
                    FileId = fd.Id,
                    ProductId = product.Id
                };

                await _productImageRepository.CreateAsync(productImage);
            }
        }

        public async Task UpdateAsync(Product product, IFormFile mainImage, List<IFormFile> images)
        {
            if (mainImage != null)
            {
                using var ms = mainImage.OpenReadStream();
                var mainFd = await _fileService.SaveFile(mainImage.FileName, ms);

                product.FileId = mainFd.Id;
            }

            var currentImageIds = product.Images.Select(x => x.Id);
            var productImages = await _productImageRepository.GetAllAsNotracking().Where(x => x.ProductId == product.Id).ToListAsync();
            var iamgesToRemove = productImages.Where(x => !currentImageIds.Contains(x.Id));
            await _productImageRepository.DeleteAsync(iamgesToRemove);

            foreach (var image in images)
            {
                using var fs = image.OpenReadStream();
                var fd = await _fileService.SaveFile(image.FileName, fs);

                var productImage = new ProductImage
                {
                    FileId = fd.Id,
                    ProductId = product.Id
                };

                await _productImageRepository.CreateAsync(productImage);
            }

            await _repository.UpdateAsync(product);
        }

        public Task CreateAsync(Product product)
        {
            return _repository.CreateAsync(product);
        }

        public Task<Product> GetAsync(long id)
        {
            return _repository.GetAll()
                .Include(x => x.Category)
                .Include(x => x.File)
                .Include(x => x.Kind)
                .Include(x => x.Images)
                .ThenInclude(z => z.File)
                .SingleAsync(x => x.Id == id);
        }

        public async Task DeleteAsync(long id)
        {
            var entity = await GetAsync(id);
            await _repository.DeleteAsync(entity);
        }

        public async Task CreateAsync(ProductRequestModel requestModel)
        {
            var brand = await _brandService.GetByExternalIdAsync(requestModel.BrandId);
            var category = await _categoryService.GetByExternalIdAsync(requestModel.CategoryId);

            var entity = new Product
            {
                BrandId = brand.Id,
                CategoryId = category.Id,
                Title = requestModel.Title,
                Description = requestModel.Description,
                MeasureUnit = requestModel.MeasureUnit,
                VenderCode = requestModel.VenderCode,
                Price = requestModel.Price,
                ExternalId = requestModel.Id,
                CreateTime = DateTimeOffset.UtcNow,
                UpdateTime = DateTimeOffset.UtcNow
            };

            await _repository.CreateAsync(entity);
        }

        public async Task UpdateAsync(ProductRequestModel requestModel)
        {
            var brand = await _brandService.GetByExternalIdAsync(requestModel.BrandId);
            var category = await _categoryService.GetByExternalIdAsync(requestModel.CategoryId);
            var entity = await GetByExternalId(requestModel.Id);

            entity.BrandId = brand.Id;
            entity.CategoryId = category.Id;
            entity.Title = requestModel.Title;
            entity.Description = requestModel.Description;
            entity.MeasureUnit = requestModel.MeasureUnit;
            entity.VenderCode = requestModel.VenderCode;
            entity.Price = requestModel.Price;
            entity.UpdateTime = DateTimeOffset.UtcNow;

            await _repository.CreateAsync(entity);
        }

        
        public IQueryable<Product> GetAllAsNotracking()
        {
            return _repository.GetAllAsNotracking();
        }

        public IQueryable<Product> GetQuery()
        {
            return _repository.GetAll();
        }

        public Task<Product> GetByExternalId(string id)
        {
            return _repository.GetAll().FirstOrDefaultAsync(x => x.ExternalId == id);
        }
    }
}
