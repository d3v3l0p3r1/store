using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using BaseCore.Entities;

namespace BaseCore.Services.Abstract
{
    public interface IFileService : IBaseService<FileData>
    {
        Task<FileData> SaveFile(string fileName, Stream stream);
        Task<string> GetFilePath(int id);
        Task<string> GetVirtualPath(int id);
    }
}
