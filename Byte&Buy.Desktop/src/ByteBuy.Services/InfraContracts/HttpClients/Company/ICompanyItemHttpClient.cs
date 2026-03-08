using ByteBuy.Services.DTO.Item;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.InfraContracts.HttpClients.Company;

public interface ICompanyItemHttpClient
{
    Task<Result<CreatedResponse>> PostCompanyItemAsync(MultipartContent request);
    Task<Result<UpdatedResponse>> PutCompanyItemAsync(Guid id, MultipartContent request);
    Task<Result<PagedList<ItemListResponse>>> GetListAsync(ItemListQuery query);
    Task<Result> DeleteCompanyItemAsync(Guid id);
    Task<Result<ItemResponse>> GetByIdAsync(Guid id);
}
