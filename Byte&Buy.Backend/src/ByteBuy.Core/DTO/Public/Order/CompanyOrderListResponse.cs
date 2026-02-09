using ByteBuy.Core.DTO.Public.Money;

namespace ByteBuy.Core.DTO.Public.Order;

public sealed record CompanyOrderListResponse(
    Guid Id,
    string Status,
    DateTime PurchasedDate,
    int LinesCount,
    string BuyerEmail,
    string BuyerFullName,
    MoneyDto TotalCost);
