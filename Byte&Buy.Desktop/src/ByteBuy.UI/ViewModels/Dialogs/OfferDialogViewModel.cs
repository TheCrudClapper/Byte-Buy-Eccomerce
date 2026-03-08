using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.ModelsUI.Delivery;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Items;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
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
    [NotifyDataErrorInfo]
    [Required]
    private decimal _pricePerDay;

    [ObservableProperty]
    private int? _maxRentalDays;

    [ObservableProperty]
    private int? _currentAvaliableQuantity;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    private int _rentalDays;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required, Range(0, double.MaxValue)]
    private decimal _pricePerItem;

    [ObservableProperty]
    private ItemListItemViewModel? _selectedItem;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required, Range(0, int.MaxValue)]
    private int _quantity;

    [ObservableProperty]
    private ObservableCollection<ParcelLockerGroupViewModel> _parcelLockerGroups = [];

    [ObservableProperty]
    private ObservableCollection<DeliveryOptionViewModel> _courierDeliveries = [];

    [ObservableProperty]
    private ObservableCollection<DeliveryOptionViewModel> _pickupPointDeliveries = [];
    #endregion

    public override async Task InitializeForEdit(Guid id)
    {
        IsSaleOffer = true;
        IsEditMode = true;
        EditingItemId = id;
        await InitializeAsync();
        var result = await saleOfferService.GetByIdAsync(id);
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
        var result = await rentOfferService.GetByIdAsync(id);
        if (!result.Success)
        {
            Error = result.Error!.Description;
            return;
        }

        OfferMappings.MapFromRentResponse(this, result.Value);
    }

    public async Task InitializeAsync()
    {
        var deliveries = await deliveryService.GetAvaliableDeliveriesAsync();
        if (!deliveries.Success || deliveries.Value is null)
            return;

        //Group parcel by carrier so user can select only one parcel locker delivery option per carrier
        ParcelLockerGroups = new ObservableCollection<ParcelLockerGroupViewModel>(
            deliveries.Value.ParcelLockerDeliveries
            .GroupBy(d => d.Carrier)
            .Select(g => new ParcelLockerGroupViewModel()
            {
                Carrier = g.Key,
                Options = new ObservableCollection<DeliveryOptionViewModel>(g.Select(d => d.ToDeliveryOption()).ToList())
            }));

        CourierDeliveries = new ObservableCollection<DeliveryOptionViewModel>(deliveries.Value.CourierDeliveries.Select(d => d.ToDeliveryOption()).ToList());
        PickupPointDeliveries = new ObservableCollection<DeliveryOptionViewModel>(deliveries.Value.PickupPointDeliveries.Select(d => d.ToDeliveryOption()).ToList());
    }

    public async Task InitializeForAdd(ItemListItemViewModel item)
    {
        await InitializeAsync();
        SelectedItem = item;
    }

    protected override async Task<bool> AddItem()
    {
        bool isValid = Validate();
        ValidateAllProperties();
        if (!isValid) return false;
        if (IsSaleOffer)
        {

            var request = OfferMappings.MapToSaleAddRequest(this);
            var response = await saleOfferService.AddAsync(request);
            if (!response.Success)
            {
                Error = response.Error!.Description;
                return false;
            }
        }
        else
        {
            var request = OfferMappings.MapToRentAddRequest(this);
            var response = await rentOfferService.AddAsync(request);
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
            var response = await saleOfferService.UpdateAsync(EditingItemId!.Value, request);
            if (!response.Success)
            {
                Error = response.Error!.Description;
                return false;
            }
        }
        else
        {
            var request = OfferMappings.MapToRentUpdateRequest(this);
            var response = await rentOfferService.UpdateAsync(EditingItemId!.Value, request);
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
