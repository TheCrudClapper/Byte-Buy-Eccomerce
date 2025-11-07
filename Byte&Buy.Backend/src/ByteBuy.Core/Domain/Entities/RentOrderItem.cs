namespace ByteBuy.Core.Domain.Entities;

public class RentOrderItem : OrderItem
{
    public DateTime RentalStartDate { get; set; }
    public DateTime RentalEndDate { get; set; }
}
