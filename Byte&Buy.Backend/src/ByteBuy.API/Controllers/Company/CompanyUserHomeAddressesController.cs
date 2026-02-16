using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.AddressValueObj;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers.Company;

[Route("api/users/home-address")]
[ApiController]
public class CompanyUserHomeAddressesController : BaseApiController
{
    private readonly IAddressService _addressService;

    public CompanyUserHomeAddressesController(IAddressService addressService)
        => _addressService = addressService;

    [HttpPut]
    public async Task<ActionResult<UpdatedResponse>> PutHomeAddresAsync(Guid userId, HomeAddressDto request)
       => HandleResult(await _addressService.SetHomeUserAddressAsync(userId, request));
}
