using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BaseCore.Entities;
using BaseCore.Services.Abstract;
using BaseCore.Services.Concrete;

namespace WebUiAdmin.Concrete
{
    public class FileService : BaseService<FileData>, IFileService
    {
        private readonly string fileDir = @"Files\";
        private readonly string dir;

        public FileService(IRepository<FileData> repository) : base(repository)
        {
            dir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileDir);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }

        public FileData SaveFile(string fileName, Stream stream)
        {
            var fileID = Guid.NewGuid();

            using (var fs = new FileStream(dir + fileID, FileMode.Create))
            {
                stream.CopyTo(fs);
            }

            var fileData = new FileData
            {
                ChangeDate = DateTime.Now,
                CreationDate = DateTime.Now,
                FileName = fileName,
                FileID = fileID,
            };

            return _repository.Create(fileData);
        }

        public string GetFilePath(int id)
        {
            var fileData = Find(id);

            return Path.Combine(dir, fileData.FileID.ToString());
        }

        public string GetVirtualPath(int id)
        {
            var fileData = Find(id);

            if (fileData == null)
            {
                return null;
            }

            return $@"\{fileDir}{fileData.FileID.ToString()}";
        }
    }
}
