using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Address;
using ByteBuy.Core.DTO.PortalUser;
using ByteBuy.Core.DTO.Shared;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Route("api/users/{userId:guid}/addresses")]
[ApiController]
public class UserAddressesAdminController : BaseApiController
{
    private readonly IAddressService _addressService;

    public UserAddressesAdminController(IAddressService addressService)
        => _addressService = addressService;

    [HttpPost]
    public async Task<ActionResult<CreatedResponse>> PostAsync(Guid userId, AddressAddRequest request)
        => HandleResult(await _addressService.AddAsync(userId, request));

    [HttpPut("{addressId:guid}")]
    public async Task<ActionResult<UpdatedResponse>> PutAsync(Guid userId, Guid addressId, AddressUpdateRequest request)
      => HandleResult(await _addressService.UpdateAsync(addressId, userId, request));

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<AddressResponse>>> GetAllAsync(Guid userId, CancellationToken ct)
        => HandleResult(await _addressService.GetUserAddressesAsync(userId, ct));

    [HttpGet("{addressId:guid}")]
    public async Task<ActionResult<AddressResponse>> GetAsync(Guid userId, Guid addressId, CancellationToken ct)
        => HandleResult(await _addressService.GetUserAddressAsync(userId, addressId, ct));

    [HttpDelete("{addressId:guid}")]
    public async Task<ActionResult> DeleteAsync(Guid userId, Guid addressId)
        => HandleResult(await _addressService.DeleteUserAddressAsync(userId, addressId));
}
