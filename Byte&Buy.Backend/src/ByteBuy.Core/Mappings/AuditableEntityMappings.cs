using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Condition;

namespace ByteBuy.Core.Mappings;

public static class AuditableEntityMappings
{
    public static UpdatedResponse ToUpdatedResponse(this AuditableEntity entity)
       => new UpdatedResponse(entity.Id, entity.DateEdited ?? DateTime.MinValue);

    public static CreatedResponse ToCreatedResponse(this AuditableEntity entity)
        => new CreatedResponse(entity.Id, entity.DateCreated);

    public static ConditionListResponse ToConditionListResponse(this Condition condition)
        => new ConditionListResponse(condition.Id, condition.Name);
}

