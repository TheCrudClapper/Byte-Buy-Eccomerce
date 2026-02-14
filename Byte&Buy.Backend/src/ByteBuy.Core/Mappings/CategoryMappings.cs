using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.Public.Category;
using ByteBuy.Core.DTO.Public.Shared;
using System.Linq.Expressions;

namespace ByteBuy.Core.Mappings;

public static class CategoryMappings
{
    public static CategoryResponse ToCategoryResponse(this Category category)
        => new CategoryResponse(category.Id, category.Name, category.Description);

    public static SelectListItemResponse<Guid> ToSelectListItemResponse(this Category category)
        => new SelectListItemResponse<Guid>(category.Id, category.Name);

    public static Expression<Func<Category, CategoryListResponse>> CategoryListProjection
        => c => new CategoryListResponse(c.Id, c.Name);
}
