using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.AddressValueObj;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers.Me;

[Route("api/me/home-address")]
[ApiController]
public class UserHomeAddressController : BaseApiController
{
    private readonly IAddressService _addressService;
    public UserHomeAddressController(IAddressService addressService)
       => _addressService = addressService;

    [HttpGet]
    public async Task<ActionResult<HomeAddressDto>> GetHomeAddressAsync(CancellationToken ct)
        => HandleResult(await _addressService.GetUserHomeAddressAsync(CurrentUserId, ct));

    [HttpPut]
    public async Task<ActionResult<UpdatedResponse>> PutHomeAddresAsync(HomeAddressDto request)
       => HandleResult(await _addressService.SetHomeUserAddressAsync(CurrentUserId, request));
}
