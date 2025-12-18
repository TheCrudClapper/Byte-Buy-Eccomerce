using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.ModelsUI.Items;
using ByteBuy.UI.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;
namespace ByteBuy.UI.ViewModels.Dialogs;

public partial class OfferDialogViewModel : DialogSingleViewModel
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
    private int _units;

    #endregion
    private readonly IDeliveryService _deliveryService;
    public OfferDialogViewModel(IDeliveryService deliveryService) : base("Publish Offer")
    {
        _deliveryService = deliveryService;
    }

    public override Task InitializeForEdit(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task InitializeAsync()
    {

    }

    public async Task InitializeForAdd(ItemListItem item)
    {
        await InitializeAsync();
        SelectedItem = item;
    }

    protected override Task<bool> AddItem()
    {
        throw new NotImplementedException();
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
}
