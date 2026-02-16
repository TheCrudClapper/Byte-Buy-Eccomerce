using ByteBuy.Services.DTO.Money;

namespace ByteBuy.UI.ViewModels.Shared;

public sealed class MoneyViewModel
{
    public string Display { get; }
    public MoneyViewModel(MoneyDto dto)
    {
        Display = $"{dto.Amount:0.00} {dto.Currency}";
    }
}
