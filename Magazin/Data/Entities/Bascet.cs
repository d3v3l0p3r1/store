using Base.Entities;
using Base.Security.Entities;

namespace Data.Entities
{
    public class Bascet : BaseEntity
    {
        public int? UserID { get; set; }
        public virtual User User { get; set; }
    }
}
