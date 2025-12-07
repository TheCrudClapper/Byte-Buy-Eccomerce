using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Core.DTO;

namespace ByteBuy.Core.Mappings;

public static class AuditableEntityMappings
{
    public static UpdatedResponse ToUpdatedResponse(this AuditableEntity entity)
       => new UpdatedResponse(entity.Id, entity.DateEdited ?? DateTime.MinValue);

    public static CreatedResponse ToCreatedResponse(this AuditableEntity entity)
        => new CreatedResponse(entity.Id, entity.DateCreated);
}

