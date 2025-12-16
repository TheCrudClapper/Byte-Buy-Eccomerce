using ByteBuy.Services.DTO.Item;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.InfraContracts.HttpClients;

public interface IItemHttpClient
{
    Task<Result<CreatedResponse>> PostCompanyItem(MultipartContent request);
    Task<Result<UpdatedResponse>> PutCompanyItem(Guid id, MultipartContent request);
    Task<Result<IReadOnlyCollection<ItemListResponse>>> GetListAsync();
    Task<Result> DeleteCompanyItem(Guid id);
    Task<Result<ItemResponse>> GetByIdAsync(Guid id);
}
