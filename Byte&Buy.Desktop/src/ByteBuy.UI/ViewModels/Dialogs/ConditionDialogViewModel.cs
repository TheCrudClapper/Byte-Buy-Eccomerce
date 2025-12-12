using ByteBuy.Services.DTO.Condition;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels.Dialogs;

public partial class ConditionDialogViewModel(IConditionService conditionService)
    : DialogSingleViewModel("Condition")
{
    #region MVVM Fields
    [ObservableProperty]
    [Required, MaxLength(20)]
    private string _name = string.Empty;

    [ObservableProperty]
    [MaxLength(50)]
    private string? _description;
    #endregion
    public override async Task InitializeForEdit(Guid id)
    {
        IsEditMode = true;
        EditingItemId = id;
        var result = await conditionService.GetById(id);
        if (!result.Success)
        {
            Error = result.Error!.Description;
            return;
        }

        Name = result.Value.Name;
        Description = result.Value.Description;
    }

    protected override async Task<bool> UpdateItem()
    {
        if (EditingItemId is null)
            return false;

        var request = new ConditionUpdateRequest(Name, Description);
        var response = await conditionService.Update(EditingItemId.Value, request);
        if (!response.Success)
        {
            Error = response.Error!.Description;
            return false;
        }
        return true;
    }

    protected override async Task<bool> AddItem()
    {
        var request = new ConditionAddRequest(Name, Description);
        var response = await conditionService.Add(request);
        if (!response.Success)
        {
            Error = response.Error!.Description;
            return false;
        }
        return true;
    }
}

