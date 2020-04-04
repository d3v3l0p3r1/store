using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace OneAssIntegration.Services.Abstractions
{
    public interface IProductFetcher
    {
        Task<Dictionary<string, FileImportResult>> ImportFile(Stream ms);
        Task<FileImportResult> UpdateProductPictures();
    }
}