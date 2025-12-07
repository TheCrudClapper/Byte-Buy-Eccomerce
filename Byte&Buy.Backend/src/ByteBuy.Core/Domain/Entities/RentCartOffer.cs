namespace ByteBuy.Core.Domain.Entities;

public class RentCartOffer : CartOffer
{
    public DateTime RentalStartDate { get; set; }
    public DateTime RentalEndDate { get; set; }
}
