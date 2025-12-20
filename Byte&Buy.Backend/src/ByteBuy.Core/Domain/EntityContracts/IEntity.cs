namespace ByteBuy.Core.Domain.EntityContracts;

/// <summary>
/// Defines a contract for entities that have a unique identifier.
/// </summary>
/// <remarks>Implement this interface to ensure that an object can be uniquely identified, typically for use in
/// data persistence or entity tracking scenarios.</remarks>
public interface IEntity
{
    public Guid Id { get; set; }
}
