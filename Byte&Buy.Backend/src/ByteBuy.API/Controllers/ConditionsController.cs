using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Condition;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Resource("conditions")]
[Route("api/[controller]")]
[ApiController]
public class ConditionsController 
    : CrudControllerBase<Guid, ConditionAddRequest, ConditionUpdateRequest, ConditionResponse>
{
    private readonly IConditionService _conditionService;
    public ConditionsController(IConditionService conditionService) : base(conditionService)
        => _conditionService = conditionService;

    [HttpGet("list")]
    //[HasPermission("category:read:many")]
    public async Task<ActionResult> GetCategoriesList(CancellationToken ct)
       => HandleResult(await _conditionService.GetConditionsListAsync(ct));

    [HttpGet]
    //[HasPermission("condition:read:many")]
    public async Task<ActionResult<IEnumerable<ConditionResponse>>> GetConditions(CancellationToken ct)
        => HandleResult(await _conditionService.GetConditionsAsync(ct));

    [HttpGet("options")]
    //[HasPermission("condition:read:options")]
    public async Task<ActionResult<IEnumerable<SelectListItemResponse<Guid>>>> GetSelectList(CancellationToken ct)
        => HandleResult(await _conditionService.GetSelectListAsync(ct));
}
