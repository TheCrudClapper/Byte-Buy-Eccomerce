namespace ByteBuy.Core.Domain.Shared.DomainModels;

public record ExistingImageUpdate(Guid Id, string? AltText, bool IsDeleted);
