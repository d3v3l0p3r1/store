using System.IO;
using System.Threading.Tasks;
using BaseCore.DAL.Implementations.Entities;

namespace BaseCore.File
{
    public interface IFileService
    {
        Task<FileData> SaveFile(string fileName, Stream stream);
        Task<string> GetFilePath(int id);
        Task<string> GetVirtualPath(int id);
    }
}
