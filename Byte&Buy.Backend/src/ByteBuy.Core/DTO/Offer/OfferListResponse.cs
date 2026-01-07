namespace ByteBuy.Core.DTO.Offer;

public record OfferListResponse(Guid Id,
    string Title,
    string ConditionName,
    string CategoryName,
    int? MaxRentalDays,
    decimal? PricePerDay,
    decimal PricePerItem,
    string Currency,
    string City,
    string PostalCode,
    string PostalCity,
    bool IsCompanyOffer,
    bool IsSaleOffer
    );