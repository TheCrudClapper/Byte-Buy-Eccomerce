using ByteBuy.Core.Domain.Shared.ValueObjects;
using ByteBuy.Core.DTO.Public.Money;

namespace ByteBuy.Core.Mappings;

public static class MoneyMappings
{
    public static MoneyDto ToMoneyDto(this Money money)
        => new(money.Amount, money.Currency);
}
