using ByteBuy.Core.DTO.Public.Checkout;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface ICheckoutService
{
    Task<Result<CheckoutResponse>> GetCheckout(Guid userId, CancellationToken ct = default);
}
