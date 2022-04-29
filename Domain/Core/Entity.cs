namespace Domain.Core
{
    public class Entity<TEntity> : IEntity
        where TEntity : IEntity, new()
    {
        public long? ID { get; set; }
    }
}
