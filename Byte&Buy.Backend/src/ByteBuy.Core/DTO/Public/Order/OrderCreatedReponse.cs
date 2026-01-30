using ByteBuy.Core.Domain.Enums;

namespace ByteBuy.Core.DTO.Public.Order;

public record OrderCreatedReponse(
    Guid PaymentId, PaymentMethod MethodUsed);
