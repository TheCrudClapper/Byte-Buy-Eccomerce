using ByteBuy.Core.Domain.EntityContracts;

namespace ByteBuy.Core.Domain.Entities;

public class Address : AuditableEntity, ISoftDelete
{
    public string PlaceName { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string HouseNumber { get; set; } = null!;
    public string PostalCity { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public Guid? UserId { get; set; }
    public ApplicationUser? User { get; set; }
    public Guid CountryId { get; set; }
    public Country Country { get; set; } = null!;
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }
}