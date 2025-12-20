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
public class ItemsController 
    : CrudControllerBase<Guid, ItemAddRequest, ItemUpdateRequest, ItemResponse>
{
    private readonly IItemsService _itemsService;
    public ItemsController(IItemsService itemsService) : base(itemsService)
        => _itemsService = itemsService;

    [HttpGet("list")]
    public async Task<ActionResult<IReadOnlyCollection<ItemListResponse>>> GetCompanyItemsList(CancellationToken ct)
      => HandleResult(await _itemsService.GetCompanyItemsListAsync(ct));
}
