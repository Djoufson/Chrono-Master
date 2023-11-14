namespace Domain.Entities.Base;

public abstract class Entity<TId>
    where TId : notnull
{
    public TId Id { get; set; }

    protected Entity(TId id)
    {
        Id = id;
    }

    // For Ef Core concerns
    #pragma warning disable CS8618
    protected Entity()
    {
    }
    #pragma warning restore
}
