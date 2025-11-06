namespace ByteBuy.Core.Domain.Entities;

public class RentOrderItem : OrderItem
{
    public DateTime RentalStartDate { get; set; }
    public DateTime RentalEndDate { get; set; }
    public bool IsReturned { get; set; }
    public DateTime? DateReturned { get; set; }
}
