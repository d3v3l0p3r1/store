using System;
using System.Collections.Generic;
using System.Text;
using BaseCore.Entities;
using BaseCore.Services.Abstract;

namespace BaseCore.Services.Concrete
{
    public class FileService : BaseService<FileData>, IFileService
    {
        public FileService(IRepository<FileData> repository) : base(repository)
        {

        }


        public FileData SaveFile()
        {
            throw new NotImplementedException();
        }
    }
}
