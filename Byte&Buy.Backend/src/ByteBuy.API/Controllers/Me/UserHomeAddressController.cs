using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.AddressValueObj;
using ByteBuy.Core.DTO.Public.Offer.Common;
using ByteBuy.Core.ServiceContracts;

namespace ByteBuy.API.Controllers.Me;

[Resource("user-home-address")]
[Route("api/me/home-address")]
[ApiController]
public class UserHomeAddressController : BaseApiController
{
    private readonly IAddressService _addressService;
    public UserHomeAddressController(IAddressService addressService)
       => _addressService = addressService;

    [HttpGet]
    [HasPermission("{resource}:read:one")]
    public async Task<ActionResult<HomeAddressDto>> GetHomeAddressAsync(CancellationToken ct)
        => HandleResult(await _addressService.GetHomeAddressAsync(CurrentUserId, ct));

    [HttpPut]
    [HasPermission("{resource}:update:one")]
    public async Task<ActionResult<UpdatedResponse>> PutHomeAddresAsync(HomeAddressDto request)
       => HandleResult(await _addressService.SetHomeAddressAsync(CurrentUserId, request));

    [HttpGet("offer")]
    public async Task<ActionResult<OfferAddressResponse?>> GetHomeAddresForOffer(CancellationToken ct)
        => HandleResult(await _addressService.GetHomeAddressForOfferAsync(CurrentUserId, ct));
}
