using System.Collections.Generic;

namespace WebApi.Models
{
    /// <summary>
    /// Класс для запросов списка
    /// </summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    public class ListRespone<T> where T : class
    {
        /// <summary>
        /// Данные
        /// </summary>
        public List<T> Data { get; set; }

        /// <summary>
        /// Всего данных
        /// </summary>
        public long Total { get; set; }
    }
}
