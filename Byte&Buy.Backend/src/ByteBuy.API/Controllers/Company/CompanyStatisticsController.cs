using ByteBuy.Core.DTO.Public.Statistics;

namespace ByteBuy.API.Controllers.Company;

[Resource("company-statistics")]
[Route("api/company/statistics")]
[ApiController]
public class CompanyStatisticsController : BaseApiController
{
    private readonly IStatisticsService _statisticsService;
    public CompanyStatisticsController(IStatisticsService statisticsService)
      => _statisticsService = statisticsService;


    [HttpGet("kpi")]
    [HasPermission("{resource}:read:kpi")]
    public async Task<ActionResult<IReadOnlyCollection<KeyPerformanceIndicatorDto>>> GetKpisAsync(CancellationToken ct)
        => HandleResult(await _statisticsService.GetKpisAsync(ct));

    [HttpGet("gmv-seller-type")]
    [HasPermission("{resource}:read:gmv-seller-type")]
    public async Task<ActionResult<IReadOnlyCollection<GMVBySellerTypeDto>>> GetGMVBySellerTypeAsync(CancellationToken ct)
        => HandleResult(await _statisticsService.GetGMVBySellerTypeAsync(ct));

    [HttpGet("gmv-months")]
    [HasPermission("{resource}:read:gmv-months")]
    public async Task<ActionResult<IReadOnlyList<OrdersAndGmvByMonthDto>>> GetOrdersAndGmvByMonthsAsync(CancellationToken ct)
        => HandleResult(await _statisticsService.GetOrdersAndGmvByMonthAsync(6, ct));
}
