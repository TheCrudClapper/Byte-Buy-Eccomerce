using ByteBuy.Services.DTO.Category;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface ICategoryService : IBaseService
{
    Task<Result<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectListAsync();
    Task<Result<CreatedResponse>> AddAsync(CategoryAddRequest request);
    Task<Result<UpdatedResponse>> UpdateAsync(Guid id, CategoryUpdateRequest request);
    Task<Result<CategoryResponse>> GetByIdAsync(Guid id);
    Task<Result<PagedList<CategoryListResponse>>> GetListAsync(CategoryListQuery query);
}
