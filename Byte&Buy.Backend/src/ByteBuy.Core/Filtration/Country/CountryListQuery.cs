using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Services.Filtration;

public sealed class CountryListQuery
{
    [Range(1, int.MaxValue, ErrorMessage = "Page number must greater that 0")]
    public int PageNumber { get; init; } = 1;

    [Range(1, int.MaxValue, ErrorMessage = "Page size must greater that 0")]
    public int PageSize { get; init; } = 10;

    public string? CountryName { get; init; }
    public string? Code { get; init; }
}
