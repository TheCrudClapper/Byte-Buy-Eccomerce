using ByteBuy.Services.DTO.Category;
using ByteBuy.UI.ViewModels.Category;

namespace ByteBuy.UI.Mappings;

public static class CategoryMappings
{
    public static CategoryListItemViewModel ToListItem(this CategoryListResponse response, int index)
    {
        return new CategoryListItemViewModel
        {
            Name = response.Name,
            Id = response.Id,
            RowNumber = index
        };
    }
}
