using System;
using System.Linq;
using System.Threading.Tasks;
using BaseCore.DAL.Abstractions.Repositories;
using BaseCore.DAL.Implementations.Entities.Documents;
using BaseCore.Documents.Implementations.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BaseCore.Documents.Implementations.Services
{
    public abstract class BaseDocumentService<T, TEntry> : IBaseDocumentService<T, TEntry>
        where T : BaseDocument<TEntry>, new()
        where TEntry : BaseDocumentEntry
    {
        private readonly IBalanceService _balanceService;
        private readonly IRepository<TEntry> _entryRepository;
        private readonly IRepository<T> _repository;

        public BaseDocumentService(IRepository<T> repository, IBalanceService balanceService, IRepository<TEntry> entryRepository)
        {
            _repository = repository;
            _balanceService = balanceService;
            _entryRepository = entryRepository;
        }

        public Task<T> GetAsync(long id)
        {
            return _repository.GetAll()
               .Include(x => x.Author)
               .Include(x => x.Entries)
               .ThenInclude(x => x.Product)
               .FirstOrDefaultAsync(x => x.Id == id);
        }

        public abstract Task Apply(long id);

        public abstract Task Discard(long id);
        public IQueryable<T> GetAllAsNotracking()
        {
            return _repository.GetAllAsNotracking();
        }

        public async Task DeleteAsync(long id)
        {
            var original = await GetAsync(id);
            if (original.DocumentStatus == DocumentStatus.Processed)
            {
                throw new Exception("Документ проведен, удаление невозможно");
            }

            await _repository.DeleteAsync(original);
        }

        public Task CreateAsync(T entity)
        {
            if (!entity.Entries.Any())
            {
                throw new Exception("Должна быть хотя бы одна позиция");
            }

            entity.DocumentStatus = DocumentStatus.New;
            entity.Date = DateTime.Now;
            entity.Title = $"Документ от {entity.Date:g}";
            
            return _repository.CreateAsync(entity);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            if (!entity.Entries.Any())
            {
                throw new Exception("Должна быть хотя бы одна позиция");
            }

            var allEntries = await _entryRepository.GetAllAsNotracking().Where(x => x.DocumentId == entity.Id).ToListAsync();
            var entryIds = entity.Entries.Select(x => x.Id);
            var toDelete = allEntries.Where(x => !entryIds.Contains(x.Id));
            await _entryRepository.DeleteAsync(toDelete);

            return await _repository.UpdateAsync(entity);
        }

        Task IBaseDocumentService<T, TEntry>.UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
