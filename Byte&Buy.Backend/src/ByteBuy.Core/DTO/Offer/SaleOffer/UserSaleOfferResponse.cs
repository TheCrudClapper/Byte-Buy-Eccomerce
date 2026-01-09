using ByteBuy.Core.DTO.Image;
using ByteBuy.Core.DTO.Money;

namespace ByteBuy.Core.DTO.Offer.SaleOffer;

public record UserSaleOfferResponse(
   Guid Id,
   Guid CategoryId,
   Guid ConditionId,
   string Name,
   string Description,
   int StockQuantity,
   int QuantityAvailable,
   MoneyDto PricePerItem,
   int MaxRentalDays,
   IReadOnlyCollection<ImageResponse> Images);