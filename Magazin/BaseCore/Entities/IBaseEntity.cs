namespace BaseCore.Entities
{
    public interface IBaseEntity
    {
        bool Hidden { get; set; }
        long Id { get; set; }
    }
}