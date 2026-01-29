namespace ByteBuy.Core.Domain.Entities;

public sealed class BlikPaymentDetails : PaymentDetails
{
    public string PhoneNumber { get; set; } = null!;

    private BlikPaymentDetails()
    {
        
    }

}
