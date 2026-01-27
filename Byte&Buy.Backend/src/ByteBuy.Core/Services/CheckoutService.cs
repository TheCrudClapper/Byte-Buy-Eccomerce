using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.Public.Checkout;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;
using static ByteBuy.Core.Specification.CartSpecifications;
using static ByteBuy.Core.Specification.PortalUserSpecifications;

namespace ByteBuy.Core.Services;

public class CheckoutService : ICheckoutService
{
    private readonly IPortalUserRepository _portalUserRepository;
    private readonly ICartRepository _cartRepository;
    public CheckoutService(IPortalUserRepository portalUserRepository,
        ICartRepository cartRepository)
    {
        _portalUserRepository = portalUserRepository;
        _cartRepository = cartRepository;
    }

    public async Task<Result<CheckoutResponse>> GetCheckout(Guid userId, CancellationToken ct)
    {
        var spec = new PortalUserToUserBasicInfoResponseSpec(userId);
        var userData = await _portalUserRepository.GetBySpecAsync(spec, ct);

        if (userData is null)
            return Result.Failure<CheckoutResponse>(CommonUserErrors.NotFound);

        var cartOfferFlatSpec = new FlatCartOffersSpec(userId);
        var c
    }
}

