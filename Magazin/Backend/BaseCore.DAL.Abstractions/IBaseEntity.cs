namespace BaseCore.DAL.Abstractions
{
    public interface IBaseEntity
    {
        bool Hidden { get; set; }
        long Id { get; set; }
    }
}