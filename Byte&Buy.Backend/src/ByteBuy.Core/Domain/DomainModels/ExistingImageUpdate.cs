namespace ByteBuy.Core.Domain.DomainModels;

public record ExistingImageUpdate(Guid Id, string? AltText, bool IsDeleted);
