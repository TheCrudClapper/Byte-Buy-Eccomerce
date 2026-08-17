using ByteBuy.Core.Domain.Base;

namespace ByteBuy.Core.Mappings;

public static class EntityMappings
{
    public static CreatedResponse ToCreatedResponse(this Entity entity)
        => new(entity.Id, entity.DateCreated);

    public static UpdatedResponse ToUpdatedResponse(this Entity entity)
        => new(entity.Id, entity.DateEdited ?? DateTime.MinValue);
}

