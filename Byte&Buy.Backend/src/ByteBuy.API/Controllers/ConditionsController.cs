using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Condition;
using ByteBuy.Core.DTO.Country;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ConditionsController 
    : CrudControllerBase<Guid, ConditionAddRequest, ConditionUpdateRequest, ConditionResponse>
{
    private readonly IConditionService _conditionService;
    public ConditionsController(IConditionService conditionService) : base(conditionService)
        => _conditionService = conditionService;

    [HttpPost]
    //[HasPermission("condition:write")]
    public override Task<ActionResult<CreatedResponse>> PostAsync(ConditionAddRequest request)
        => base.PostAsync(request);

    [HttpPut("{id}")]
    //[HasPermission("condition:update")]
    public override Task<ActionResult<UpdatedResponse>> PutAsync(Guid id, ConditionUpdateRequest request)
        => base.PutAsync(id, request);

    [HttpDelete("{id}")]
    //[HasPermission("condition:delete")]
    public override Task<IActionResult> DeleteAsync(Guid id)
        => base.DeleteAsync(id);

    [HttpGet("{id}")]
    //[HasPermission("condition:read")]
    public override Task<ActionResult<ConditionResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => base.GetByIdAsync(id, cancellationToken);

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
