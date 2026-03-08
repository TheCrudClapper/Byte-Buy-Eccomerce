using ByteBuy.Services.DTO.Item;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.InfraContracts.HttpClients.Company;

public interface ICompanyItemHttpClient
{
    Task<Result<CreatedResponse>> PostCompanyItem(MultipartContent request);
    Task<Result<UpdatedResponse>> PutCompanyItem(Guid id, MultipartContent request);
    Task<Result<PagedList<ItemListResponse>>> GetListAsync(ItemListQuery query);
    Task<Result> DeleteCompanyItem(Guid id);
    Task<Result<ItemResponse>> GetByIdAsync(Guid id);
}
