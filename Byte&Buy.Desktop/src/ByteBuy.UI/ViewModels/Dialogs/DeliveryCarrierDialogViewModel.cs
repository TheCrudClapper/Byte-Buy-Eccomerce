using ByteBuy.Services.DTO.DeliveryCarrier;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels.Dialogs;

public partial class DeliveryCarrierDialogViewModel(IDeliveryCarrierService deliveryCarrierService)
    : DialogSingleViewModel("OrderDelivery Carrier")
{
    #region MVVM Properties
    [ObservableProperty]
    [Required, MaxLength(50)]
    private string _name = string.Empty;

    [ObservableProperty]
    [Required, MaxLength(20)]
    private string _code = string.Empty;

    #endregion

    public override async Task InitializeForEdit(Guid id)
    {
        IsEditMode = true;
        EditingItemId = id;
        var result = await deliveryCarrierService.GetById(id);
        if (!result.Success)
        {
            Error = result.Error!.Description;
            return;
        }

        Name = result.Value.Name;
        Code = result.Value.Code;
    }

    protected override async Task<bool> AddItem()
    {
        var request = new DeliveryCarrierAddRequest(Name, Code);
        var response = await deliveryCarrierService.Add(request);
        if (!response.Success)
        {
            Error = response.Error!.Description;
            return false;
        }
        return true;
    }

    protected override async Task<bool> UpdateItem()
    {
        if (EditingItemId is null)
            return false;

        var request = new DeliveryCarrierUpdateRequest(Name, Code);
        var response = await deliveryCarrierService.Update(EditingItemId.Value, request);
        if (!response.Success)
        {
            Error = response.Error!.Description;
            return false;
        }
        return true;
    }

}
