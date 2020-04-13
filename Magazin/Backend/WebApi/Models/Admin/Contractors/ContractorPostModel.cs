using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Admin.Contractors
{
    /// <summary>
    /// Модель создания/изменения контрагента
    /// </summary>
    public class ContractorPostModel
    {
        /// <summary>
        /// Object json
        /// </summary>
        [Required]
        public string Model { get; set; }

        /// <summary>
        /// Аватар
        /// </summary>
        public IFormFile Image { get; set; }
    }
}
