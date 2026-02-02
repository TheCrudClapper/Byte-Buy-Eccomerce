using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Services.DTO.SaleOffer;

public record SaleOfferUpdateRequest(
    int AdditionalQuantity,
    decimal PricePerItem,
    IEnumerable<Guid>? ParcelLockerDeliveries,
    IEnumerable<Guid> OtherDeliveriesIds);