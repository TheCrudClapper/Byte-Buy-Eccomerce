using ByteBuy.Services.DTO.Image;


namespace ByteBuy.Services.DTO.Item;

public record ItemResponse(
        Guid Id,
        Guid CategoryId,
        Guid ConditionId,
        string Name,
        string Description,
        int StockQuantity,
        IReadOnlyCollection<ImageResponse> Images
    );


