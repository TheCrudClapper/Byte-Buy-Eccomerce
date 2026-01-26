using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.Domain.ValueObjects;

namespace ByteBuy.Core.Domain.Entities;

public class Rental : AuditableEntity, ISoftDeletable
{
    public Guid RentOrderLineId { get; set; }
    public Guid BorrowerId { get; set; }
    public Seller Lender { get; set; } = null!;
    public RentalStatus Status { get; set; }
    public Money PricePerDay { get; private set; } = null!;
    public int RentalDays { get; private set;  }
    public DateTime RentalStartDate { get; set; }
    public DateTime? RentalEndDate { get; set; }
    public DateTime? ReturnedDate { get; set; }
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }

    //Navigation EF
    public PortalUser Borrower { get; set; } = null!;
}
