using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.AddressValueObj;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers.Company;

[Resource("company-user-home-addresses")]
[Route("api/users/{userId:guid}/home-address")]
[ApiController]
public class CompanyUserHomeAddressesController : BaseApiController
{
    private readonly IAddressService _addressService;

    public CompanyUserHomeAddressesController(IAddressService addressService)
        => _addressService = addressService;

    [HttpPut]
    [HasPermission("company-user-home-addresses:update:one")]
    public async Task<ActionResult<UpdatedResponse>> PutHomeAddresAsync(Guid userId, HomeAddressDto request)
       => HandleResult(await _addressService.SetHomeUserAddressAsync(userId, request));
}
