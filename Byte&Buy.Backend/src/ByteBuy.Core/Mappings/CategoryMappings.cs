using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Category;
using ByteBuy.Services.DTO.Category;

namespace ByteBuy.Core.Mappings;

public static class CategoryMappings
{
    public static CategoryResponse ToCategoryResponse(this Category category)
        => new CategoryResponse(category.Id, category.Name, category.Description);

    public static CategoryListResponse ToCategoryListResponse(this Category category)
        => new CategoryListResponse(category.Id, category.Name);

    public static SelectListItemResponse ToSelectListItemResponse(this Category category)
        => new SelectListItemResponse(category.Id, category.Name);
}
