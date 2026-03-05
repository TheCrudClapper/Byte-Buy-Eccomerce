using ByteBuy.Services.DTO.Condition;
using ByteBuy.UI.ViewModels.Condition;

namespace ByteBuy.UI.Mappings;

public static class ConditionMappings
{
    public static ConditionListItemViewModel ToListItem(this ConditionListResponse response, int index)
    {
        return new ConditionListItemViewModel
        {
            RowNumber = index,
            Id = response.Id,
            Name = response.Name,
        };
    }
}
