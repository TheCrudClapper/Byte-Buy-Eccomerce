using ByteBuy.Services.DTO.Money;

public sealed record CompanyOrderListResponse(
    Guid Id,
    string Status,
    DateTime PurchasedDate,
    int LinesCount,
    string BuyerEmail,
    string BuyerFullName,
    MoneyDto TotalCost);
