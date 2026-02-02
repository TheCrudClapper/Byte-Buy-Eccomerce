using ByteBuy.Core.DTO.Public.Image;
using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Public.Item;

public record ItemUpdateRequest(
      [Required] Guid CategoryId,
      [Required] Guid ConditionId,
      [Required, MaxLength(75)] string Name,
      [Required, MaxLength(2000)] string Description,
      [Required, Range(0, double.MaxValue)] int StockQuantity,
      IEnumerable<ExistingImageUpdateRequest> ExistingImages,
      IEnumerable<ImageAddRequest>? NewImages);
