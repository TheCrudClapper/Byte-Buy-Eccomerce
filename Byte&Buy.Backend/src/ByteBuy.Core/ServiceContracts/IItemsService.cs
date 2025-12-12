using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Item;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface IItemsService
{
    Task<Result<CreatedResponse>> AddCompanyItem(ItemAddRequest request);
    Task<Result<UpdatedResponse>> UpdateCompanyItem(Guid itemId, ItemUpdateRequest request);
    Task<Result<IReadOnlyCollection<ItemListResponse>>> GetCompanyItemsList(CancellationToken ct = default);
    Task<Result<ItemResponse>> GetCompanyItem(Guid itemId, CancellationToken ct = default);
    Task<Result> DeleteCompanyItem(Guid itemId);
}
