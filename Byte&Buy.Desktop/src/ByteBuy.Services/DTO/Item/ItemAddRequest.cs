using ByteBuy.Services.DTO.Image;

namespace ByteBuy.Services.DTO.Item;

public record ItemAddRequest(
        Guid CategoryId,
        Guid ConditionId,
        string Name,
        string Description,
        int StockQuantity,
        IList<ImageAddRequest> Images
    );