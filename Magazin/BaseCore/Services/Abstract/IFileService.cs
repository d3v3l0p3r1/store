using System;
using System.Collections.Generic;
using System.Text;
using BaseCore.Entities;

namespace BaseCore.Services.Abstract
{
    public interface IFileService : IBaseService<FileData>
    {
        FileData SaveFile();
    }
}
