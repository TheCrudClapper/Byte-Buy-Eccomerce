using ByteBuy.Services.InfraContracts.Stores;
using ByteBuy.Services.Models;
using ByteBuy.Services.ResultTypes;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace ByteBuy.Infrastructure.Stores;

public class UserSettingsStore : IUserSettingsStore
{
    private readonly IConfiguration _configuration;
    private readonly JsonSerializerOptions _serializerOptions;
    public UserSettingsStore(IConfiguration configuration)
    {
        _configuration = configuration;
        _serializerOptions = new JsonSerializerOptions()
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    public Result<UserSettings?> GetSettings()
    {
        try
        {
            var path = GetOrCreateFilePath();
            if (new FileInfo(path).Length == 0)
                return Result<UserSettings?>.Ok(null);

            var json = File.ReadAllText(path);
            var appSettings = JsonSerializer.Deserialize<UserSettings>(json, _serializerOptions);
            return Result<UserSettings?>.Ok(appSettings);
        }
        catch (Exception)
        {
            return Result<UserSettings?>
                .Fail(new Error("Something went wrong with deserialization."));
        }
    }

    public async Task<Result<UserSettings?>> GetSettingsAsync()
    {
        try
        {
            var path = GetOrCreateFilePath();
            if (new FileInfo(path).Length == 0)
                return Result<UserSettings?>.Ok(null);

            var json = await File.ReadAllTextAsync(path);
            var appSettings = JsonSerializer.Deserialize<UserSettings>(json, _serializerOptions);
            return Result<UserSettings?>.Ok(appSettings);
        }
        catch (Exception)
        {
            //you can smth log here
            return Result<UserSettings?>
                .Fail(new Error("Something went wrong with deserialization."));
        }
    }

    public async Task<Result> SaveSettingsAsync(UserSettings settings)
    {
        try
        {
            var path = GetOrCreateFilePath();
            var json = JsonSerializer.Serialize(settings, _serializerOptions);
            await File.WriteAllTextAsync(path, json);
            return Result.Ok();
        }
        catch (Exception)
        {
            //you can smth log here
            return Result
               .Fail(new Error("Something went while saving settings."));
        }
    }

    private string GetOrCreateFilePath()
    {
        string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string byteBuyAppData = Path.Combine(appDataPath, _configuration.GetValue<string>("SpecialFolderName") ?? "ByteBuy");

        if (!Path.Exists(byteBuyAppData))
            Directory.CreateDirectory(byteBuyAppData);

        string filePath = Path.Combine(byteBuyAppData, _configuration.GetValue<string>("SettingsFileName") ?? "settings.json");
        if (!File.Exists(filePath))
            File.Create(filePath).Close();

        return filePath;
    }
}
