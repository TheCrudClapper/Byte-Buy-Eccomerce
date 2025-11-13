using ByteBuy.Core.Domain.EntityContracts;

namespace ByteBuy.Core.Domain.Entities;

public class Address : AuditableEntity, ISoftDeletable
{
    public string Label { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string HouseNumber { get; set; } = null!;
    public string PostalCity { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string? FlatNumber { get; set; }
    public Guid? UserId { get; set; }
    public PortalUser? User { get; set; }
    public Guid CountryId { get; set; }
    public Country Country { get; set; } = null!;
    public bool IsActive { get;  set; }
    public DateTime? DateDeleted { get; set; }
}