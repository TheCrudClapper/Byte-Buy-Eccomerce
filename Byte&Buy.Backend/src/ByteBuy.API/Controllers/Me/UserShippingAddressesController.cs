using ByteBuy.Core.DTO.Public.Address;

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
    [HasPermission("{resource}:create:one")]
    public async Task<ActionResult<CreatedResponse>> PostShippingAddressAsync(ShippingAddressAddRequest request)
        => HandleResult(await _addressService.AddShippingAddressAsync(CurrentUserId, request));

    [HttpPut("{addressId:guid}")]
    [HasPermission("{resource}:update:one")]
    public async Task<ActionResult<UpdatedResponse>> PutShippingAddressAsync(Guid addressId, ShippingAddressUpdateRequest request)
        => HandleResult(await _addressService.UpdateShippingAddressAsync(addressId, CurrentUserId, request));

    [HttpGet("list")]
    [HasPermission("{resource}:read:many")]
    public async Task<ActionResult<ShippingAddressListResponse>> GetShippingAddressesListAsync(CancellationToken ct)
        => HandleResult(await _addressService.GetShippingAddressesListAsync(CurrentUserId, ct));

    [HttpGet("{addressId:guid}")]
    [HasPermission("{resource}:read:one")]
    public async Task<ActionResult<ShippingAddressResponse>> GetUserShippingAddressAsync(Guid addressId, CancellationToken ct)
        => HandleResult(await _addressService.GetShippingAddressAsync(CurrentUserId, addressId, ct));

    [HttpDelete("{addressId:guid}")]
    [HasPermission("{resource}:delete:one")]
    public async Task<ActionResult> DeleteUserShippingAddress(Guid addressId)
        => HandleResult(await _addressService.DeleteShippingAddressAsync(addressId, CurrentUserId));

    [HttpGet("checkout/{addressId?}")]
    [HasPermission("{resource}:read:checkout")]
    public async Task<ActionResult<ShippingAddressCheckout>> GetCheckoutAddressAsync(CancellationToken ct, Guid? addressId = null)
        => HandleResult(await _addressService.GetCheckoutAddressAsync(addressId, CurrentUserId, ct));
}
