namespace ByteBuy.Core.Filtration.Condition;

public sealed class ConditionListQuery
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string? ConditionName { get; init; }
};
