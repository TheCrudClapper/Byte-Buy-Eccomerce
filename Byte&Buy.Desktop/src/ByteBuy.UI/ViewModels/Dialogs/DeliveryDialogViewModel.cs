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
    #endregion
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

        Name = result.Value.Name;
        Description = result.Value.Description;
        Price = result.Value.Amount;
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
