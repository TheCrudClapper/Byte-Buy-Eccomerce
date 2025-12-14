using ByteBuy.Core.DTO.Item;
using ByteBuy.Services.DTO.Item;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.InfraContracts.HttpClients;

public interface IItemHttpClient
{
    Task<Result<CreatedResponse>> PostCompanyItem(MultipartContent request);
    Task<Result<UpdatedResponse>> PutCompanyItem(Guid id, ItemUpdateRequest request);
    Task<Result> DeleteCompanyItem(Guid id);
}
