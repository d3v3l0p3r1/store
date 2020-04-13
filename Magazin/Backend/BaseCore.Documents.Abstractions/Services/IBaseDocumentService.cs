using System.Linq;
using System.Threading.Tasks;
using BaseCore.DAL.Implementations.Entities.Documents;

namespace BaseCore.Documents.Implementations.Services.Abstractions
{
    public interface IBaseDocumentService<T, TEntry>
        where T: BaseDocument<TEntry>
        where TEntry : BaseDocumentEntry
    {
        Task Apply(long id);
        Task Discard(long id);
        IQueryable<T> GetAllAsNotracking();
        Task CreateAsync(T entity);
        Task<T> GetAsync(long id);
        Task UpdateAsync(T entity);
        Task DeleteAsync(long id);
    }
}
