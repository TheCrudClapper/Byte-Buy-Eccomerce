using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers.Public;

[Route("api/conditions")]
[ApiController]

public class PublicConditionsController : BaseApiController
{
    private readonly IConditionService _conditionService;
    public PublicConditionsController(IConditionService conditionService)
        => _conditionService = conditionService;

    [HttpGet("options")]
    public async Task<ActionResult<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectListAsync(CancellationToken ct)
      => HandleResult(await _conditionService.GetSelectListAsync(ct));
}
