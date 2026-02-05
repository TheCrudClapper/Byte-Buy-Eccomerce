using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Entities;

public static class RentalStatusTransitions
{
    public static Dictionary<RentalStatus, RentalStatus[]> AllowedTransitions = new()
    {
        { RentalStatus.Created, [ RentalStatus.Active ] },
        { RentalStatus.Active, [ RentalStatus.Overdue, RentalStatus.Completed ] },
        { RentalStatus.Overdue, [ RentalStatus.Completed] },
        { RentalStatus.Completed, [] },

    };
}

public sealed class Rental : AuditableEntity, ISoftDeletable
{
    public Guid RentOrderLineId { get; private set; }
    public Guid BorrowerId { get; private set; }
    public ImageThumbnail Thumbnail { get; private set; } = null!;
    public string ItemName { get; private set; } = null!;
    public int Quantity { get; private set; }
    public SellerSnapshot Lender { get; private set; } = null!;
    public RentalStatus Status { get; private set; }
    public Money PricePerDay { get; private set; } = null!;
    public int RentalDays { get; private set; }
    public DateTime DeliveryDate { get; private set; }
    public DateTime RentalStartDate { get; private set; }
    public DateTime? RentalEndDate { get; private set; }
    public DateTime? ReturnedDate { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime? DateDeleted { get; private set; }

    //Navigation EF
    public PortalUser Borrower { get; private set; } = null!;

    private Rental() { }

    private Rental(Guid rentOrderLineId,
        Guid borrowerId,
        ImageThumbnail thumbnail,
        string itemName,
        int quantity,
        SellerSnapshot lender,
        Money pricePerDay,
        int rentalDays,
        DateTime deliveryDate)
    {
        Status = RentalStatus.Created;
        Lender = lender;
        Thumbnail = thumbnail;
        ItemName = itemName;
        BorrowerId = borrowerId;
        Quantity = quantity;
        PricePerDay = pricePerDay;
        RentalDays = rentalDays;
        DeliveryDate = deliveryDate;
        IsActive = true;
        DateCreated = DateTime.UtcNow;
    }

    public static Result<Rental> CreateRental(
        Guid rentOrderLineId,
        Guid borrowerId,
        string imagePath,
        string? altText,
        decimal pricePerDayAmount,
        string pricePerDayCurrency,
        string itemName,
        int quantity,
        SellerSnapshot lender,
        Money pricePerDay,
        int rentalDays,
        DateTime deliveryDate)
    {
        if (quantity <= 0)
            return Result.Failure<Rental>(RentalErrors.QuantityInvalid);

        if (rentalDays <= 0)
            return Result.Failure<Rental>(RentalErrors.RentalDaysInvalid);

        var thumbnailResult = ImageThumbnail.Create(imagePath, altText);
        if (thumbnailResult.IsFailure)
            return Result.Failure<Rental>(thumbnailResult.Error);

        var moneyResult = Money.Create(pricePerDayAmount, pricePerDayCurrency);
        if (moneyResult.IsFailure)
            return Result.Failure<Rental>(moneyResult.Error);

        var rentalStartDate = deliveryDate.Date.AddDays(1);
        var rentalEndDate = rentalStartDate.AddDays(rentalDays);

        var rental = new Rental(
            rentOrderLineId,
            borrowerId,
            thumbnailResult.Value,
            itemName,
            quantity,
            lender,
            moneyResult.Value,
            rentalDays,
            deliveryDate);

        rental.RentalStartDate = rentalStartDate;
        rental.RentalEndDate = rentalEndDate;

        return rental;
    }
}
