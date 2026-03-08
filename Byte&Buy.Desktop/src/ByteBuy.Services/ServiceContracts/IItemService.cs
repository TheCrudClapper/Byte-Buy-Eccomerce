using ByteBuy.Core.DTO.Item;
using ByteBuy.Services.DTO.Item;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface IItemService : IBaseService
{
    Task<Result<CreatedResponse>> AddAsync(ItemAddRequest request);
    Task<Result<UpdatedResponse>> UpdateAsync(Guid id, ItemUpdateRequest request);
    Task<Result<ItemResponse>> GetByIdAsync(Guid id);
    Task<Result<PagedList<ItemListResponse>>> GetListAsync(ItemListQuery query);
}
