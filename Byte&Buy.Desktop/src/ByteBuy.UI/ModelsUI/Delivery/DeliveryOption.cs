using ByteBuy.Services.DTO.Delivery;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace ByteBuy.UI.ModelsUI.Delivery;
public partial class DeliveryOption : ObservableObject
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Carrier { get; set; } = null!;
    public string DeliveryChannel { get; set; } = null!;
    public string PriceAndCurrency { get; set; } = null!;

    [ObservableProperty]
    private bool _isSelected;
}
