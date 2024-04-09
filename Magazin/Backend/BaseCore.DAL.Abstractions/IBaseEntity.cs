namespace BaseCore.DAL.Abstractions
{
    public interface IBaseEntity<T> where T : struct
    {
        bool Hidden { get; set; }
        T Id { get; set; }
    }
}