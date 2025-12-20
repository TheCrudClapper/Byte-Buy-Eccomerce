using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Address;
using ByteBuy.Core.DTO.PortalUser;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AddressController : BaseApiController
{
    private readonly IAddressService _addressService;
    public AddressController(IAddressService addressService)
       => _addressService = addressService;


    [HttpPost("user")]
    public async Task<ActionResult<CreatedResponse>> PostAddress(AddressAddRequest request)
        => HandleResult(await _addressService.AddAsync(CurrentUserId, request));

    [HttpPut("user/{addressId}")]
    public async Task<ActionResult<UpdatedResponse>> PutAddress(Guid addressId, AddressUpdateRequest request)
        => HandleResult(await _addressService.UpdateAsync(addressId, CurrentUserId, request));

    [HttpGet]
    public async Task<ActionResult<AddressResponse>> GetAddress(Guid addressId, CancellationToken ct)
        => HandleResult(await _addressService.GetByIdAsync(addressId, ct));

    [HttpGet("user")]
    public async Task<ActionResult<IEnumerable<AddressResponse>>> GetUserAdresses(CancellationToken ct)
        => HandleResult(await _addressService.GetUserAddressesAsync(CurrentUserId, ct));

    [HttpGet("user/{addressId}")]
    public async Task<ActionResult<AddressResponse>> GetUserAddress(Guid addressId, CancellationToken ct)
        => HandleResult(await _addressService.GetUserAddressAsync(addressId, CurrentUserId, ct));

    [HttpDelete("user/{addressId}")]
    public async Task<ActionResult> DeleteUserAddress(Guid addressId)
        => HandleResult(await _addressService.DeleteUserAddressAsync(addressId, CurrentUserId));

}
