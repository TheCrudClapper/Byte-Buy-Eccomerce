using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.Pagination;

public class PaginationParameters
{
    private const int MaxPageSize = 20;
    private int _pageSize = 10;

    [Range(1, int.MaxValue, ErrorMessage = "Page number must greater that 0")]
    public int PageNumber { get; set; } = 1;

    [Range(1, int.MaxValue, ErrorMessage = "Page size must greater that 0")]
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
    }
}
