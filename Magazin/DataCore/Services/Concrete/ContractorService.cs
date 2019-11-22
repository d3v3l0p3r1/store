using BaseCore.Services.Abstract;
using BaseCore.Services.Concrete;
using DataCore.Entities;
using DataCore.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DataCore.Services.Concrete
{
    public class ContractorService : BaseService<Contractor>, IContractorService
    {

        private readonly IFileService _fileService;

        public ContractorService(IRepository<Contractor> repository, IFileService fileService) : base(repository)
        {
            _fileService = fileService;
        }

        public async Task CreateAsync(Contractor contractor, IFormFile image)
        {
            if (image != null)
            {
                using var ms = image.OpenReadStream();
                var mainImage = await _fileService.SaveFile(image.FileName, ms);

                contractor.ImageId = mainImage.Id;
            }

            await CreateAsync(contractor);
        }

        public override Task<Contractor> GetAsync(long id)
        {
            return GetQuery()
                .Include(x => x.Image)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Contractor contractor, IFormFile image)
        {
            if (image != null)
            {
                using var ms = image.OpenReadStream();
                var mainFd = await _fileService.SaveFile(image.FileName, ms);

                contractor.ImageId = mainFd.Id;
            }

            await UpdateAsync(contractor);
        }
    }
}
