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
    [ObservableProperty]
    [Required(ErrorMessage = "Name is required"), MaxLength(20)]
    private string _name = string.Empty;

    [ObservableProperty]
    [MaxLength(50)]
    private string? _description;

    [ObservableProperty]
    private string _error = string.Empty;
    public override async Task InitializeForEdit(Guid id)
    {
        IsEditMode = true;
        EditingItemId = id;
        var response = await categoryService.GetById(id);
        if (!response.Success)
        {
            Error = response.Error!.Description;
            return;
        }

        var item = response.Value;

        Name = item.Name;
        Description = item.Description;
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
