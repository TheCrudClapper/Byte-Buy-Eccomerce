using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Address;
using ByteBuy.Core.DTO.Public.AddressValueObj;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers.Company;

[Resource("user-addresses-admin")]
[Route("api/company/users/{userId:guid}")]
[ApiController]
public class CompanyUserAddressesController : BaseApiController
{
    private readonly IAddressService _addressService;

    public CompanyUserAddressesController(IAddressService addressService)
        => _addressService = addressService;

    [HttpPut("home-address")]
    public async Task<ActionResult<UpdatedResponse>> PutHomeAddresAsync(Guid userId, HomeAddressDto request)
        => HandleResult(await _addressService.SetHomeUserAddress(userId, request));

    [HttpPost("shipping-addresses")]
    public async Task<ActionResult<CreatedResponse>> PostAsync(Guid userId, ShippingAddressAddRequest request)
        => HandleResult(await _addressService.AddUserShippingAddressAsync(userId, request));

    [HttpPut("shipping-addresses/{addressId:guid}")]
    public async Task<ActionResult<UpdatedResponse>> PutAsync(Guid userId, Guid addressId, ShippingAddressUpdateRequest request)
      => HandleResult(await _addressService.UpdateUserShippingAddressAsync(addressId, userId, request));

    [HttpGet("shipping-addresses")]
    public async Task<ActionResult<IReadOnlyCollection<ShippingAddressResponse>>> GetAllAsync(Guid userId, CancellationToken ct)
        => HandleResult(await _addressService.GetUserShippingAddressesAsync(userId, ct));

    [HttpGet("shipping-addresses/{addressId:guid}")]
    public async Task<ActionResult<ShippingAddressResponse>> GetAsync(Guid userId, Guid addressId, CancellationToken ct)
        => HandleResult(await _addressService.GetUserShippingAddressAsync(userId, addressId, ct));

    [HttpDelete("shipping-addresses/{addressId:guid}")]
    public async Task<ActionResult> DeleteAsync(Guid userId, Guid addressId)
        => HandleResult(await _addressService.DeleteUserShippingAddressAsync(userId, addressId));
}
