using ByteBuy.Services.DTO.Address;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface IHomeAddressService
{
    Task<Result<UpdatedResponse>> SetHomeAddressAsync(Guid userId, HomeAddressDto request);
}
