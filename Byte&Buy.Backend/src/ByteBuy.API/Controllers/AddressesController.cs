using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Address;
using ByteBuy.Core.DTO.AddressValueObj;
using ByteBuy.Core.DTO.PortalUser;
using ByteBuy.Core.DTO.Shared;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Resource("my-addresses")]
[Route("api/me")]
[ApiController]
public class AddressesController : BaseApiController
{
    private readonly IAddressService _addressService;
    public AddressesController(IAddressService addressService)
       => _addressService = addressService;

    [HttpGet("home-address")]
    public async Task<ActionResult<HomeAddressDto>> GetHomeAddress(CancellationToken ct)
        => HandleResult(await _addressService.GetUserHomeAddress(CurrentUserId, ct));

    [HttpPut("home-address")]
    public async Task<ActionResult<UpdatedResponse>> PutHomeAddresAsync(HomeAddressDto request)
       => HandleResult(await _addressService.SetHomeUserAddress(CurrentUserId, request));

    [HttpPost("shipping-addresses")]
    public async Task<ActionResult<CreatedResponse>> PostShippingAddress(ShippingAddressAddRequest request)
        => HandleResult(await _addressService.AddUserShippingAddressAsync(CurrentUserId, request));

    [HttpPut("shipping-addresses/{addressId:guid}")]
    public async Task<ActionResult<UpdatedResponse>> PutShippingAddress(Guid addressId, ShippingAddressUpdateRequest request)
        => HandleResult(await _addressService.UpdateUserShippingAddressAsync(addressId, CurrentUserId, request));

    [HttpGet("shipping-addresses/list")]
    public async Task<ActionResult<ShippingAddressListResponse>> GetShippingAddressesList(CancellationToken ct)
        => HandleResult(await _addressService.GetShippingAddressesList(CurrentUserId, ct));

    [HttpGet("shipping-addresses")]
    public async Task<ActionResult<IReadOnlyCollection<ShippingAddressResponse>>> GetUserShippingAdresses(CancellationToken ct)
        => HandleResult(await _addressService.GetUserShippingAddressesAsync(CurrentUserId, ct));

    [HttpGet("shipping-addresses/{addressId:guid}")]
    public async Task<ActionResult<ShippingAddressResponse>> GetUserShippingAddress(Guid addressId, CancellationToken ct)
        => HandleResult(await _addressService.GetUserShippingAddressAsync(addressId, CurrentUserId, ct));

    [HttpDelete("shipping-addresses/{addressId:guid}")]
    public async Task<ActionResult> DeleteUserShippingAddress(Guid addressId)
        => HandleResult(await _addressService.DeleteUserShippingAddressAsync(addressId, CurrentUserId));

}
