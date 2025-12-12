using System;

namespace ByteBuy.Core.DTO.Item;

public record ItemListResponse(
    Guid Id,
    string Name,
    int ImagesCount,
    int StockQuantity,
    string ConditionName,
    string CategoryName);
