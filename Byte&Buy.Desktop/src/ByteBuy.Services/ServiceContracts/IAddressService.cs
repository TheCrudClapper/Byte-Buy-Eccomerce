using ByteBuy.Services.DTO.Address;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface IAddressService
{
    Task<Result<CreatedResponse>> AddAsync(Guid userId, AddressAddRequest request);
    Task<Result<UpdatedResponse>> UpdateAsync(Guid userId, Guid addressId, AddressUpdateRequest request);
}
