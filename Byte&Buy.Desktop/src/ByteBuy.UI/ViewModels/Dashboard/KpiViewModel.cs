using ByteBuy.Services.DTO.Statistics;

namespace ByteBuy.UI.ViewModels.Dashboard;

public class KpiViewModel
{
    public string Key { get; set; } = null!;
    public string Label { get; set; } = null!;
    public string DisplayValue { get; set; } = null!;
    public string IconPath { get; set; } = null!;

    public KpiViewModel(KeyPerformanceIndicatorDto dto)
    {
        Key = dto.Key;
        Label = dto.Label;
        DisplayValue = dto.DisplayValue;
        IconPath = Key switch
        {
            "users" => "avares://ByteBuy.UI/Assets/Images/regular/users-solid-full.svg",
            "employees" => "avares://ByteBuy.UI/Assets/Images/regular/building-user-solid-full.svg",
            "orders" => "avares://ByteBuy.UI/Assets/Images/regular/boxes-packing-solid-full-white.svg",
            "gmv" => "avares://ByteBuy.UI/Assets/Images/regular/dollar-sign-solid-full.svg",
            "cashflow" => "avares://ByteBuy.UI/Assets/Images/regular/coins-solid-full.svg",
            "activeSellers" => "avares://ByteBuy.UI/Assets/Images/regular/hand-holding-dollar-solid-full.svg",
            _ => "avares://ByteBuy.UI/Assets/Images/regular/users-solid-full.svg",
        };
    }
}
