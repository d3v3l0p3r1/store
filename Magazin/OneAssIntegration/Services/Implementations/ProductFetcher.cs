using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using BaseCore.DAL.Abstractions.Repositories;
using BaseCore.DAL.Implementations.Entities;
using BaseCore.File;
using BaseCore.Products.Abstractions.Services;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OneAss.Services;
using OneAssIntegration.Models;
using OneAssIntegration.Options;
using OneAssIntegration.Services.Abstractions;

namespace OneAssIntegration.Services.Implementations
{
    public class ProductFetcher : IProductFetcher
    {
        private const string PackagePropertyId = "c0b36a6c-4812-11ea-b7f2-96e483274866";

        private readonly SiteExchange2PortTypeClient _client;
        private readonly IRepository _repository;
        private readonly OneAssOptions _options;
        private readonly ILogger<ProductFetcher> _logger;
        private readonly IFileService _fileService;
        private readonly IBalanceService _balanceService;

        public ProductFetcher(IRepository repository,
            IOptions<OneAssOptions> options,
            ILogger<ProductFetcher> logger,
            IFileService fileService, 
            IBalanceService balanceService)
        {
            _repository = repository;
            _logger = logger;
            _fileService = fileService;
            _balanceService = balanceService;
            _options = options.Value;

            _client = new SiteExchange2PortTypeClient(GetBindings(), new EndpointAddress(_options.Url));
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

        public async Task<Dictionary<string, FileImportResult>> ImportFile(Stream ms)
        {
            try
            {
                return await DeserializeXml(ms);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        private async Task ParseXml(Stream ms)
        {
            var xDocument = await XDocument.LoadAsync(ms, LoadOptions.None, CancellationToken.None);
            if (xDocument.Root == null)
            {
                throw new Exception("Invalid XML");
            }

            var xNameSpace = xDocument.Root.GetDefaultNamespace();
            var brands = xDocument.Element(XName.Get("КоммерческаяИнформация", xNameSpace.NamespaceName))
                .Element(XName.Get("Классификатор", xNameSpace.NamespaceName))
                .Element(XName.Get("Группы", xNameSpace.NamespaceName))
                .Elements()
                .First(x => x.Element(XName.Get("Наименование", xNameSpace.NamespaceName))?.Value == "Товары")
                .Elements(XName.Get("Группы", xNameSpace.NamespaceName))
                .Elements();

            foreach (var brandElement in brands)
            {
                var id = brandElement.Element(XName.Get("Ид", xNameSpace.NamespaceName)).Value;
                var brand = await _repository.GetAll<Brand>().FirstOrDefaultAsync(x => x.ExternalId == id);
                if (brand == null)
                {
                    brand = new Brand
                    {
                        ExternalId = id,
                        CreateTime = DateTimeOffset.UtcNow,
                        UpdateTime = DateTimeOffset.UtcNow,
                        Title = brandElement.Element(XName.Get("Наименование", xNameSpace.NamespaceName)).Value
                    };

                    await _repository.CreateAsync(brand);
                }
            }
        }

        private async Task<Dictionary<string, FileImportResult>> DeserializeXml(Stream ms)
        {
            var xDocument = await XDocument.LoadAsync(ms, LoadOptions.None, CancellationToken.None);
            if (xDocument.Root == null)
            {
                throw new Exception("Invalid XML");
            }

            var serializer = new XmlSerializer(typeof(Root));

            var root = (Root)serializer.Deserialize(xDocument.CreateReader());

            var brandsResult = await WriteBrands(root.Classifier.Groups);
            var catsResult = await WriteCategories(root.Classifier.Categories);
            var catalogResult = await WriteCatalogs(root.Classifier.Properties);
            var productResult = await WriteProducts(root.Cataglogue.Items);

            return new Dictionary<string, FileImportResult>()
            {
                { "Brands", brandsResult },
                { "Categories", catsResult },
                { "Catalogue", catalogResult },
                { "Products", productResult }
            };
        }

        private async Task<FileImportResult> WriteBrands(List<Group> groups)
        {
            var childs = groups.First(x => x.Name == "Товары").Childs;
            var result = new FileImportResult
            {
                Total = childs.Count
            };

            foreach (var brandElement in childs)
            {
                var id = brandElement.Id;
                var brand = await _repository.GetAll<Brand>().FirstOrDefaultAsync(x => x.ExternalId == id);
                if (brand != null)
                {
                    continue;
                }

                brand = new Brand
                {
                    ExternalId = id,
                    CreateTime = DateTimeOffset.UtcNow,
                    UpdateTime = DateTimeOffset.UtcNow,
                    Title = brandElement.Name
                };

                await _repository.CreateAsync(brand);
                result.Success++;
            }

            return result;
        }

        private async Task<FileImportResult> WriteCategories(List<Category> categories)
        {
            var result = new FileImportResult
            {
                Total = categories.Count
            };

            foreach (var categoryElement in categories)
            {
                var productCategory = await _repository.GetAll<ProductCategory>().FirstOrDefaultAsync(x => x.ExternalId == categoryElement.Id);
                if (productCategory != null)
                {
                    productCategory.Title = categoryElement.Name;
                    productCategory.UpdateTime = DateTimeOffset.UtcNow;

                    await _repository.UpdateAsync(productCategory);
                }
                else
                {
                    productCategory = new ProductCategory()
                    {
                        ExternalId = categoryElement.Id,
                        CreateTime = DateTimeOffset.UtcNow,
                        UpdateTime = DateTimeOffset.UtcNow,
                        Title = categoryElement.Name
                    };

                    await _repository.CreateAsync(productCategory);
                }

                result.Success++;

            }

            return result;
        }

        private async Task<FileImportResult> WriteCatalogs(List<Property> props)
        {
            var result = new FileImportResult();
            var packageProps = props.FirstOrDefault(x => x.Id == PackagePropertyId);
            if (packageProps != null)
            {
                result.Total += packageProps.Properties.Count;
                foreach (var pp in packageProps.Properties)
                {
                    var package = await _repository.GetAll<Package>().FirstOrDefaultAsync(x => x.ExternalId == pp.Id);
                    if (package == null)
                    {
                        package = new Package
                        {
                            ExternalId = pp.Id,
                            Value = pp.Value
                        };

                        await _repository.CreateAsync(package);
                    }
                    else
                    {
                        package.Value = pp.Value;
                        await _repository.UpdateAsync(package);
                    }

                    result.Success++;
                }
            }

            return result;
        }

        private async Task<FileImportResult> WriteProducts(List<CatalogueItem> items)
        {
            var result = new FileImportResult
            {
                Total = items.Count
            };
            foreach (var catalogueItem in items)
            {
                var product = await _repository.GetAll<Product>().FirstOrDefaultAsync(x => x.ExternalId == catalogueItem.Id);
                if (product == null)
                {
                    product = new Product
                    {
                        CreateTime = DateTimeOffset.UtcNow,
                        ExternalId = catalogueItem.Id
                    };
                }

                product.Title = catalogueItem.Name;
                product.Description = catalogueItem.Description;
                product.UpdateTime = DateTimeOffset.UtcNow;
                product.MeasureUnit = catalogueItem.MeasureUnit.Name;
                product.VenderCode = catalogueItem.Article;

                var category = await _repository.GetAll<ProductCategory>().FirstOrDefaultAsync(x => x.ExternalId == catalogueItem.CategoryId);
                product.CategoryId = category.Id;

                var group = catalogueItem.Groups.FirstOrDefault();
                if (string.IsNullOrEmpty(group))
                {
                    result.Failed++;
                    _logger.LogError($"Не указан бренд для {catalogueItem.Id}");
                    continue;
                }

                var brand = await _repository.GetAll<Brand>().FirstOrDefaultAsync(x => x.ExternalId == group);
                if (brand == null)
                {
                    result.Failed++;
                    _logger.LogError($"ProductID ={product.ExternalId}, Brand 404 {group}.");
                    continue;
                }

                product.BrandId = brand.Id;

                var packageProperty = catalogueItem.Props.FirstOrDefault(x => x.Key == PackagePropertyId);
                if (packageProperty != null)
                {
                    var package = await _repository.GetAll<Package>().FirstOrDefaultAsync(x => x.ExternalId == packageProperty.Value);
                    if (package != null)
                    {
                        product.PackageId = package.Id;
                    }
                }

                if (product.Id == 0)
                {
                    await _repository.CreateAsync(product);
                }
                else
                {
                    await _repository.UpdateAsync(product);
                }

                result.Success++;
            }

            return result;

        }

        public async Task<FileImportResult> UpdateProductPictures()
        {
            var page = 0;
            var take = 50;
            var result = new FileImportResult();
            var query = _repository.GetAll<Product>().Where(x => x.FileId == null);

            await _client.OpenAsync();
            while (true)
            {
                var products = await query.Skip(page * take).Take(take).ToListAsync();
                if (!products.Any())
                {
                    break;
                }

                foreach (var product in products)
                {
                    result.Total++;
                    try
                    {
                        var resp = await _client.GetPictureAsync(product.ExternalId, null);
                        using (var ms = new MemoryStream(resp.@return))
                        {
                            var fileData = await _fileService.SaveFile($"pic_{product.Id}.jpg", ms);
                            product.FileId = fileData.Id;

                            await _repository.UpdateAsync(product);
                        }

                        result.Success++;
                    }
                    catch (Exception e)
                    {
                        _logger.LogError("Load image for product.", e);
                        result.Failed++;
                    }
                }

                page++;
            }

            await _client.CloseAsync();

            return result;
        }

        public async Task<FileImportResult> UpdateProductPrices(Stream ms)
        {
            var result = new FileImportResult();
            var xDocument = await XDocument.LoadAsync(ms, LoadOptions.None, CancellationToken.None);
            if (xDocument.Root == null)
            {
                throw new Exception("Invalid XML");
            }

            var serializer = new XmlSerializer(typeof(Root));

            var root = (Root)serializer.Deserialize(xDocument.CreateReader());

            foreach (var item in root.WarehouseRoot.Items)
            {
                result.Total++;
                try
                {
                    var product = _repository.GetAll<Product>().First(x => x.ExternalId == item.Id);
                    await _balanceService.SetBalance(product.Id, item.Amount);


                    var price = item.Prices.First();
                    product.Price = price.Value;
                    await _repository.UpdateAsync(product);

                    result.Success++;
                }
                catch (Exception e)
                {
                    _logger.LogError($"Set price error: {item.Id}.", e);
                    result.Failed++;
                }

            }

            return result;
        }

    }
}
