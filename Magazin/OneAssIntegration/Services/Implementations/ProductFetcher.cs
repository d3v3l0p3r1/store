using System;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using BaseCore.DAL.Abstractions.Repositories;
using BaseCore.DAL.Implementations.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OneAss.Services;
using OneAssIntegration.Options;
using OneAssIntegration.Services.Abstractions;

namespace OneAssIntegration.Services.Implementations
{
    public class ProductFetcher : IProductFetcher
    {
        private readonly SiteExchangePortTypeClient _client;
        private readonly IRepository<OneAssSync> _syncRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Brand> _brandRepository;
        private readonly IRepository<ProductCategory> _categoryRepository;
        private readonly OneAssOptions _options;

        public ProductFetcher(IRepository<OneAssSync> syncRepository, 
            IRepository<Product> productRepository,
            IRepository<Brand> brandRepository,
            IRepository<ProductCategory> categoryRepository,
            IOptions<OneAssOptions> options)
        {
            _syncRepository = syncRepository;
            _productRepository = productRepository;
            _brandRepository = brandRepository;
            _categoryRepository = categoryRepository;

            _options = options.Value;

            _client = new SiteExchangePortTypeClient(GetBindings(), new EndpointAddress(_options.Url));
            _client.ClientCredentials.UserName.UserName = _options.UserName;
            _client.ClientCredentials.UserName.Password = _options.Password;

        }

        private Binding GetBindings()
        {
            var binding = new CustomBinding()
            {
                Elements =
                {
                    new TextMessageEncodingBindingElement(MessageVersion.CreateVersion(EnvelopeVersion.Soap12, AddressingVersion.None), Encoding.UTF8),
                    new HttpsTransportBindingElement()
                    {
                        AllowCookies = true,
                        MaxBufferSize = int.MaxValue,
                        MaxReceivedMessageSize = int.MaxValue,
                        AuthenticationScheme = AuthenticationSchemes.Basic,
                        ProxyAuthenticationScheme = AuthenticationSchemes.Basic,
                        UseDefaultWebProxy = true,
                        BypassProxyOnLocal = true,
                    },
                }
            };

            return binding;

        }

        public async Task LoadProducts()
        {
            var last = await _syncRepository.GetAll().OrderByDescending(x => x.Id).FirstOrDefaultAsync();

            await _client.OpenAsync();

            var items =  await _client.GetItemsAsync(last?.DateTime, null);

            last = new OneAssSync
            {
                DateTime = DateTime.UtcNow
            };

            

            foreach (var item in items.@return.Каталог.Товары)
            {
                var cat = await _categoryRepository.GetAll().FirstOrDefaultAsync(x => x.Title == item.Категория);
                if (cat == null)
                {
                    cat = new ProductCategory()
                    {
                        Title = item.Категория
                    };
                    await _categoryRepository.CreateAsync(cat);
                }

                var brand = await _brandRepository.GetAll().FirstOrDefaultAsync(x => x.Title == item.Группы[0]);
                if (brand == null)
                {
                    brand = new Brand
                    {
                        Title = item.Группы[0]
                    };
                    await _brandRepository.CreateAsync(brand);
                }

                var str = JsonConvert.SerializeObject(item);

                var product = new Product
                {
                    Title = item.Наименование,
                    Description = item.Описание,
                    BrandId = brand.Id,
                    CategoryId = cat.Id,
                    VenderCode = item.Артикул,
                    MeasureUnit = item.БазоваяЕдиница.НаименованиеПолное,

                };
            }

            await _syncRepository.CreateAsync(last);
        }



    }
}
