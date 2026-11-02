using ByteBuy.Services.Models;
using ByteBuy.Services.ResultTypes;
namespace ByteBuy.Services.ServiceContracts;

public interface ISettingsService
{
    Task<Result<AppSettings?>> LoadSettings();

    Task<Result> SaveSettings(AppSettings settings);
}
