using ByteBuy.Services.DTO.Image;
using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Item;

public record ItemUpdateRequest(
      Guid CategoryId,
      Guid ConditionId,
      string Name,
      string Description,
      int StockQuantity,
      IEnumerable<ImageUpdateRequest> Images
    );
