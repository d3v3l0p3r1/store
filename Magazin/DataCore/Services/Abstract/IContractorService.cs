using BaseCore.Services.Abstract;
using DataCore.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataCore.Services.Abstract
{
    public interface IContractorService : IBaseService<Contractor>
    {
        Task CreateAsync(Contractor product, IFormFile image);
        Task UpdateAsync(Contractor product, IFormFile image);
    }
}
