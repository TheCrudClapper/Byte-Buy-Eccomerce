using ByteBuy.Core.DTO.Public.Money;

namespace ByteBuy.Core.Helpers;

public class MoneyHelper
{
    public const string DefaultCurrency = "PLN";

    public static MoneyDto Sum(IEnumerable<MoneyDto> monies)
    {
        var list = monies.Where(m => m is not null).ToList();

        if (list.Count == 0)
            return new MoneyDto(0, DefaultCurrency);

        var currency = list.First().Currency;

        if (list.Any(m => m.Currency != currency))
            throw new InvalidOperationException("Cannot sum different currencies");

        return new MoneyDto(
            list.Sum(m => m.Amount),
            currency
        );
    }

    public static MoneyDto From(decimal amount, MoneyDto dto)
    {
        return new MoneyDto(amount, dto.Currency);
    }
}
