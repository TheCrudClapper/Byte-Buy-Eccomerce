using ByteBuy.Services.DTO.Address;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.InfraContracts.HttpClients;

public interface ICompanyUserHomeAddressHttpClient
{
    Task<Result<UpdatedResponse>> PutUserHomeAddressAsync(Guid userId, HomeAddressDto request);
}
