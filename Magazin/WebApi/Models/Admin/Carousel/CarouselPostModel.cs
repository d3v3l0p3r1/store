using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebApi.Models.Admin.Carousel
{
    /// <summary>
    /// Модель создания карусели
    /// </summary>
    public class CarouselPostModel
    {
        /// <summary>
        /// Модель
        /// </summary>
        [Required]
        public string Model { get; set; }

        /// <summary>
        /// Картинка
        /// </summary>
        public IFormFile File { get; set; }

    }
}
