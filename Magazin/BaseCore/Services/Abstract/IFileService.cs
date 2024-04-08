using BaseCore.Entities;
using System.IO;
using System.Threading.Tasks;

namespace BaseCore.Services.Abstract
{
    public interface IFileService
    {
        Task<FileData> SaveFile(string fileName, Stream stream);
        Task<string> GetFilePath(int id);
        Task<string> GetVirtualPath(int id);
    }
}
