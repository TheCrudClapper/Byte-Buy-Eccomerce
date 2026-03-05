using ByteBuy.UI.ViewModels.Base;
using System;

namespace ByteBuy.UI.ViewModels.SaleOffer;

public class SaleOfferListItemViewModel : IListItemViewModel
{
    public int RowNumber { get; set; }
    public Guid Id { get; init; }
    public string ItemName { get; set; } = string.Empty;
    public int QuantityAvailable { get; set; }
    public string CreatorEmail { get; set; } = string.Empty;
    public string PriceAndCurrencyPerItem { get; set; } = string.Empty;
}
