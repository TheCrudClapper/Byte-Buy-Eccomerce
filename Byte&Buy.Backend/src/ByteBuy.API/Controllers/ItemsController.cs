using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Item;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Resource("items")]
[Route("api/[controller]")]
[ApiController]
public class ItemsController : BaseApiController
{
    private readonly IItemsService _itemsService;
    public ItemsController(IItemsService itemsService)
        => _itemsService = itemsService;

    [HttpPost]
    //[HasPermission("{resource}.create")]
    public virtual async Task<ActionResult<CreatedResponse>> PostAsync([FromForm] ItemAddRequest request)
        => HandleResult(await _itemsService.AddAsync(request));

    [HttpPut("{id}")]
    public virtual async Task<ActionResult<UpdatedResponse>> PutAsync(Guid id, [FromForm] ItemUpdateRequest request)
        => HandleResult(await _itemsService.UpdateAsync(id, request));

    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> DeleteAsync(Guid id)
        => HandleResult(await _itemsService.DeleteAsync(id));

    [HttpGet("{id}")]
    //[HasPermission("{resource}:read")]
    public virtual async Task<ActionResult<ItemResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => HandleResult(await _itemsService.GetByIdAsync(id, cancellationToken));

    [HttpGet("list")]
    public async Task<ActionResult<IReadOnlyCollection<ItemListResponse>>> GetCompanyItemsList(CancellationToken ct)
      => HandleResult(await _itemsService.GetCompanyItemsListAsync(ct));
}
