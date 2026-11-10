using ByteBuy.Services.InfraContracts.Stores;
using ByteBuy.Services.Models;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class UserSettingsService : IUserSettingsService
{
    private readonly IUserSettingsStore _store;
    private UserSettings _settings;
    public UserSettings Settings
    {
        get => _settings;
    }

    public UserSettingsService(IUserSettingsStore store)
    {
        _store = store;
        //synchronusly get settings into memory 
        //var result = Task.Run(async () => await LoadSettingsAsync()).GetAwaiter().GetResult();
        var result = _store.GetSettings();
        if (result.Success && result.Value is not null)
            _settings = result.Value;
        else
            _settings = new UserSettings();
    }

    public async Task<Result> SaveSettingsAsync(UserSettings settings)
    {
        var result = await _store.SaveSettingsAsync(settings);
        if (result.Success)
            _settings = settings;

        return Result.Ok();
    }

    public async Task<Result<UserSettings?>> LoadSettingsAsync()
        => await _store.GetSettingsAsync();

    public Result<UserSettings?> LoadSettings()
        => _store.GetSettings();
}
