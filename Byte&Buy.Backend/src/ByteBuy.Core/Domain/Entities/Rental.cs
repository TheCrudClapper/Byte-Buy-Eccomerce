using ByteBuy.Core.Domain.EntityContracts;

namespace ByteBuy.Core.Domain.Entities;

public class Rental : AuditableEntity, ISoftDelete
{
    public Guid RentOrderItemId { get; set; }
    public RentOrderItem RentOrderItem { get; set; } = null!;
    public DateTime RentalStartDate { get; set; }
    public DateTime RentalEndDate { get; set; }
    public DateTime? ReturnedDate { get; set; }
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }
}
