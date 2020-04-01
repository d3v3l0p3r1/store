using System.Linq;
using System.Threading.Tasks;
using BaseCore.DAL.Implementations.Entities;

namespace BaseCore.Products.Abstractions.Services
{
    public interface IProductCategoryService
    {
        /// <summary>
        /// Создать категорию
        /// </summary>
        /// <param name="title">Название</param>
        /// <param name="id">Внешний идентификатор</param>
        /// <param name="parentId">Внешний идентификатор родительской категории</param>
        /// <returns></returns>
        Task CreateAsync(string title, string id, string parentId);

        IQueryable<ProductCategory> GetAllAsNotracking();
        IQueryable<ProductCategory> GetQuery();
    }
}
