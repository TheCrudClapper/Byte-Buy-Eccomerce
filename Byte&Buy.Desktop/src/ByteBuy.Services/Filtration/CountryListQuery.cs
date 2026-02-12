namespace ByteBuy.Services.Filtration;

public sealed class CountryListQuery
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 11;
    public string? CountryName { get; init; }
    public string? Code { get; init; }
}
