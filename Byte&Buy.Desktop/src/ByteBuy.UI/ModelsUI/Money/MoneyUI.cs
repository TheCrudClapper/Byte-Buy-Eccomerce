namespace ByteBuy.UI.ModelsUI.Money;

public sealed record MoneyUI
{
    public decimal Amount { get; set; }
    public string Currency { get; set; } = null!;
}
