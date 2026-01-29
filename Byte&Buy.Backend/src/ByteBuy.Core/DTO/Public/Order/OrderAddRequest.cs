using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Public.Order;

public record OrderAddRequest(
    [Required] Guid PaymentMethodId,
    [Required] IEnumerable<SellerDeliveryRequest> SelectedDeliveries);