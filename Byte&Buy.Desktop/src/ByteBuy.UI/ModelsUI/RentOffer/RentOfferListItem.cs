using ByteBuy.UI.ModelsUI.Abstractions;
using System;

namespace ByteBuy.UI.ModelsUI.RentOffer;

public class RentOfferListItem : IListItem
{
    public int RowNumber { get; set; }
    public Guid Id { get; init; }
    public string ItemName { get; set; } = string.Empty;
    public int QuantityAvailable { get; set; }
    public string CreatorEmail { get; set; } = string.Empty;
    public string PriceAndCurrencyPerDay { get; set; } = string.Empty;
    public int MaxRentalDays { get; set; }
}
