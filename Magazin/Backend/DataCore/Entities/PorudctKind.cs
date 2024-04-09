using BaseCore.DAL.Abstractions;

namespace BaseCore.DAL.Implementations.Entities
{
    /// <summary>
    /// Вид ролов
    /// </summary>
    public class ProductKind : BaseEntity<long>
    {
        public string Title { get; set; }
    }
}
