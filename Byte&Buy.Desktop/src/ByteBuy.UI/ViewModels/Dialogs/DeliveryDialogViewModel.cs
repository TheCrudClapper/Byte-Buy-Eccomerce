using ByteBuy.Core.DTO.Delivery;
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

public partial class DeliveryDialogViewModel(IDeliveryService deliveryService)
    : DialogSingleViewModel("Delivery")
{
    #region MVVM Properties
    [ObservableProperty]
    [Required, MaxLength(50)]
    private string _name = string.Empty;

    [ObservableProperty]
    [MaxLength(50)]
    private string? _description;

    [ObservableProperty]
    [Range(1, double.MaxValue, ErrorMessage = "Price cannot be less that 1")]
    private decimal _price;

    [ObservableProperty]
    private ObservableCollection<SelectListItemResponse<int>> _parcelLockerSize = [];

    [ObservableProperty]
    private ObservableCollection<SelectListItemResponse<int>> _deliveryChannels = [];

    [ObservableProperty]
    private SelectListItemResponse<int>? _selectedSize;

    [ObservableProperty]
    [Required]
    private SelectListItemResponse<int>? _selectedChannel;
    #endregion

    public async Task InitializeAsync()
    {
        var parcelLockerSizesTask = deliveryService.GetParcelLockerSizesList();
        var deliveryChannelsTask = deliveryService.GetDeliveryChannelsList();

        await Task.WhenAll(parcelLockerSizesTask, deliveryChannelsTask);

        var parcelResult = await parcelLockerSizesTask;
        var deliveryChannelResult = await deliveryChannelsTask;

        ParcelLockerSize = new ObservableCollection<SelectListItemResponse<int>>(parcelResult.Value ?? []);
        DeliveryChannels = new ObservableCollection<SelectListItemResponse<int>>(deliveryChannelResult.Value ?? []);
    }

    public async override Task InitializeForEdit(Guid id)
    {
        await InitializeAsync();
        IsEditMode = true;
        EditingItemId = id;
        var result = await deliveryService.GetById(id);
        if (!result.Success)
        {
            Error = result.Error!.Description;
            return;
        }

        Name = result.Value.Name;
        Description = result.Value.Description;
        Price = result.Value.Amount;
        SelectedSize = ParcelLockerSize.FirstOrDefault(p => p.Id == result.Value.ParcelSizeId);
        SelectedChannel = DeliveryChannels.FirstOrDefault(p => p.Id == result.Value.ChannelId);
    }


    protected async override Task<bool> AddItem()
    {
        var request = new DeliveryAddRequest(Name, Description, Price, SelectedSize?.Id, SelectedChannel?.Id ?? default);
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

        var request = new DeliveryUpdateRequest(Name, Description, Price, SelectedSize?.Id, SelectedChannel?.Id ?? default);
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
}
