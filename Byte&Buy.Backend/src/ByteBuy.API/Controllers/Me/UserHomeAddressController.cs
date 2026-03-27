using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.AddressValueObj;
using ByteBuy.Core.DTO.Public.Offer.Common;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

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
    [HasPermission("user-home-address:read:one")]
    public async Task<ActionResult<HomeAddressDto>> GetHomeAddressAsync(CancellationToken ct)
        => HandleResult(await _addressService.GetHomeAddressAsync(CurrentUserId, ct));

    [HttpPut]
    [HasPermission("user-home-address:update:one")]
    public async Task<ActionResult<UpdatedResponse>> PutHomeAddresAsync(HomeAddressDto request)
       => HandleResult(await _addressService.SetHomeAddressAsync(CurrentUserId, request));

    [HttpGet("offer")]
    public async Task<ActionResult<OfferAddressResponse?>> GetHomeAddresForOffer(CancellationToken ct)
        => HandleResult(await _addressService.GetHomeAddressForOfferAsync(CurrentUserId, ct));
}
