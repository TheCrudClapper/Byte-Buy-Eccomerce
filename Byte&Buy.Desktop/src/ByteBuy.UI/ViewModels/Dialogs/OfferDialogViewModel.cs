using ByteBuy.Services.DTO.Delivery;
using ByteBuy.Services.DTO.SaleOffer;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.ModelsUI.Delivery;
using ByteBuy.UI.ModelsUI.Items;
using ByteBuy.UI.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
namespace ByteBuy.UI.ViewModels.Dialogs;

public partial class OfferDialogViewModel(IDeliveryService deliveryService, ISaleOfferService saleOfferService)
    : DialogSingleViewModel("Publish Offer")
{
    #region MVVM Properties
    [ObservableProperty]
    private bool _isSaleOffer;

    [ObservableProperty]
    private decimal _pricePerDay;

    [ObservableProperty]
    private int _maxRentalDays;

    [ObservableProperty]
    private decimal _pricePerItem;

    [ObservableProperty]
    private ItemListItem? _selectedItem;

    [ObservableProperty]
    private int _quantityAvaliable;

    [ObservableProperty]
    private ObservableCollection<ParcelLockerCarrierGroup> _parcelLockerGroups = [];

    [ObservableProperty]
    private ObservableCollection<DeliveryOptionResponse> _courierDeliveries = [];

    [ObservableProperty]
    private ObservableCollection<DeliveryOptionResponse> _pickupPointDeliveries = [];

    [ObservableProperty]
    private ObservableCollection<DeliveryOptionResponse> _selectedCourierDeliveries = [];

    [ObservableProperty]
    private ObservableCollection<DeliveryOptionResponse> _selectedPickupDeliveries = [];

    #endregion

    public override Task InitializeForEdit(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task InitializeAsync()
    {
        var deliveries = await deliveryService.GetAvaliableDeliveries();

        //Group parcel by carrier so user can select only one parcel locker delivery option per carrier
        ParcelLockerGroups= new ObservableCollection<ParcelLockerCarrierGroup>(
            deliveries.Value.ParcelLockerDeliveries
            .GroupBy(d => d.Carrier)
            .Select(g => new ParcelLockerCarrierGroup()
            {
                Carrier = g.Key,
                Options = new ObservableCollection<DeliveryOptionResponse>(g)
            }));

        CourierDeliveries = new ObservableCollection<DeliveryOptionResponse>(deliveries.Value.CourierDeliveries ?? []);
        PickupPointDeliveries = new ObservableCollection<DeliveryOptionResponse>(deliveries.Value.PickupPointDeliveries ?? []);
    }

    public async Task InitializeForAdd(ItemListItem item)
    {
        await InitializeAsync();
        SelectedItem = item;
    }

    protected override async Task<bool> AddItem()
    {
        if (IsSaleOffer)
        {
            var selectedDeliveries = SelectedCourierDeliveries.Select(d => d.Id).ToList();
            var request = new SaleOfferAddRequest(SelectedItem?.Id ?? Guid.Empty, QuantityAvaliable, PricePerDay, selectedDeliveries);

            var response = await saleOfferService.Add(request);
            if (!response.Success)
            {
                Error = response.Error!.Description;
                return false;
            }
        }
        else
        { 

        }

        return true;
    }

    protected override Task<bool> UpdateItem()
    {
        throw new NotImplementedException();
    }

    [RelayCommand]
    private void ChangeOfferType()
    {
        IsSaleOffer = !IsSaleOffer;
    }

    [RelayCommand]
    private void TogglePickupDelivery(DeliveryOptionResponse delivery)
    {
        if (SelectedPickupDeliveries.Contains(delivery))
            SelectedPickupDeliveries.Remove(delivery);
        else
            SelectedPickupDeliveries.Add(delivery);
    }
        
    [RelayCommand]
    private void ToggleCourierDelivery(DeliveryOptionResponse delivery)
    {
        if (SelectedCourierDeliveries.Contains(delivery))
            SelectedCourierDeliveries.Remove(delivery);
        else
            SelectedCourierDeliveries.Add(delivery);
    }
    
}
