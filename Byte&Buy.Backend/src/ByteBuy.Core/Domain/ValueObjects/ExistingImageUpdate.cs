namespace ByteBuy.Core.Domain.ValueObjects;

public record ExistingImageUpdate(Guid Id, string? AltText, bool IsDeleted);
