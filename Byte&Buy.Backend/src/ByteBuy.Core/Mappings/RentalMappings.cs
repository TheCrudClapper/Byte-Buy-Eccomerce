using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.Public.ImageThumbnail;
using ByteBuy.Core.DTO.Public.Money;
using ByteBuy.Core.DTO.Public.Rental;
using System.Linq.Expressions;

namespace ByteBuy.Core.Mappings;

public static class RentalMappings
{
    public static Expression<Func<Rental, RentalLenderResponse>> UserRentalLenderResponseProjection
        => r => new RentalLenderResponse(
            r.Id,
            r.Status,
            r.ItemName,
            r.Quantity,
            new MoneyDto(r.PricePerDay.Amount * r.RentalDays * r.Quantity, r.PricePerDay.Currency),
            new ImageThumbnailDto(r.Thumbnail.ImagePath, r.Thumbnail.AltText),
            r.DateCreated,
            r.RentalDays,
            r.Borrower.FirstName + " " + r.Borrower.LastName,
            r.Borrower.Email!,
            r.RentalStartDate,
            r.RentalEndDate!.Value);

    public static Expression<Func<Rental, UserRentalBorrowerResponse>> UserRentalBorrowerResponseProjection
        => r => new UserRentalBorrowerResponse(
            r.Id,
            r.Status,
            r.ItemName,
            r.Quantity,
            new MoneyDto(r.PricePerDay.Amount * r.RentalDays * r.Quantity, r.PricePerDay.Currency),
            new ImageThumbnailDto(r.Thumbnail.ImagePath, r.Thumbnail.AltText),
            r.DateCreated,
            r.RentalDays,
            r.Lender.DisplayName,
            r.RentalStartDate,
            r.RentalEndDate!.Value);

    public static Expression<Func<Rental, CompanyRentalLenderListResponse>> CompanyRentalLenderResponseProjection
      => r => new CompanyRentalLenderListResponse(
          r.Id,
          r.Status,
          r.ItemName,
          r.Quantity,
          r.RentalDays,
          r.Borrower.Email!,
          r.RentalStartDate,
          r.RentalEndDate!.Value);
}
