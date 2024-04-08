<<<<<<< HEAD:Magazin/DataCore/Services/Concrete/ContractorService.cs
﻿using BaseCore.Services.Abstract;
using DataCore.Entities;
using DataCore.Services.Abstract;
=======
﻿using System.Linq;
using System.Threading.Tasks;
using BaseCore.Catalogues.Services.Abstract;
using BaseCore.DAL.Abstractions.Repositories;
using BaseCore.DAL.Implementations.Entities;
using BaseCore.File;
using Microsoft.AspNetCore.Http;
>>>>>>> 703143d03ef44b8f5666e74dce4a64271aa0157c:Magazin/Backend/BaseCore.Catalogues/Services/Concrete/ContractorService.cs
using Microsoft.EntityFrameworkCore;

namespace BaseCore.Catalogues.Services.Concrete
{
    public class ContractorService : IContractorService
    {

        private readonly IFileService _fileService;
        private readonly IRepository<Contractor> _repository;

        public ContractorService(IRepository<Contractor> repository, IFileService fileService)
        {
            _fileService = fileService;
            _repository = repository;
        }

        public async Task CreateAsync(Contractor contractor, IFormFile image)
        {
            if (image != null)
            {
                using var ms = image.OpenReadStream();
                var mainImage = await _fileService.SaveFile(image.FileName, ms);

                contractor.ImageId = mainImage.Id;
            }

            await _repository.CreateAsync(contractor);
        }

        public async Task DeleteAsync(long id)
        {
            var entity = await GetAsync(id);
            await _repository.DeleteAsync(entity);
        }

        public IQueryable<Contractor> GetAllAsNotracking()
        {
            return _repository.GetAllAsNotracking();
        }

        public Task<Contractor> GetAsync(long id)
        {
            return _repository.GetAll()
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

            await _repository.UpdateAsync(contractor);
        }
    }
}
