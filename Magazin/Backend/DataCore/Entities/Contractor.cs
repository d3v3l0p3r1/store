using BaseCore.DAL.Abstractions;
using System;

namespace BaseCore.DAL.Implementations.Entities
{

    /// <summary>
    /// Контрагент
    /// </summary>
    public class Contractor : BaseEntity<int>
    {
        /// <summary>
        /// Имя контрагента
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// Идентификатор изображения
        /// </summary>
        public Guid ImageId { get; set; }

        /// <summary>
        /// Аватар
        /// </summary>
        public FileData Image { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Допольнительная информация
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Телефон
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
    }
}
