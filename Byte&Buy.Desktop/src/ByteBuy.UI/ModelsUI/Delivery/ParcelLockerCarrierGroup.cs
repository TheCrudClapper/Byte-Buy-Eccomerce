using ByteBuy.Services.DTO.Delivery;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace ByteBuy.UI.ModelsUI.Delivery;
public partial class ParcelLockerCarrierGroup : ObservableObject
{
    public string Carrier { get; set; } = null!;

    [ObservableProperty]
    public ObservableCollection<DeliveryOptionResponse> _options  = [];

    [ObservableProperty]
    public DeliveryOptionResponse? _selectedOption;
}
