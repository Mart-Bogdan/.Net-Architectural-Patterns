namespace WebApp.Core.Entity.Entities
{
    public interface IBaseEntity<TKey>
    {
        TKey Id { get; set; }
    }
}