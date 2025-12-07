using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Address;
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
        => HandleResult(await _addressService.AddAddress(CurrentUserId, request));

    [HttpPut("user/{addressId}")]
    public async Task<ActionResult<UpdatedResponse>> PutAddress(Guid addressId, AddressUpdateRequest request)
        => HandleResult(await _addressService.UpdateAddress(addressId, CurrentUserId, request));

    [HttpGet]
    public async Task<ActionResult<Core.DTO.PortalUser.AddressResponse>> GetAddress(Guid addressId, CancellationToken ct)
        => HandleResult(await _addressService.GetAddress(addressId, ct));

    [HttpGet("user")]
    public async Task<ActionResult<IEnumerable<Core.DTO.PortalUser.AddressResponse>>> GetUserAdresses(CancellationToken ct)
        => HandleResult(await _addressService.GetUserAddresses(CurrentUserId, ct));

    [HttpGet("user/{addressId}")]
    public async Task<ActionResult<Core.DTO.PortalUser.AddressResponse>> GetUserAddress(Guid addressId, CancellationToken ct)
        => HandleResult(await _addressService.GetUserAddress(addressId, CurrentUserId, ct));

    [HttpDelete("user/{addressId}")]
    public async Task<ActionResult> DeleteUserAddress(Guid addressId)
        => HandleResult(await _addressService.DeleteUserAddress(addressId, CurrentUserId));

}
