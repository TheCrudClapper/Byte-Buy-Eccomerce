using ByteBuy.Services.InfraContracts.Stores;
using ByteBuy.Services.Models;
using ByteBuy.Services.ResultTypes;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace ByteBuy.Infrastructure.Stores;

public class SettingsStore : ISettingsStore
{
    private readonly IConfiguration _configuration;
    private readonly JsonSerializerOptions _serializerOptions;
    public SettingsStore(IConfiguration configuration)
    {
        _configuration = configuration;
        _serializerOptions = new JsonSerializerOptions()
        { 
            WriteIndented = true, 
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
        };
    }

    public async Task<Result<AppSettings?>> GetSettings()
    {
        try
        {
            var path = GetOrCreateFilePath();
            if(new FileInfo(path).Length == 0)
                return Result<AppSettings?>.Ok(null);
            
            var json = await File.ReadAllTextAsync(path);
            var appSettings = JsonSerializer.Deserialize<AppSettings>(json, _serializerOptions);
            return Result<AppSettings?>.Ok(appSettings);
        }
        catch (Exception ex)
        {
            //you can smth log here
            return Result<AppSettings?>
                .Fail(new Error("Something went wrong with deserialization."));
        }
    }

    public async Task<Result> SaveSettings(AppSettings settings)
    {
        try
        {
            var path = GetOrCreateFilePath();
            var json = JsonSerializer.Serialize(settings, _serializerOptions);
            await File.WriteAllTextAsync(path, json);
            return Result.Ok();
        }
        catch (Exception ex)
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
