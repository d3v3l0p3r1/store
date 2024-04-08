using BaseCore.DAL.Abstractions;

namespace BaseCore.DAL.Implementations.Entities
{
    /// <summary>
    /// Вид ролов
    /// </summary>
    public class ProductKind : BaseEntity
    {
        public string Title { get; set; }
    }
}
