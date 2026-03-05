using ByteBuy.UI.ViewModels.Base;
using System;

namespace ByteBuy.UI.ViewModels.RentOffer;

public class RentOfferListItemViewModel : IListItemViewModel
{
    public int RowNumber { get; set; }
    public Guid Id { get; init; }
    public string ItemName { get; set; } = string.Empty;
    public int QuantityAvailable { get; set; }
    public string CreatorEmail { get; set; } = string.Empty;
    public string PriceAndCurrencyPerDay { get; set; } = string.Empty;
    public int MaxRentalDays { get; set; }
}
