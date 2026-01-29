using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Public.Order;

public record OrderAddRequest(
    [Required] int PaymentMethodId,
    [Required] IEnumerable<SellerDeliveryRequest> SelectedDeliveries);