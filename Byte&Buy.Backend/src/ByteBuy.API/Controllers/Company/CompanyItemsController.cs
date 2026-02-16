using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Item;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ServiceContracts;
using ByteBuy.Services.Filtration;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers.Company;

[Resource("company-items")]
[Route("api/company/items")]
[ApiController]
public class CompanyItemsController : BaseApiController
{
    private readonly IItemsService _itemsService;
    public CompanyItemsController(IItemsService itemsService)
        => _itemsService = itemsService;

    [HttpPost]
    //[HasPermission("{resource}.create")]
    public virtual async Task<ActionResult<CreatedResponse>> PostAsync([FromForm] ItemAddRequest request)
        => HandleResult(await _itemsService.AddAsync(request));

    [HttpPut("{id:guid}")]
    public virtual async Task<ActionResult<UpdatedResponse>> PutAsync(Guid id, [FromForm] ItemUpdateRequest request)
        => HandleResult(await _itemsService.UpdateAsync(id, request));

    [HttpDelete("{id:guid}")]
    public virtual async Task<IActionResult> DeleteAsync(Guid id)
        => HandleResult(await _itemsService.DeleteAsync(id));

    [HttpGet("{id:guid}")]
    //[HasPermission("{resource}:read")]
    public virtual async Task<ActionResult<ItemResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => HandleResult(await _itemsService.GetByIdAsync(id, cancellationToken));

    [HttpGet("list")]
    public async Task<ActionResult<PagedList<ItemListResponse>>> GetItemsList([FromQuery] ItemListQuery queryParams, CancellationToken ct)
      => HandleResult(await _itemsService.GetListAsync(queryParams, ct));
}
