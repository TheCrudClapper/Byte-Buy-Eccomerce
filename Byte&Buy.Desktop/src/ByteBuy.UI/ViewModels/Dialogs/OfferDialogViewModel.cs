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
    private ObservableCollection<DeliveryOptionResponse> _selectedOtherDeliveries = [];

    [ObservableProperty]
    private ObservableCollection<ParcelLockerCarrierGroup> _selectedParcelLockerDeliveries = [];
    #endregion

    public override Task InitializeForEdit(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task InitializeAsync()
    {
        var deliveries = await deliveryService.GetAvaliableDeliveries();

        //Group parcel by carrier so user can select only one parcel locker delivery option per carrier
        ParcelLockerGroups = new ObservableCollection<ParcelLockerCarrierGroup>(
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
            var selectedDeliveries = SelectedOtherDeliveries.Select(d => d.Id).ToList();
            var request = new SaleOfferAddRequest(SelectedItem?.Id ?? Guid.Empty, QuantityAvaliable, PricePerDay, null, selectedDeliveries);

            var response = await saleOfferService.Add(request);
            if (!response.Success)
            {
                Error = response.Error!.Description;
                return false;
            }
        }
        else
        {
            var selectedDeliveries = SelectedOtherDeliveries.Select(d => d.Id).ToList();

        }

        return true;
    }

    protected override Task<bool> UpdateItem()
    {
        throw new NotImplementedException();
    }

    [RelayCommand]
    private void ChangeOfferType()
       => IsSaleOffer = !IsSaleOffer;

    [RelayCommand]
    private void ToggleSelectedDelivery(DeliveryOptionResponse delivery)
    {
        if (SelectedOtherDeliveries.Contains(delivery))
            SelectedOtherDeliveries.Remove(delivery);
        else
            SelectedOtherDeliveries.Add(delivery);
    }
}
