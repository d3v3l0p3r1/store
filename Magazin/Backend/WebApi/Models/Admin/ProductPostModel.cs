using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Admin
{
    /// <summary>
    /// Multipart model
    /// </summary>
    public class ProductPostModel
    {
        /// <summary>
        /// Object json
        /// </summary>
        [Required]
        public string Model { get; set; }

        /// <summary>
        /// Main image
        /// </summary>
        public IFormFile MainImage { get; set; }

        /// <summary>
        /// Images
        /// </summary>
        public List<IFormFile> Images { get; set; } = new List<IFormFile>();
    }
}
