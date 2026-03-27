using ByteBuy.Core.Domain.Base;
using ByteBuy.Core.Domain.Rentals.Enums;
using ByteBuy.Core.Domain.Rentals.Errors;
using ByteBuy.Core.Domain.Shared.ResultTypes;
using ByteBuy.Core.Domain.Shared.ValueObjects;
using ByteBuy.Core.Domain.Users;

namespace ByteBuy.Core.Domain.Rentals;

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

public sealed class Rental : AggregateRoot, ISoftDeletable
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
        RentOrderLineId = rentOrderLineId;
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

        var lenderSnapshotCopy = lender.Copy();

        var rental = new Rental(
            rentOrderLineId,
            borrowerId,
            thumbnailResult.Value,
            itemName,
            quantity,
            lenderSnapshotCopy,
            moneyResult.Value,
            rentalDays,
            deliveryDate)
        {
            RentalStartDate = rentalStartDate,
            RentalEndDate = rentalEndDate
        };

        return rental;
    }

    //this is intended to be used only by background job
    public Result ActivateRental()
       => ChangeStatus(RentalStatus.Active);

    // this is intended to be used only by background job
    public Result MarkAsOverdue()
        => ChangeStatus(RentalStatus.Overdue);

    public Result ReturnRental()
    {
        var statusResult = ChangeStatus(RentalStatus.Completed);
        if (statusResult.IsFailure)
            return statusResult;

        ReturnedDate = DateTime.UtcNow;

        return Result.Success();
    }

    private bool CanChangeStatus(RentalStatus newStatus)
    {
        return RentalStatusTransitions.AllowedTransitions.TryGetValue(Status, out var allowedStatus)
               && allowedStatus.Contains(newStatus);
    }

    public Result ChangeStatus(RentalStatus newStatus)
    {
        if (!CanChangeStatus(newStatus))
        {
            return Result.Failure(Error.Validation("Rental.OrderStatus", $"Cannot change status from {Status} to {newStatus}"));
        }

        Status = newStatus;
        DateEdited = DateTime.UtcNow;

        return Result.Success();
    }
}
