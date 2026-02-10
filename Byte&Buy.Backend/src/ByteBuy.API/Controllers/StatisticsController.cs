using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Statistics;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Route("api/statistics")]
[ApiController]
public class StatisticsController : BaseApiController
{
    private readonly IStatisticsService _statisticsService;
    public StatisticsController(IStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
    }

    [HttpGet("kpi")]
    public async Task<ActionResult<IReadOnlyCollection<KeyPerformanceIndicatorDto>>> GetKpis(CancellationToken ct = default)
        => HandleResult(await _statisticsService.GetKpisAsync());
}
