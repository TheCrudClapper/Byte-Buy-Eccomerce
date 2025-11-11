namespace ByteBuy.Core.Domain.EntityContracts;

/// <summary>
/// Defines members for entities that support soft deletion, allowing them to be marked as inactive without being
/// permanently removed.
/// </summary>
/// <remarks>Implementing this interface enables tracking of an entity's active status and the date it was
/// deleted, which can be useful for audit trails or restoring deleted items.</remarks>
public interface ISoftDelete
{
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }
}
