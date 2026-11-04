using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Attributes;
using ByteBuy.UI.Data;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Result;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    [ObservableProperty]
    private ResultViewModel _result = null!;
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
        {
            Result = ResultViewModel.CreateSuccess(result?.Error?.Description ?? "Something went wrong");
            return;
        }

        Result = ResultViewModel.CreateSuccess("Successfully saved changes.");
    }

    [RelayCommand]
    public void ClearFields()
        => BackendUrl = null;
}