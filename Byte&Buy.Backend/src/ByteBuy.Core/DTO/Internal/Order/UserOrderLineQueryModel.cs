using ByteBuy.Core.DTO.Internal.Order.Enum;
using ByteBuy.Core.DTO.Public.ImageThumbnail;
using ByteBuy.Core.DTO.Public.Money;

namespace ByteBuy.Core.DTO.Internal.Order;

public sealed record UserOrderLineQueryModel(
    string ItemTitle,
    OrderLineType Type,
    int Quantity,
    MoneyDto Total,
    ImageThumbnailDto Thumbnail,
    MoneyDto? PricePerItem,
    MoneyDto? PricePerDay,
    int? RentalDays
);
