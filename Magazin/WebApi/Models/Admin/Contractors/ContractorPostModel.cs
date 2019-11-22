using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
