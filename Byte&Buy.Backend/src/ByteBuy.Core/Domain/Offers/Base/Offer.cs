using ByteBuy.Core.Domain.Base;
using ByteBuy.Core.Domain.Carts.Entities;
using ByteBuy.Core.Domain.Items;
using ByteBuy.Core.Domain.Offers.Entities;
using ByteBuy.Core.Domain.Offers.Enums;
using ByteBuy.Core.Domain.Shared.ValueObjects;
using ByteBuy.Core.Domain.Users.Base;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Offers.Base;

public abstract class Offer : AggregateRoot, ISoftDeletable
{
    public Guid ItemId { get; protected set; }
    public OfferStatus Status { get; protected set; }
    public AddressValueObject OfferAddressSnapshot { get; protected set; } = null!;
    public int QuantityAvailable { get; protected set; }
    public Guid CreatedByUserId { get; protected set; }
    public Seller Seller { get; protected set; } = null!;
    public bool IsActive { get; protected set; }
    public DateTime? DateDeleted { get; protected set; }


    //EF Navigation Properties ONLY
    public ICollection<CartOffer> CartOffers { get; protected set; } = new List<CartOffer>();
    public ICollection<OfferDelivery> OfferDeliveries { get; protected set; } = new List<OfferDelivery>();
    public ApplicationUser CreatedBy { get; protected set; } = null!;
    public Item Item { get; protected set; } = null!;

    protected Offer() { }

    protected Offer(Guid itemId, Guid createdByUserId, int quantityAvailable, AddressValueObject offerAddress, Seller seller)
    {
        ItemId = itemId;
        CreatedByUserId = createdByUserId;
        QuantityAvailable = quantityAvailable;
        OfferAddressSnapshot = offerAddress;
        Seller = seller;
        IsActive = true;
        DateCreated = DateTime.UtcNow;
        Status = OfferStatus.Available;
    }

    public void Deactivate()
    {
        IsActive = false;
        DateDeleted = DateTime.UtcNow;
        foreach (var od in OfferDeliveries)
            od.Deactivate();
    }

    public void RestoreQuantity(int quantity)
    {
        QuantityAvailable += quantity;
        MarkAsAvailable();
    }

    public Result DecreaseQuantity(int requestedQuantity)
    {
        if (Status == OfferStatus.SoldOut)
            return Result.Failure(OfferErrors.SoldOut);

        if (requestedQuantity > QuantityAvailable)
            return Result.Failure(OfferErrors.QuantityDecreaseInvalid);

        QuantityAvailable -= requestedQuantity;

        if (QuantityAvailable == 0)
            Status = OfferStatus.SoldOut;

        return Result.Success();
    }

    protected void MarkAsAvailable()
    {
        if (QuantityAvailable > 0 && Status == OfferStatus.SoldOut)
            Status = OfferStatus.Available;
    }

    public static Result ValidateBasicCreateData(int quantityAvailable)
    {
        if (quantityAvailable < 1)
            return Result.Failure(OfferErrors.QuantityInvalid);
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
