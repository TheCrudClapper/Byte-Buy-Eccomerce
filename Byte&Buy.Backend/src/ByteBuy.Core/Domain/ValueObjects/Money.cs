using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.ValueObjects;

public sealed class Money : IEquatable<Money>
{
    public decimal Amount { get; private set; } = 0;
    public string Currency { get; private set; } = "PLN";

    private Money() { }

    private Money(decimal amount, string currency)
    {
        Amount = decimal.Round(amount, 2, MidpointRounding.AwayFromZero);
        Currency = currency;
    }

    public static Result<Money> Create(decimal amount, string currency = "PLN")
    {
        if (amount < 1)
            return Result.Failure<Money>(MoneyErrors.AmountInvalid);

        if (string.IsNullOrWhiteSpace(currency))
            return Result.Failure<Money>(MoneyErrors.EmptyCurrency);

        if (currency.Length > 3)
            return Result.Failure<Money>(MoneyErrors.CurrencyInvalid);


        return new Money(amount, currency.ToUpper());
    }

    public static Money Zero => new(0m, "PLN");

    public static void EnusureSameCurrency(Money a, Money b)
    {
        if (!string.Equals(a.Currency, b.Currency, StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException("Cannot operate on different currencies !");
    }

    public static Money operator +(Money a, Money b)
    {
        EnusureSameCurrency(a, b);
        return new Money((a.Amount + b.Amount), a.Currency);
    }

    public static Money operator -(Money a, Money b)
    {
        EnusureSameCurrency(a, b);
        return new Money((a.Amount - b.Amount), a.Currency);
    }

    public static Money operator *(Money a, Money b)
        => new Money(a.Amount * b.Amount, a.Currency);

    public static Money operator *(Money a, int multiplier)
        => new Money(a.Amount * multiplier, a.Currency);

    public static Money operator *(int multiplier, Money a)
        => a * multiplier;

    public static Money operator *(Money a, decimal multiplier)
        => new Money(a.Amount * multiplier, a.Currency);

    public override bool Equals(object? obj)
        => Equals(obj as Money);

    public override int GetHashCode()
    {
        return HashCode
            .Combine(decimal.Round(Amount, 2, MidpointRounding.AwayFromZero), Currency);
    }

    public bool Equals(Money? other)
    {
        if (other is null)
            return false;

        if (!string.Equals(Currency, other.Currency, StringComparison.OrdinalIgnoreCase))
            return false;

        if (Amount != other.Amount)
            return false;

        return true;
    }
}
