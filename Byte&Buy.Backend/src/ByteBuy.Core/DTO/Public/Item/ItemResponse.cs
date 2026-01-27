using ByteBuy.Core.DTO.Public.Image;

namespace ByteBuy.Core.DTO.Public.Item;

public sealed record ItemResponse(
        Guid Id,
        Guid CategoryId,
        Guid ConditionId,
        string Name,
        string Description,
        int StockQuantity,
        IReadOnlyCollection<ImageResponse> Images
    );