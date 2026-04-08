namespace ByteBuy.Core.Domain.Base;

public abstract class Entity : IEntity, IEquatable<Entity>
{
    public Guid Id { get; protected set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateEdited { get; set; }

    public override bool Equals(object? obj)
        => obj is Entity entity && Id.Equals(entity.Id);

    public static bool operator ==(Entity? left, Entity? right)
        => Equals(left, right);

    public static bool operator !=(Entity? left, Entity? right)
        => !Equals(left, right);

    public bool Equals(Entity? other)
        => Equals((object?)other);

    public override int GetHashCode()
        => Id.GetHashCode();
}
