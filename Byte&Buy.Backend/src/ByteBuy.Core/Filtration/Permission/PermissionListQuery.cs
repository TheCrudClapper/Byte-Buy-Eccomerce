namespace ByteBuy.Core.Filtration.Permission;

public sealed class PermissionListQuery
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string? Name { get; init; }
    public string? Description { get; init; }
}
