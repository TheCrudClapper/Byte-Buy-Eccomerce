using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Address;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers.Me;

[Resource("user-shipping-addresses")]
[Route("api/me/shipping-addresses")]
[ApiController]
public class UserShippingAddressesController : BaseApiController
{
    private readonly IAddressService _addressService;
    public UserShippingAddressesController(IAddressService addressService)
       => _addressService = addressService;

    [HttpPost]
    [HasPermission("user-shipping-addresses:create:one")]
    public async Task<ActionResult<CreatedResponse>> PostShippingAddress(ShippingAddressAddRequest request)
        => HandleResult(await _addressService.AddUserShippingAddressAsync(CurrentUserId, request));

    [HttpPut("{addressId:guid}")]
    [HasPermission("user-shipping-addresses:update:one")]
    public async Task<ActionResult<UpdatedResponse>> PutShippingAddress(Guid addressId, ShippingAddressUpdateRequest request)
        => HandleResult(await _addressService.UpdateUserShippingAddressAsync(addressId, CurrentUserId, request));

    [HttpGet("list")]
    [HasPermission("user-shipping-addresses:read:many")]
    public async Task<ActionResult<ShippingAddressListResponse>> GetShippingAddressesList(CancellationToken ct)
        => HandleResult(await _addressService.GetShippingAddressesList(CurrentUserId, ct));

    [HttpGet("{addressId:guid}")]
    [HasPermission("user-shipping-addresses:read:one")]
    public async Task<ActionResult<ShippingAddressResponse>> GetUserShippingAddress(Guid addressId, CancellationToken ct)
        => HandleResult(await _addressService.GetUserShippingAddressAsync(CurrentUserId, addressId, ct));

    [HttpDelete("{addressId:guid}")]
    [HasPermission("user-shipping-addresses:delete:one")]
    public async Task<ActionResult> DeleteUserShippingAddress(Guid addressId)
        => HandleResult(await _addressService.DeleteUserShippingAddressAsync(addressId, CurrentUserId));

    [HttpGet("checkout/{addressId?}")]
    [HasPermission("user-shipping-addresses:read:checkout")]
    public async Task<ActionResult<ShippingAddressCheckout>> GetCheckoutAddress(CancellationToken ct, Guid? addressId = null)
        => HandleResult(await _addressService.GetCheckoutAddress(addressId, CurrentUserId, ct));
}
