using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Condition;

namespace ByteBuy.Core.Mappings;

public static class ConditionMappings
{
    public static ConditionResponse ToConditionResponse(this Condition condition)
        => new ConditionResponse(condition.Id, condition.Name, condition.Description);

    public static SelectListItemResponse ToSelectListItemResponse(this Condition condition)
        => new SelectListItemResponse(condition.Id, condition.Name);
}
