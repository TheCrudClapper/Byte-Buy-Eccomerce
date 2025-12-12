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
}
