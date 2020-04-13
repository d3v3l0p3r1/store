using System.Collections.Generic;

namespace WebApi.Models.Api.Category
{
    /// <summary>
    /// Категория
    /// </summary>
    public class CategoryDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Sort order
        /// </summary>
        public decimal SortOrder { get; set; }

        /// <summary>
        /// Дочерние категории
        /// </summary>
        public List<CategoryDto> Childs { get; set; } = new List<CategoryDto>();
    }
}
