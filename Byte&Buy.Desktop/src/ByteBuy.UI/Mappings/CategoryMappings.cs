using ByteBuy.Services.DTO.Category;
using ByteBuy.UI.ModelsUI.Category;

namespace ByteBuy.UI.Mappings;

public static class CategoryMappings
{
    public static CategoryListItem ToListItem(this CategoryListResponse response, int index)
    {
        return new CategoryListItem
        {
            Name = response.Name,
            Id = response.Id,
            RowNumber = index + 1
        };
    }
}
