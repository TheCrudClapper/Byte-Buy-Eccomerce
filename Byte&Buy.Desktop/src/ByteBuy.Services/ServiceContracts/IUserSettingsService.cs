using ByteBuy.Services.Models;
using ByteBuy.Services.ResultTypes;
namespace ByteBuy.Services.ServiceContracts;

public interface IUserSettingsService
{
    UserSettings Settings { get; }
    Task<Result<UserSettings?>> LoadSettingsAsync();
    Result<UserSettings?> LoadSettings();
    Task<Result> SaveSettingsAsync(UserSettings settings);
}
