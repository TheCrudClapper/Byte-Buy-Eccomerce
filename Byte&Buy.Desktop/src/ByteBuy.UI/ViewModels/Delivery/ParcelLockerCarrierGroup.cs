using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace ByteBuy.UI.ModelsUI.Delivery;

public partial class ParcelLockerGroupViewModel : ObservableObject
{
    public string Carrier { get; set; } = null!;

    [ObservableProperty]
    public ObservableCollection<DeliveryOptionViewModel> _options = [];
}
