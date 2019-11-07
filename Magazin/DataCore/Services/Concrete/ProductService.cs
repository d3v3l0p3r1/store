using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseCore.Services.Abstract;
using BaseCore.Services.Concrete;
using DataCore.Entities;
using DataCore.Models;
using DataCore.Services.Abstract;
using DataCore.Services.Abstract.Documents;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DataCore.Services.Concrete
{
    public class ProductService : BaseService<Product>, IProductService
    {
        private readonly IBalanceService _balanceService;
        private readonly IFileService _fileService;
        private readonly IRepository<ProductImage> _productImageRepository;

        public ProductService(IRepository<Product> repository, IBalanceService balanceService, IFileService fileService, IRepository<ProductImage> productImageRepository) : base(repository)
        {
            _balanceService = balanceService;
            _fileService = fileService;
            _productImageRepository = productImageRepository;
        }

        public async Task CreateAsync(Product product, IFormFile mainFile, List<IFormFile> images)
        {
            using var ms = mainFile.OpenReadStream();
            var mainImage = await _fileService.SaveFile(mainFile.FileName, ms);

            product.FileID = mainImage.Id;

            await CreateAsync(product);

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

                product.FileID = mainFd.Id;
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

            await UpdateAsync(product);
        }

        public override Task<Product> GetAsync(long id)
        {
            return _repository.GetDbSet()
                .Include(x => x.Category)
                .Include(x => x.File)
                .Include(x => x.Kind)
                .Include(x => x.Images)
                .ThenInclude(z => z.File)
                .SingleAsync(x => x.Id == id);
        }

        public async Task<List<BalancedProductModel>> GetWithBalance(long cat, string schema, string host)
        {
            var productsQuery = GetAllAsNotracking().Where(x => x.CategoryId == cat);
            var balanceQuery = _balanceService.GetAllAsNoTracking().Include(x => x.BalanceEntries);

            var url = $"{schema}://{host}/File/GetFile/";

            var t = from product in productsQuery
                    join balance in balanceQuery on product.Id equals balance.ProductId
                    where balance.ZeroDate == null && balance.BalanceEntries.Sum(z => z.Count) > 0
                    select new BalancedProductModel()
                    {
                        Id = product.Id,
                        CateogryId = product.Category.Id,
                        Title = product.Title,
                        Description = product.Description,
                        File = product.FileID != null ? url + product.FileID : url + 1,
                        KindId = product.KindId,
                        KingTitle = product.Kind.Title,
                        Price = product.Price,
                        Count = balance.BalanceEntries.Sum(z => z.Count)
                    };

            return await t.ToListAsync();
        }
    }
}
