using ByteBuy.Services.DTO.Image;
using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Item;

public record ItemUpdateRequest(
      [Required] Guid CategoryId,
      [Required] Guid ConditionId,
      [Required, MaxLength(75)] string Name,
      [Required, MaxLength(2000)] string Description,
      [Required] int StockQuantity,
      [Required] IEnumerable<ImageUpdateRequest> Images
    );
