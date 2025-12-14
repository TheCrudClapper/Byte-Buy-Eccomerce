using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Item;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ItemsController : BaseApiController
{
    private readonly IItemsService  _itemsService;
    public ItemsController(IItemsService itemsService)
        => _itemsService = itemsService;

    [HttpGet("list")]
    public async Task<ActionResult<IReadOnlyCollection<ItemListResponse>>> GetCompanyItemsList()
        => HandleResult(await _itemsService.GetCompanyItemsList());

    [HttpPost]
    public async Task<ActionResult<CreatedResponse>> PostItem([FromForm] ItemAddRequest request)
        => HandleResult(await _itemsService.AddCompanyItem(request));

    [HttpPut]
    public async Task<ActionResult<UpdatedResponse>> PutItem(Guid itemId, ItemUpdateRequest request)
        => HandleResult(await _itemsService.UpdateCompanyItem(itemId, request));

    [HttpDelete]
    public async Task<IActionResult> DeleteItem(Guid itemId)
        => HandleResult(await _itemsService.DeleteCompanyItem(itemId));
}
