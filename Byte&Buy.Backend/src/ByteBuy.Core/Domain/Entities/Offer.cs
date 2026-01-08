using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Entities;

public abstract class Offer : AuditableEntity, ISoftDeletable
{
    public Guid ItemId { get; set; }
    public Item Item { get; set; } = null!;
    public ICollection<OfferDelivery> OfferDeliveries { get; set; } = new List<OfferDelivery>();
    public AddressValueObject OwnerAddressSnapshot = null!;
    public int QuantityAvailable { get; set; }
    public Guid CreatedByUserId { get; set; }
    public ApplicationUser CreatedBy { get; set; } = null!;
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }

    //EF Navigation Properties ONLY
    public ICollection<CartOffer> CartOffers { get; set; } = new List<CartOffer>();
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    protected Offer() { }

    protected Offer(Guid itemId, Guid createdByUserId, int quantityAvailable, AddressValueObject offerAddress)
    {
        ItemId = itemId;
        CreatedByUserId = createdByUserId;
        QuantityAvailable = quantityAvailable;
        OwnerAddressSnapshot = offerAddress;
        IsActive = true;
        DateCreated = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        DateDeleted = DateTime.UtcNow;
        foreach (var od in OfferDeliveries)
            od.Deactivate();
    }

    public void DeactivateForPortalUser()
    {
        IsActive = false;
        DateDeleted = DateTime.UtcNow;
        Item.Deactivate();
    }

    public static Result ValidateBasicInfo(int quantityAvailable)
    {
        if (quantityAvailable < 1)
            return Result.Failure(OfferErrors.QuantityAvaliableInvalid);
        return Result.Success();
    }

    /// <summary>
    /// Assigns deliveries to NEWLY created Offer
    /// </summary>
    /// <param name="newDeliveryIds"></param>
    public void AssignDeliveriesToOffer(IEnumerable<Guid> newDeliveryIds)
    {
        foreach (var deliveryId in newDeliveryIds)
            OfferDeliveries.Add(OfferDelivery.Create(Id, deliveryId));

    }

    /// <summary>
    /// Updates already existing offer's deliveries
    /// </summary>
    /// <param name="newDeliveryIds">Ids of deliveries to be assigned to offer</param>
    /// <returns></returns>
    public Result UpdateDeliveries(IEnumerable<Guid> newDeliveryIds)
    {
        //Deactivate those deliveries that are not in new set
        foreach (var offerDelivery in OfferDeliveries)
        {
            if (!newDeliveryIds.Contains(offerDelivery.DeliveryId))
                offerDelivery.Deactivate();
        }

        //If offer offerDelivery is found within collection (even soft deleted) - reactivate
        //If new offerDelivery id is provied and not in collection - add
        foreach (var deliveryId in newDeliveryIds)
        {
            var existing = OfferDeliveries
                .FirstOrDefault(d => d.DeliveryId == deliveryId);

            if (existing is null)
                OfferDeliveries.Add(OfferDelivery.Create(Id, deliveryId));
            else
                existing.Reactivate();
        }

        return Result.Success();
    }
}
