using ByteBuy.Services.DTO.Category;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels.Dialogs;

public partial class CategoryDialogViewModel(ICategoryService categoryService)
    : DialogSingleViewModel("Category")
{
    #region MVVM Fields
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Name is required"), MaxLength(20)]
    private string _name = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [MaxLength(50)]
    private string? _description;
    #endregion
    public override async Task InitializeForEdit(Guid id)
    {
        IsEditMode = true;
        EditingItemId = id;
        var result = await categoryService.GetById(id);
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

        var request = new CategoryUpdateRequest(Name, Description);
        var response = await categoryService.Update(EditingItemId.Value, request);
        if (!response.Success)
        {
            Error = response.Error!.Description;
            return false;
        }
        return true;
    }

    protected override async Task<bool> AddItem()
    {
        var request = new CategoryAddRequest(Name, Description);
        var response = await categoryService.Add(request);
        if (!response.Success)
        {
            Error = response.Error!.Description;
            return false;
        }
        return true;
    }
}
