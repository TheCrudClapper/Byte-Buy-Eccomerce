using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Statistics;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers.Company;

[Resource("company-statistics")]
[Route("api/company/statistics")]
[ApiController]
public class CompanyStatisticsController : BaseApiController
{
    private readonly IStatisticsService _statisticsService;
    public CompanyStatisticsController(IStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
    }

    [HttpGet("kpi")]
    //[HasPermission("kpi")]
    public async Task<ActionResult<IReadOnlyCollection<KeyPerformanceIndicatorDto>>> GetKpis(CancellationToken ct)
        => HandleResult(await _statisticsService.GetKpisAsync(ct));

    [HttpGet("gmv-seller-type")]
    //[HasPermission("gmv-seller-type")]
    public async Task<ActionResult<IReadOnlyCollection<GMVBySellerTypeDto>>> GetGMVBySellerType(CancellationToken ct)
        => HandleResult(await _statisticsService.GetGMVBySellerType(ct));

    [HttpGet("gmv-months")]
    //[HasPermission("gmv-months")]
    public async Task<ActionResult<IReadOnlyList<OrdersAndGmvByMonthDto>>> GetOrdersAndGmvByMonths(CancellationToken ct)
        => HandleResult(await _statisticsService.GetOrdersAndGmvByMonthAsync(6, ct));
}
