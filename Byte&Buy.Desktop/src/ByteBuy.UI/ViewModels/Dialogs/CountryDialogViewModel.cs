using ByteBuy.Core.DTO.Country;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels.Dialogs;

public partial class CountryDialogViewModel(ICountryService countryService)
    : DialogSingleViewModel("Country")
{
    [ObservableProperty]
    [Required, MaxLength(50)]
    private string _name = string.Empty;

    [ObservableProperty]
    [Required, MaxLength(3)]
    private string _code = string.Empty;

    public override async Task InitializeForEdit(Guid id)
    {
        IsEditMode = true;
        EditingItemId = id;
        var response = await countryService.GetById(id);
        if (!response.Success)
        {
            Error = response.Error!.Description;
            return;
        }

        var item = response.Value;

        Name = item.Name;
        Code = item.Code;
    }

    protected override async Task<bool> UpdateItem()
    {
        if (EditingItemId is null)
            return false;

        var request = new CountryUpdateRequest(Name, Code);
        var response = await countryService.Update(EditingItemId.Value, request);
        if (!response.Success)
        {
            Error = response.Error!.Description;
            return false;
        }
        return true;
    }

    protected override async Task<bool> AddItem()
    {
        var request = new CountryAddRequest(Name, Code);
        var response = await countryService.Add(request);
        if (!response.Success)
        {
            Error = response.Error!.Description;
            return false;
        }
        return true;
    }


}
