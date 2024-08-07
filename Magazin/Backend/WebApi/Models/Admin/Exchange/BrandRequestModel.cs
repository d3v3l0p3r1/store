﻿namespace WebApi.Models.Admin.Exchange
{
    /// <summary>
    /// Модель создания бренда
    /// </summary>
    public class BrandRequestModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }


    }
}
