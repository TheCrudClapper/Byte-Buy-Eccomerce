namespace ByteBuy.Core.Domain.Entities;

public sealed class CardPaymentDetails : PaymentDetails
{
    public string MaskedCardNumber { get; set; } = null!;
    public string CardHolderName { get; set; } = null!;

    private CardPaymentDetails() { }
}
