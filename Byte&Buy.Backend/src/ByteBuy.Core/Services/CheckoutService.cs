using ByteBuy.Core.DTO.Checkout;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;

namespace ByteBuy.Core.Services;

public class CheckoutService : ICheckoutService
{
    public Task<Result<CheckoutResponse>> GetCheckout(Guid userId)
    {
        throw new NotImplementedException();
    }
}

