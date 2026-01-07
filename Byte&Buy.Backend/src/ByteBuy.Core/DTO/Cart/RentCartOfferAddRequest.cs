
namespace ByteBuy.Core.DTO.Cart;

public record RentCartOfferAddRequest(
    int Quantity,
    Guid OfferId,
    int RentalDays);
