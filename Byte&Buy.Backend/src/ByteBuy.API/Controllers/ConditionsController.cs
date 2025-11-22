using ByteBuy.Core.DTO.Condition;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ConditionsController : BaseApiController
{
    private readonly IConditionService _conditionService;
    public ConditionsController(IConditionService conditionService)
        => _conditionService = conditionService;

    //[HasPermission("condition:write")]
    [HttpPost]
    public async Task<ActionResult> PostCondition(ConditionAddRequest request)
        => HandleResult(await _conditionService.AddCondition(request));

    [HttpPut("{conditionId}")]
    //[HasPermission("condition:update")]
    public async Task<ActionResult> PutCondition(Guid conditionId, ConditionUpdateRequest request)
        => HandleResult(await _conditionService.UpdateCondition(conditionId, request));

    [HttpDelete("{conditionId}")]
    //[HasPermission("condition:delete")]
    public async Task<IActionResult> DeleteCondition(Guid conditionId)
        => HandleResult(await _conditionService.DeleteCondition(conditionId));

    [HttpGet("{conditionId}")]
    //[HasPermission("condition:read")]
    public async Task<ActionResult> GetCondition(Guid conditionId, CancellationToken ct)
        => HandleResult(await _conditionService.GetCondition(conditionId, ct));

    [HttpGet]
    //[HasPermission("condition:read:many")]
    public async Task<ActionResult> GetCountries(CancellationToken ct)
        => HandleResult(await _conditionService.GetConditions(ct));

    [HttpGet("options")]
    //[HasPermission("condition:selectlist")]
    public async Task<ActionResult> GetSelectList(CancellationToken ct)
        => HandleResult(await _conditionService.GetSelectList(ct));
}
