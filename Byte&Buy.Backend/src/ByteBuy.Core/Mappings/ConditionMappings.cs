using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.Condition;
using ByteBuy.Core.DTO.Shared;

namespace ByteBuy.Core.Mappings;

public static class ConditionMappings
{
    public static ConditionResponse ToConditionResponse(this Condition condition)
        => new ConditionResponse(condition.Id, condition.Name, condition.Description);

    public static SelectListItemResponse<Guid> ToSelectListItemResponse(this Condition condition)
        => new SelectListItemResponse<Guid>(condition.Id, condition.Name);
}
