using ByteBuy.Core.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Public.Order;

public record OrderAddRequest(
    [Required, EnumDataType(typeof(PaymentMethod))] int PaymentMethodId,
    [Required] IEnumerable<SellerDeliveryRequest> SelectedDeliveries);