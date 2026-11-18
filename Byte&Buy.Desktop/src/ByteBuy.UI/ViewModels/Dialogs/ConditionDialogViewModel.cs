using ByteBuy.Services.DTO.Condition;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels.Dialogs;

public partial class ConditionDialogViewModel(IConditionService conditionService)
    : DialogSingleViewModel("Condition")
{
    #region MVVM Fields
    private const int _maxDescriptionCharCount = 50;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required, MaxLength(20)]
    private string _name = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [MaxLength(_maxDescriptionCharCount)]
    private string? _description;

    [ObservableProperty]
    private CharCounterViewModel _charCounter = new(_maxDescriptionCharCount);
    #endregion

    partial void OnDescriptionChanged(string? value)
    {
        CharCounter.CurrentCount = value?.Length ?? 0;
        ValidateProperty(value, nameof(Description));
    }

    public override async Task InitializeForEdit(Guid id)
    {
        IsEditMode = true;
        EditingItemId = id;
        var result = await conditionService.GetByIdAsync(id);
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
        var response = await conditionService.UpdateAsync(EditingItemId.Value, request);
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
        var response = await conditionService.AddAsync(request);
        if (!response.Success)
        {
            Error = response.Error!.Description;
            return false;
        }
        return true;
    }
}

