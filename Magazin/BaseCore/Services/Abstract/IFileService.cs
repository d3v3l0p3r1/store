using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BaseCore.Entities;

namespace BaseCore.Services.Abstract
{
    public interface IFileService : IBaseService<FileData>
    {
        FileData SaveFile(string fileName, Stream stream);
        string GetFilePath(int id);
        string GetVirtualPath(int id);
    }
}
