using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.Domain.ValueObjects;

namespace ByteBuy.Core.Domain.Entities;

public class OrderDelivery : AuditableEntity, ISoftDeletable
{
    public Guid OrderId { get; set; }

    //Delivery snapshot fields
    public string DeliveryName { get; private set; } = null!;
    public string CarrierCode { get; private set; } = null!;
    public DeliveryChannel Channel { get; private set; }
    public Money Price { get; private set; } = null!;

    // Courier 
    public string? BuyerFullName { get; private set; }
    public string? Street { get; private set; }
    public string? HouseNumber { get; private set; }
    public string? FlatNumber { get; private set; }
    public string? City { get; private set; }
    public string? PostalCity { get; private set; }
    public string? PostalCode { get; private set; }
    public string? Phone { get; private set; }

    // Pickup
    public string? PickupPointId { get; private set; }
    public string? PickupPointName { get; private set; }
    public string? PickupStreet { get; private set; }
    public string? PickupCity { get; private set; }
    public string? PickupLocalNumber { get; private set; }

    // Locker
    public string? ParcelLockerId { get; private set; }

    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }

    //Navigatio Ef
    public Order Order { get; set; } = null!;
}
