namespace ByteBuy.Services.DTO.Money;

public sealed record MoneyDto
{
    public string Currency { get; init; }
    public decimal Amount { get; init; }
    public MoneyDto(decimal amount, string currency)
    {
        Currency = currency;
        Amount = decimal.Round(amount, 2, MidpointRounding.AwayFromZero);
    }
};