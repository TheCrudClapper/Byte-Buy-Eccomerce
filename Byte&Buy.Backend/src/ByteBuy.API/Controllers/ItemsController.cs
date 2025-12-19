using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Item;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ItemsController : BaseApiController
{
    private readonly IItemsService _itemsService;
    public ItemsController(IItemsService itemsService)
        => _itemsService = itemsService;

    [HttpGet("list")]
    public async Task<ActionResult<IReadOnlyCollection<ItemListResponse>>> GetCompanyItemsList(CancellationToken ct)
        => HandleResult(await _itemsService.GetCompanyItemsList(ct));

    [HttpPost]
    public async Task<ActionResult<CreatedResponse>> PostItem([FromForm] ItemAddRequest request)
        => HandleResult(await _itemsService.AddCompanyItem(request));

    [HttpPut("{itemId}")]
    public async Task<ActionResult<UpdatedResponse>> PutItem(Guid itemId, [FromForm] ItemUpdateRequest request)
        => HandleResult(await _itemsService.UpdateCompanyItem(itemId, request));

    [HttpDelete("{itemId}")]
    public async Task<IActionResult> DeleteItem(Guid itemId)
        => HandleResult(await _itemsService.DeleteCompanyItem(itemId));

    [HttpGet("{itemId}")]
    public async Task<ActionResult<ItemResponse>> GetItem(Guid itemId, CancellationToken ct)
        => HandleResult(await _itemsService.GetCompanyItem(itemId, ct));
}
