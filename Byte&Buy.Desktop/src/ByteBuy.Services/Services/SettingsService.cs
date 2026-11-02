using ByteBuy.Services.InfraContracts.Stores;
using ByteBuy.Services.Models;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class SettingsService : ISettingsService
{
    private readonly ISettingsStore _store;
    public SettingsService(ISettingsStore store)
        => _store = store;

    public async Task<Result> SaveSettings(AppSettings settings)
        => await _store.SaveSettings(settings);

    public async Task<Result<AppSettings?>> LoadSettings()
        => await _store.GetSettings();
}
