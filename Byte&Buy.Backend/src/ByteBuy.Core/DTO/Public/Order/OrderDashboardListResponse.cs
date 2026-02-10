using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.DTO.Public.Money;

namespace ByteBuy.Core.DTO.Public.Order;

public record OrderDashboardListResponse(
     Guid Id,
     string BuyerEmail,
     MoneyDto Price,
     int LinesCount,
     DateTime PurchaseDate,
     OrderStatus Status);

