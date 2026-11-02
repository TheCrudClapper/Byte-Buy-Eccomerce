using ByteBuy.Services.InfraContracts.Stores;
using ByteBuy.Services.Models;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class SettingsService : ISettingsService
{
    private readonly ISettingsStore _store;
    //Main source of truth for settings
    public AppSettings Settings { get; set; } = null!;
    public SettingsService(ISettingsStore store)
    {
        _store = store;
        //synchronusly get settings into memory 
        var result = Task.Run(async () => await LoadSettingsAsync()).GetAwaiter().GetResult();
        if (result.Success && result.Value is not null)
            Settings = result.Value;
        else
            Settings = new AppSettings();
    }

    public async Task<Result> SaveSettingsAsync(AppSettings settings)
    {
        var result = await _store.SaveSettings(settings);
        if (result.Success)
            Settings = settings;

        return Result.Ok();
    }

    public async Task<Result<AppSettings?>> LoadSettingsAsync()
        => await _store.GetSettings();
}
