using ByteBuy.Services.Models;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Attributes;
using ByteBuy.UI.Data;
using ByteBuy.UI.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
namespace ByteBuy.UI.ViewModels.Dialogs;

public partial class SettingsDialogViewModel : DialogViewModel
{
    [ObservableProperty]
    [StringLength(maximumLength: 50)]
    [UrlValidator(OmitNullOrEmpty = true)]
    [NotifyDataErrorInfo]
    private string? _backendUrl;
    [ObservableProperty]
    private string? _message;
    private readonly IUserSettingsService _settingsService;
    public SettingsDialogViewModel(IUserSettingsService settingsService) : base(ApplicationDialogNames.Settings.ToString())
    {
        _settingsService = settingsService;
    }

    public async Task Initialize()
    {
        BackendUrl = _settingsService.Settings?.BackendUrl ?? string.Empty;
    }

    [RelayCommand]
    public async Task Save()
    {
        if (HasErrors) return;

        var settings = _settingsService.Settings;
        settings.BackendUrl = BackendUrl;
        var result = await _settingsService.SaveSettingsAsync(settings);

        if (!result.Success)
            Message = result.Error!.Description;
    }

    [RelayCommand]
    public void ClearFields()
    {
        BackendUrl = null;
    }
}