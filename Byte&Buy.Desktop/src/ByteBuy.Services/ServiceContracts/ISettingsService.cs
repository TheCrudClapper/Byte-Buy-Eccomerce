using ByteBuy.Services.Models;
using ByteBuy.Services.ResultTypes;
namespace ByteBuy.Services.ServiceContracts;

public interface ISettingsService
{
    AppSettings Settings { get; set; }
    Task<Result<AppSettings?>> LoadSettingsAsync();
    Task<Result> SaveSettingsAsync(AppSettings settings);
}
