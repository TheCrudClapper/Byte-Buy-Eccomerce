using ByteBuy.Core.DTO.Image;

namespace ByteBuy.Core.DTO.Item;

public sealed record ItemResponse(
        Guid Id,
        Guid CategoryId,
        Guid ConditionId,
        string Name,
        string Description,
        int StockQuantity,

        IReadOnlyCollection<ImageResponse> Images
    );