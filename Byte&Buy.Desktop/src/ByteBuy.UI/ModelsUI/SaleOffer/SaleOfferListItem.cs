using ByteBuy.UI.ModelsUI.Abstractions;
using System;

namespace ByteBuy.UI.ModelsUI.SaleOffer;

public class SaleOfferListItem : IListItem
{
    public int RowNumber { get; set; }
    public Guid Id { get; init; }
    public string ItemName { get; set;  } = string.Empty;
    public int QuantityAvailable { get; set; }
    public string CreatedByName { get; set; } = string.Empty;
    public string PriceAndCurrencyPerItem { get; set; } = string.Empty;
}
