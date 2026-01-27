using ByteBuy.Core.DTO.Public.Image;
using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Public.Item;

public record ItemAddRequest(
        [Required] Guid CategoryId,
        [Required] Guid ConditionId,
        [Required, MaxLength(75)] string Name,
        [Required, MaxLength(2000)] string Description,
        [Required] int StockQuantity,
        [Required] IEnumerable<ImageAddRequest> Images);