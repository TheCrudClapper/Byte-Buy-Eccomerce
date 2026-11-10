using ByteBuy.Services.Models;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.InfraContracts.Stores;

public interface IUserSettingsStore
{
    Task<Result<UserSettings?>> GetSettingsAsync();
    Result<UserSettings?> GetSettings();
    Task<Result> SaveSettingsAsync(UserSettings settings);
}
