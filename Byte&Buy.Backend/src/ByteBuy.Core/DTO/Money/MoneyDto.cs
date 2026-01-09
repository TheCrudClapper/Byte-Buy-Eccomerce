namespace ByteBuy.Core.DTO.Money;

public class MoneyDto
{
    public string Currency { get; private set; }
    public decimal Amount { get; private set; }
    public MoneyDto(decimal amount, string currency)
    {
        Currency = currency;
        Amount = decimal.Round(amount, 2, MidpointRounding.AwayFromZero);
    }
};