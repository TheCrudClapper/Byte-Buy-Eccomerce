using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Condition;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Filtration.Condition;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers.Company;

[Resource("company-conditions")]
[Route("api/company/conditions")]
[ApiController]
public class CompanyConditionsController
    : CrudControllerBase<Guid, ConditionAddRequest, ConditionUpdateRequest, ConditionResponse>
{
    private readonly IConditionService _conditionService;
    public CompanyConditionsController(IConditionService conditionService) : base(conditionService)
        => _conditionService = conditionService;

    [HttpGet("list")]
    [HasPermission("company-conditions:read:many")]
    public async Task<ActionResult<PagedList<ConditionListResponse>>> GetConditionsList([FromQuery] ConditionListQuery queryParams, CancellationToken ct)
       => HandleResult(await _conditionService.GetConditionsListAsync(queryParams, ct)); 
}
