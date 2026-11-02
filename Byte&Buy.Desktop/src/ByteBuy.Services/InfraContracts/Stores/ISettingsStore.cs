using ByteBuy.Services.Models;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.InfraContracts.Stores;

public interface ISettingsStore
{
    Task<Result<AppSettings?>> GetSettings();
    Task<Result> SaveSettings(AppSettings settings);
}
