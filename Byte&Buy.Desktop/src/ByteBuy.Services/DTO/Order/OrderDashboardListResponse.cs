using ByteBuy.Services.DTO.Money;
using ByteBuy.Services.DTO.Order.Enums;

namespace ByteBuy.Services.DTO.Order;

public record OrderDashboardListResponse(
     Guid Id,
     string BuyerEmail,
     MoneyDto Price,
     int LinesCount,
     DateTime PurchaseDate,
     OrderStatus Status);

