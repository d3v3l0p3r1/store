using System;
using System.IO;
using System.Threading.Tasks;
using BaseCore.DAL.Abstractions.Repositories;
using BaseCore.DAL.Implementations.Entities;
using BaseCore.File;

namespace WebApi.Concrete
{
    public class FileService : IFileService
    {
        private readonly string fileDir = @"Files";
        private readonly string dir;
        private readonly IRepository<FileData> _repository;

        public FileService(IRepository<FileData> repository)
        {
            dir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileDir);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            _repository = repository;
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
            var fileData = await _repository.GetAsync(id);

            return Path.Combine(dir, fileData.FileID.ToString());
        }

        public async Task<string> GetVirtualPath(int id)
        {
            var fileData = await _repository.GetAsync(id);

            if (fileData == null)
            {
                return null;
            }

            return Path.Combine(fileDir, fileData.FileID.ToString());

            //return $@"\{fileDir}{fileData.FileID.ToString()}";
        }
    }
}
