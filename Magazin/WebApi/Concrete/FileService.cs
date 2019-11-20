using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using BaseCore.Entities;
using BaseCore.Services.Abstract;
using BaseCore.Services.Concrete;

namespace WebUiAdmin.Concrete
{
    public class FileService : BaseService<FileData>, IFileService
    {
        private readonly string fileDir = @"Files";
        private readonly string dir;

        public FileService(IRepository<FileData> repository) : base(repository)
        {
            dir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileDir);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }

        public async Task<FileData> SaveFile(string fileName, Stream stream)
        {
            var fileID = Guid.NewGuid();

            using (var fs = new FileStream(Path.Combine(dir, fileID.ToString()), FileMode.CreateNew))
            {
                await stream.CopyToAsync(fs);
            }

            var fileData = new FileData
            {
                ChangeDate = DateTime.Now,
                CreationDate = DateTime.Now,
                FileName = fileName,
                FileID = fileID,
            };

            return await _repository.CreateAsync(fileData);
        }

        public async Task<string> GetFilePath(int id)
        {
            var fileData = await GetAsync(id);

            return Path.Combine(dir, fileData.FileID.ToString());
        }

        public async Task<string> GetVirtualPath(int id)
        {
            var fileData = await GetAsync(id);

            if (fileData == null)
            {
                return null;
            }

            return Path.Combine(fileDir, fileData.FileID.ToString());

            //return $@"\{fileDir}{fileData.FileID.ToString()}";
        }
    }
}
