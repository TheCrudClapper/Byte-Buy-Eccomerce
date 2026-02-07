using ByteBuy.Services.DTO.Image;

namespace ByteBuy.Core.DTO.Item;

public sealed record ItemUpdateRequest(
      Guid CategoryId,
      Guid ConditionId,
      string Name,
      string Description,
      int AdditionalStockQuantity,
      IList<ImageAddRequest> NewImages,
      IList<ExistingImageUpdateRequest> ExistingImages);
