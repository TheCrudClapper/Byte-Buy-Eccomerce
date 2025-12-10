using ByteBuy.Core.DTO.Delivery;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels.Dialogs;

public partial class DeliveryDialogViewModel(IDeliveryService deliveryService)
    : DialogSingleViewModel("Delivery")
{

    [ObservableProperty]
    [Required, MaxLength(50)]
    private string _name = string.Empty;

    [ObservableProperty]
    [MaxLength(50)]
    private string? _description;

    [ObservableProperty]
    private decimal _price;

    public async override Task InitializeForEdit(Guid id)
    {
        IsEditMode = true;
        EditingItemId = id;
        var response = await deliveryService.GetById(id);
        if (!response.Success)
        {
            Error = response.Error!.Description;
            return;
        }

        var item = response.Value;

        Name = item.Name;
        Description = item.Description;
        Price = item.Amount;
    }

    protected async override Task<bool> AddItem()
    {
        var request = new DeliveryAddRequest(Name, Description, Price);
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

        var request = new DeliveryUpdateRequest(Name, Description, Price);
        var response = await deliveryService.Update(EditingItemId.Value, request);
        if (!response.Success)
        {
            Error = response.Error!.Description;
            return false;
        }
        return true;
    }
}
