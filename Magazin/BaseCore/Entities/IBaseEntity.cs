namespace BaseCore.Entities
{
    public interface IBaseEntity
    {
        bool Hidden { get; set; }
        int Id { get; set; }
    }
}