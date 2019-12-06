using BaseCore.Services.Abstract;
using BaseCore.Services.Concrete;
using DataCore.Entities.Documents;
using DataCore.Services.Abstract.Documents;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCore.Services.Concrete.Documents
{
    public abstract class BaseDocumentService<T, TEntry> : BaseService<T>, IBaseDocumentService<T, TEntry>
        where T : BaseDocument<TEntry>, new()
        where TEntry : BaseDocumentEntry
    {
        private readonly IBalanceService _balanceService;
        private readonly IRepository<TEntry> _entryRepository;

        public BaseDocumentService(IRepository<T> repository, IBalanceService balanceService, IRepository<TEntry> entryRepository) : base(repository)
        {
            _balanceService = balanceService;
            _entryRepository = entryRepository;
        }

        public override Task<T> GetAsync(long id)
        {
            return _repository.GetAll()
               .Include(x => x.Author)
               .Include(x => x.Entries)
               .ThenInclude(x => x.Product)
               .FirstOrDefaultAsync(x => x.Id == id);
        }

        public abstract Task Apply(long id);

        public abstract Task Discard(long id);

        public override async Task DeleteAsync(long id)
        {
            var original = await GetAsync(id);
            if (original.DocumentStatus == DocumentStatus.Processed)
            {
                throw new Exception("Документ проведен, удаление невозможно");
            }

            await base.DeleteAsync(id);
        }

        public override Task<T> CreateAsync(T entity)
        {
            if (!entity.Entries.Any())
            {
                throw new Exception("Должна быть хотя бы одна позиция");
            }

            entity.DocumentStatus = DocumentStatus.New;
            entity.Date = DateTime.Now;
            entity.Title = $"Документ от {entity.Date:g}";
            
            return base.CreateAsync(entity);
        }

        public override async Task<T> UpdateAsync(T entity)
        {
            if (!entity.Entries.Any())
            {
                throw new Exception("Должна быть хотя бы одна позиция");
            }

            var allEntries = await _entryRepository.GetAllAsNotracking().Where(x => x.DocumentId == entity.Id).ToListAsync();
            var entryIds = entity.Entries.Select(x => x.Id);
            var toDelete = allEntries.Where(x => !entryIds.Contains(x.Id));
            await _entryRepository.DeleteAsync(toDelete);

            return await base.UpdateAsync(entity);
        }
    }
}
