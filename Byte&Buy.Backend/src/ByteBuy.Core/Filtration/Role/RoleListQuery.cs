namespace ByteBuy.Core.Filtration.Role;

public sealed class RoleListQuery
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 11;
    public string? RoleName { get; init; }
}
