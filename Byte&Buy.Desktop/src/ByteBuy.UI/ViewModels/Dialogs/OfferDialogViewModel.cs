using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Mappings;
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

public partial class OfferDialogViewModel(IDeliveryService deliveryService,
    ISaleOfferService saleOfferService,
    IRentOfferService rentOfferService)
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
    private ObservableCollection<DeliveryOption> _courierDeliveries = [];

    [ObservableProperty]
    private ObservableCollection<DeliveryOption> _pickupPointDeliveries = [];

    [ObservableProperty]
    private ObservableCollection<ParcelLockerCarrierGroup> _selectedParcelLockerDeliveries = [];
    #endregion

    public override async Task InitializeForEdit(Guid id)
    {
        IsSaleOffer = true;
        IsEditMode = true;
        EditingItemId = id;
        await InitializeAsync();
        var result = await saleOfferService.GetById(id);
        if (!result.Success)
        {
            Error = result.Error!.Description;
            return;
        }

        OfferMappings.MapFromSaleResponse(this, result.Value);
    }

    public async Task InitializeForRentEdit(Guid id)
    {
        IsEditMode = true;
        EditingItemId = id;
        await InitializeAsync();
        var result = await rentOfferService.GetById(id);
        if (!result.Success)
        {
            Error = result.Error!.Description;
            return;
        }

        OfferMappings.MapFromRentResponse(this, result.Value);
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
                Options = new ObservableCollection<DeliveryOption>(g.Select(d => d.ToDeliveryOption()).ToList())
            }));

        CourierDeliveries = new ObservableCollection<DeliveryOption>(deliveries.Value.CourierDeliveries.Select(d => d.ToDeliveryOption()).ToList());
        PickupPointDeliveries = new ObservableCollection<DeliveryOption>(deliveries.Value.PickupPointDeliveries.Select(d => d.ToDeliveryOption()).ToList());
    }

    public async Task InitializeForAdd(ItemListItem item)
    {
        await InitializeAsync();
        SelectedItem = item;
    }

    protected override async Task<bool> AddItem()
    {
        bool isValid = Validate();
        if (!isValid) return false;
        if (IsSaleOffer)
        {

            var request = OfferMappings.MapToSaleAddRequest(this);
            var response = await saleOfferService.Add(request);
            if (!response.Success)
            {
                Error = response.Error!.Description;
                return false;
            }
        }
        else
        {
            var request = OfferMappings.MapToRentAddRequest(this);
            var response = await rentOfferService.Add(request);
            if (!response.Success)
            {
                Error = response.Error!.Description;
                return false;
            }
        }
        return true;
    }

    protected override async Task<bool> UpdateItem()
    {
        bool isValid = Validate();
        if (!isValid) return false;
        if (IsSaleOffer)
        {
            var request = OfferMappings.MapToSaleUpdateRequest(this);
            var response = await saleOfferService.Update(EditingItemId!.Value, request);
            if (!response.Success)
            {
                Error = response.Error!.Description;
                return false;
            }
        }
        else
        {
            var request = OfferMappings.MapToRentUpdateRequest(this);
            var response = await rentOfferService.Update(EditingItemId!.Value, request);
            if (!response.Success)
            {
                Error = response.Error!.Description;
                return false;
            }
        }

        return true;
    }

    [RelayCommand]
    private void ChangeOfferType()
       => IsSaleOffer = !IsSaleOffer;

    public bool Validate()
    {
        if (!CourierDeliveries.Any(cd => cd.IsSelected) && !PickupPointDeliveries.Any(cd => cd.IsSelected))
        {
            Error = "At least one delivery is required";
            return false;
        }
        return true;
    }
}
