using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Address;
using ByteBuy.Core.DTO.PortalUser;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Resource("addresses")]
[Route("api/users/addresses")]
[ApiController]
public class MyAddressesController : BaseApiController
{
    private readonly IAddressService _addressService;
    public MyAddressesController(IAddressService addressService)
       => _addressService = addressService;

    [HttpPost]
    public async Task<ActionResult<CreatedResponse>> PostAddress(AddressAddRequest request)
        => HandleResult(await _addressService.AddAsync(CurrentUserId, request));

    [HttpPut("{addressId:guid}")]
    public async Task<ActionResult<UpdatedResponse>> PutAddress(Guid addressId, AddressUpdateRequest request)
        => HandleResult(await _addressService.UpdateAsync(addressId, CurrentUserId, request));

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<AddressResponse>>> GetUserAdresses(CancellationToken ct)
        => HandleResult(await _addressService.GetUserAddressesAsync(CurrentUserId, ct));

    [HttpGet("{addressId:guid}")]
    public async Task<ActionResult<AddressResponse>> GetUserAddress(Guid addressId, CancellationToken ct)
        => HandleResult(await _addressService.GetUserAddressAsync(addressId, CurrentUserId, ct));

    [HttpDelete("{addressId:guid}")]
    public async Task<ActionResult> DeleteUserAddress(Guid addressId)
        => HandleResult(await _addressService.DeleteUserAddressAsync(addressId, CurrentUserId));

}
