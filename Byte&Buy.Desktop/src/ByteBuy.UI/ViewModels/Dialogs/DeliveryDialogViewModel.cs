using ByteBuy.Core.DTO.Delivery;
using ByteBuy.Services.DTO.Delivery;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels.Dialogs;

public partial class DeliveryDialogViewModel(
    IDeliveryService deliveryService,
    IDeliveryCarrierService carrierService)
    : DialogSingleViewModel("OrderDelivery")
{
    #region MVVM Properties
    [ObservableProperty]
    [Required, MaxLength(50)]
    private string _name = string.Empty;

    [ObservableProperty]
    [MaxLength(50)]
    private string? _description;

    [ObservableProperty]
    [Range(1, double.MaxValue, ErrorMessage = "PriceFrom cannot be less that 1")]
    private decimal _price;

    [ObservableProperty]
    private ObservableCollection<SelectListItemResponse<int>> _parcelLockerSize = [];

    [ObservableProperty]
    private ObservableCollection<SelectListItemResponse<int>> _deliveryChannels = [];

    [ObservableProperty]
    private ObservableCollection<SelectListItemResponse<Guid>> _deliveryCarriers = [];

    [ObservableProperty]
    private SelectListItemResponse<int>? _selectedSize;

    [ObservableProperty]
    [Required]
    private SelectListItemResponse<int>? _selectedChannel;

    [ObservableProperty]
    [Required]
    private SelectListItemResponse<Guid>? _selectedDeliveryCarrier;
    #endregion

    //Optimize it
    public async Task InitializeAsync()
    {
        var parcelLockerSizesTask = deliveryService.GetParcelLockerSizesSelectList();
        var deliveryChannelsTask = deliveryService.GetDeliveryChannelsSelectList();
        var deliveryCarriersTask = carrierService.GetSelectList();

        await Task.WhenAll(parcelLockerSizesTask, deliveryChannelsTask, deliveryCarriersTask);

        var parcelResult = parcelLockerSizesTask.Result;
        var deliveryChannelResult = deliveryChannelsTask.Result;
        var deliveryCarriersResult = deliveryCarriersTask.Result;

        ParcelLockerSize = new ObservableCollection<SelectListItemResponse<int>>(parcelResult.Value ?? []);
        DeliveryChannels = new ObservableCollection<SelectListItemResponse<int>>(deliveryChannelResult.Value ?? []);
        DeliveryCarriers = new ObservableCollection<SelectListItemResponse<Guid>>(deliveryCarriersResult.Value ?? []);
    }

    public async override Task InitializeForEdit(Guid id)
    {
        IsEditMode = true;
        EditingItemId = id;
        var result = await deliveryService.GetById(id);
        if (!result.Success)
        {
            Error = result.Error!.Description;
            return;
        }
        await InitializeAsync();

        Name = result.Value.Name;
        Description = result.Value.Description;
        Price = result.Value.Amount;
        SelectedSize = ParcelLockerSize.FirstOrDefault(p => p.Id == result.Value.ParcelSizeId);
        SelectedChannel = DeliveryChannels.FirstOrDefault(p => p.Id == result.Value.ChannelId);
        SelectedDeliveryCarrier = DeliveryCarriers.FirstOrDefault(dc => dc.Id == result.Value.CarrierId);
    }


    protected async override Task<bool> AddItem()
    {
        var request = new DeliveryAddRequest(
            Name,
            Description,
            Price,
            SelectedSize?.Id,
            SelectedChannel?.Id ?? default,
            SelectedDeliveryCarrier?.Id ?? Guid.Empty);

        var response = await deliveryService.Add(request);
        if (!response.Success)
        {
            Error = response.Error!.Description;
            return false;
        }
        return true;
    }

    protected async override Task<bool> UpdateItem()
    {
        if (EditingItemId is null)
            return false;

        var request = new DeliveryUpdateRequest(
            Name,
            Description,
            Price,
            SelectedSize?.Id,
            SelectedChannel?.Id ?? default,
            SelectedDeliveryCarrier?.Id ?? Guid.Empty);

        var response = await deliveryService.Update(EditingItemId.Value, request);
        if (!response.Success)
        {
            Error = response.Error!.Description;
            return false;
        }
        return true;
    }

    [RelayCommand]
    private void ClearSelectedSize()
        => SelectedSize = null;

    [RelayCommand]
    private void ClearSelectedChannel()
      => SelectedChannel = null;

    [RelayCommand]
    private void ClearSelectedCarrier()
      => SelectedDeliveryCarrier = null;

}
