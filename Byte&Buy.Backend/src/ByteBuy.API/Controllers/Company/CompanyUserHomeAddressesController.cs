using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.AddressValueObj;
using ByteBuy.Core.ServiceContracts;

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
    [HasPermission("{resource}:update:one")]
    public async Task<ActionResult<UpdatedResponse>> PutHomeAddresAsync(Guid userId, HomeAddressDto request)
       => HandleResult(await _addressService.SetHomeAddressAsync(userId, request));
}
