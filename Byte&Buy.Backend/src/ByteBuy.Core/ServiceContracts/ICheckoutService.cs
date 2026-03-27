using ByteBuy.Core.Domain.Shared.ResultTypes;
using ByteBuy.Core.DTO.Public.Checkout;

namespace ByteBuy.Core.ServiceContracts;

public interface ICheckoutService
{
    Task<Result<CheckoutResponse>> GetCheckoutAsync(Guid userId, CancellationToken ct = default);
}
