namespace Byte_Buy.Core.Domain.EntityContracts;

/// <summary>
/// Allows entity to be soft-deletable
/// </summary>
public interface ISoftDelete
{
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }
}
