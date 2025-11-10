namespace ByteBuy.Core.Domain.Entities;

public class RentOrderItem : OrderItem
{
    public Rental Rental { get; set; } = null!;
    public DateTime RentalStartDate { get; set; }
    public DateTime RentalEndDate { get; set; }
}
