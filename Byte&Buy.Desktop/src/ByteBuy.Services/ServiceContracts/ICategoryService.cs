using ByteBuy.Services.DTO.Category;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface ICategoryService
{
    Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectList();
    Task<Result<CreatedResponse>> Add(CategoryAddRequest request);
    Task<Result<UpdatedResponse>> Update(Guid id, CategoryUpdateRequest request);
    Task<Result> DeleteById(Guid id);
    Task<Result<CategoryResponse>> GetById(Guid id);
    Task<Result<IEnumerable<CategoryListResponse>>> GetList();
}
