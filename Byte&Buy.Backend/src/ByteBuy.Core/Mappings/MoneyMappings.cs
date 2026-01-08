using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.DTO.Money;

namespace ByteBuy.Core.Mappings;

public static class MoneyMappings
{
    public static MoneyDto ToMoneyDto(this Money money)
        => new(money.Amount, money.Currency);
}
