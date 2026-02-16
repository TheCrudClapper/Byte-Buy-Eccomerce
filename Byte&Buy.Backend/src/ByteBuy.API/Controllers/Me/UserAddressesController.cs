using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Address;
using ByteBuy.Core.DTO.Public.AddressValueObj;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers.Me;

[Resource("my-addresses")]
[Route("api/me")]
[ApiController]
public class UserAddressesController : BaseApiController
{
    private readonly IAddressService _addressService;
    public UserAddressesController(IAddressService addressService)
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
        => HandleResult(await _addressService.GetUserShippingAddressAsync(CurrentUserId, addressId, ct));

    [HttpDelete("shipping-addresses/{addressId:guid}")]
    public async Task<ActionResult> DeleteUserShippingAddress(Guid addressId)
        => HandleResult(await _addressService.DeleteUserShippingAddressAsync(addressId, CurrentUserId));

    [HttpGet("shipping-addresses/checkout/{addressId?}")]
    public async Task<ActionResult<ShippingAddressCheckout>> GetCheckoutAddress(CancellationToken ct, Guid? addressId = null)
        => HandleResult(await _addressService.GetCheckoutAddress(addressId, CurrentUserId, ct));
}
