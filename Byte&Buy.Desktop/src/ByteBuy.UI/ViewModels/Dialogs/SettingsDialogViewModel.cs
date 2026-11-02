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
    [Required]
    [StringLength(maximumLength: 50)]
    [UrlValidator]
    [NotifyDataErrorInfo]
    private string? _backendUrl;
    private AppSettings? _appSettings;
    [ObservableProperty]
    private string? _error;
    private readonly ISettingsService _settingsService;
    public SettingsDialogViewModel(ISettingsService settingsService) : base(ApplicationDialogNames.Settings.ToString())
    {
        _settingsService = settingsService;
    }

    public async Task Initialize()
    {
        var result = await _settingsService.LoadSettingsAsync();
        if (!result.Success)
        {
            Error = result.Error!.Description;
            return;
        }

        //when returned null it means that we have no settings
        _appSettings = result.Value ?? new AppSettings();
        BackendUrl = _appSettings?.BackendUrl ?? string.Empty;
    }

    [RelayCommand]
    public async Task Save()
    {
        if (_appSettings is null || HasErrors)
        {
            Error = "Something went wrong, try using valid url.";
            return;
        }

        _appSettings.BackendUrl = BackendUrl;
        var result = await _settingsService.SaveSettingsAsync(_appSettings);
        if(!result.Success)
            Error = result.Error!.Description;
    }

    [RelayCommand]
    public void ClearFields()
    {
        BackendUrl = null;
    }
}