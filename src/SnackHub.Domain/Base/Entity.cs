namespace SnackHub.Domain.Base;

public abstract class Entity<TId> : IEquatable<Entity<TId>>
    where TId : notnull
{
    protected Entity()
    {
    }

    protected Entity(TId id)
    {
        Id = id;
    }

    public virtual TId Id { get; }
    
    public virtual DateTimeOffset CreatedAt { get; protected set; } = DateTimeOffset.UtcNow;
    
    public virtual DateTimeOffset? UpdatedAt { get; protected set; } = DateTimeOffset.UtcNow;

    public static bool operator ==(Entity<TId> left, Entity<TId> right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Entity<TId> left, Entity<TId> right)
    {
        return !Equals(left, right);
    }

    public virtual bool Equals(Entity<TId>? other)
    {
        return Equals((object?)other);
    }

    public override bool Equals(object? obj)
    {
        return obj is Entity<TId> entity && Id.Equals(entity.Id);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
