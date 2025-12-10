using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.ValueObjects;

public sealed class Money
{
    public decimal Amount { get; private set; } = 0;
    public string Currency { get; private set; } = "PLN";

    private Money() { }

    private Money(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public static Result<Money> Create(decimal amount, string currency)
    {
        if (amount < 1)
            return Result.Failure<Money>(Error.Validation("Amount of money can't 0 or negative"));

        if (string.IsNullOrWhiteSpace(currency))
            return Result.Failure<Money>(Error.Validation("Currency can't be null"));

        if (currency.Length > 3)
            return Result.Failure<Money>(Error.Validation("Currency must consist of 3 characters"));

        return new Money(amount, currency.ToUpper());
    }

    public static Money Zero => new(0, "PLN");

    public override bool Equals(object? obj)
    {
        if (obj is null || obj is not Money)
            return false;

        Money other = (Money)obj;

        if (Currency != other.Currency)
            return false;

        if (Amount != other.Amount)
            return false;

        return true;
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(Amount, Currency);
    }
}
