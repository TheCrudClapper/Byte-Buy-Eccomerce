using ByteBuy.Core.Domain.Shared.ResultTypes;

namespace ByteBuy.Core.Domain.Shared.Errors;

public static class MoneyErrors
{
    public static readonly Error AmountInvalid =
        Error.Validation("Money.Amount", "Amount of can't 0 or negative");

    public static readonly Error EmptyCurrency =
        Error.Validation("Money.Currency", "Currency cannot be empty");

    public static readonly Error CurrencyInvalid =
        Error.Validation("Money.Currency", "Currency must consist of 3 characters");
}
