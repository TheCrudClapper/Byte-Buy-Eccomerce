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
    {
        _conditionService = conditionService;
    }

    //[HasPermission("condition:write")]
    [HttpPost]
    public async Task<ActionResult> PostCondition(ConditionAddRequest request, CancellationToken cancellationToken)
        => HandleResult(await _conditionService.AddCondition(request, cancellationToken));

    [HttpPut("{conditionId}")]
    //[HasPermission("condition:update")]
    public async Task<ActionResult> PutCondition(Guid conditionId, ConditionUpdateRequest request, CancellationToken cancellationToken)
        => HandleResult(await _conditionService.UpdateCondition(conditionId, request, cancellationToken));

    [HttpDelete("{conditionId}")]
    //[HasPermission("condition:delete")]
    public async Task<IActionResult> DeleteCondition(Guid conditionId, CancellationToken cancellationToken)
        => HandleResult(await _conditionService.DeleteCondition(conditionId, cancellationToken));

    [HttpGet("{conditionId}")]
    //[HasPermission("condition:read")]
    public async Task<ActionResult> GetCondition(Guid conditionId, CancellationToken cancellationToken)
        => HandleResult(await _conditionService.GetCondition(conditionId, cancellationToken));

    [HttpGet]
    //[HasPermission("condition:read:many")]
    public async Task<ActionResult> GetCountries(CancellationToken cancellationToken)
        => HandleResult(await _conditionService.GetConditions(cancellationToken));

    [HttpGet("Options")]
    //[HasPermission("condition:selectlist")]
    public async Task<ActionResult> GetSelectList(CancellationToken cancellationToken)
        => HandleResult(await _conditionService.GetSelectList(cancellationToken));
}
