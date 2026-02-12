using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.Public.Condition;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Pagination;

namespace ByteBuy.Core.Mappings;

public static class ConditionMappings
{
    public static ConditionResponse ToConditionResponse(this Condition condition)
        => new ConditionResponse(condition.Id, condition.Name, condition.Description);

    public static SelectListItemResponse<Guid> ToSelectListItemResponse(this Condition condition)
        => new SelectListItemResponse<Guid>(condition.Id, condition.Name);

}
