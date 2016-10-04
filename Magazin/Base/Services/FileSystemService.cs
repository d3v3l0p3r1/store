using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Entities;

namespace Base.Services
{
    public interface IFileSystemService
    {
        FileData Save(string fileName, Stream inputStream, int contentLength);
        string GetFilePath(string fileName);
    }


    public class FileSystemService : IFileSystemService
    {
        private string defaultPath { get; set; }

        public FileSystemService()
        {
            defaultPath = AppDomain.CurrentDomain.BaseDirectory + "Files";
            var di = new DirectoryInfo(defaultPath);
            if (!di.Exists)
            {
                di.Create();
            }
        }


        public FileData Save(string fileName, Stream inputStream, int contentLength)
        {
            var key = Guid.NewGuid();
            
            string appDir = Path.Combine(defaultPath, fileName);

            var result = new FileData()
            {
                FileID = key,
                FileName = fileName,
                Size = contentLength,
                CreationDate = DateTime.Now,
                ChangeDate = DateTime.Now,
                Extension = Path.GetExtension(fileName)
            };

            using (var fs = new FileStream(appDir, FileMode.Create))
            {
                inputStream.CopyTo(fs);
                fs.Close();
            }

            return result;
        }

        public string GetFilePath(string fileName)
        {           

            DirectoryInfo directoryInfo = new DirectoryInfo(defaultPath);

            var file = directoryInfo.GetFiles(fileName).FirstOrDefault();

            if (file != null)
            {
                return file.FullName;
            }

            throw new FileNotFoundException();
        }
    }



    public interface IPostedFileWrapper
    {
        void SetItem(object obj);
        int ContentLength { get; }
        string ContentType { get; }
        string FileName { get; }
        Stream InputStream { get; }
        void SaveAs(string filename);
    }
}
