using ByteBuy.Services.DTO.Condition;
using ByteBuy.UI.ModelsUI.Condition;

namespace ByteBuy.UI.Mappings;

public static class ConditionMappings
{
    public static ConditionListItem ToListItem(this ConditionListResponse response, int index)
    {
        return new ConditionListItem
        {
            RowNumber = index,
            Id = response.Id,
            Name = response.Name,
        };
    }
}
