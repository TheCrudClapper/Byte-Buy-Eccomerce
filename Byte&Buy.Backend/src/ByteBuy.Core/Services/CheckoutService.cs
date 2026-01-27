using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.Checkout;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;
using static ByteBuy.Core.Specification.PortalUserSpecifications;

namespace ByteBuy.Core.Services;

public class CheckoutService : ICheckoutService
{
    private readonly IPortalUserRepository _portalUserRepository;
    public CheckoutService(IPortalUserRepository portalUserRepository)
    {
        _portalUserRepository = portalUserRepository;
    }

    public async Task<Result<CheckoutResponse>> GetCheckout(Guid userId, CancellationToken ct)
    {
        var spec = new PortalUserToUserBasicInfoResponseSpec(userId);
        var userData = await _portalUserRepository.GetBySpecAsync(spec, ct);

        if (userData is null)
            return Result.Failure<CheckoutResponse>(CommonUserErrors.NotFound);


    }
}

