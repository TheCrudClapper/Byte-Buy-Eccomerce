using ByteBuy.Services.DTO.Category;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface ICategoryService : IBaseService
{
    Task<Result<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectList();
    Task<Result<CreatedResponse>> Add(CategoryAddRequest request);
    Task<Result<UpdatedResponse>> Update(Guid id, CategoryUpdateRequest request);
    Task<Result<CategoryResponse>> GetById(Guid id);
    Task<Result<PagedList<CategoryListResponse>>> GetList(CategoryListQuery query);
}
