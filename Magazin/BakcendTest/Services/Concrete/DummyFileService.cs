using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BaseCore.DAL.Implementations.Entities;
using BaseCore.File;

namespace BackendTest.Services.Concrete
{
    public class DummyFileService : IFileService
    {
        public Task<FileData> CreateAsync(FileData entity)
        {
            return Task.FromResult(new FileData());
        }

        public Task DeleteAsync(long id)
        {
            return Task.Delay(1);
        }

        public IQueryable<FileData> GetAllAsNotracking()
        {
            throw new NotImplementedException();
        }

        public Task<FileData> GetAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetFilePath(int id)
        {
            throw new NotImplementedException();
        }

        public string GetImageNotFound()
        {
            throw new NotImplementedException();
        }

        public IQueryable<FileData> GetQuery()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetVirtualPath(int id)
        {
            throw new NotImplementedException();
        }

        public Task<FileData> SaveFile(string fileName, Stream stream)
        {
            throw new NotImplementedException();
        }

        public Task<FileData> UpdateAsync(FileData entity)
        {
            throw new NotImplementedException();
        }
    }
}
