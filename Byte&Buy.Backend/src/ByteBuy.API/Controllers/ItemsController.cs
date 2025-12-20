using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Item;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ItemsController 
    : CrudControllerBase<Guid, ItemAddRequest, ItemUpdateRequest, ItemResponse>
{
    private readonly IItemsService _itemsService;
    public ItemsController(IItemsService itemsService) : base(itemsService)
        => _itemsService = itemsService;


    [HttpPost]
    public override Task<ActionResult<CreatedResponse>> PostAsync(ItemAddRequest request)
        => base.PostAsync(request);

    [HttpPut("{id}")]
    public override Task<ActionResult<UpdatedResponse>> PutAsync(Guid id, ItemUpdateRequest request) 
        => base.PutAsync(id, request);

    [HttpDelete("{id}")]
    public override Task<IActionResult> DeleteAsync(Guid id) 
        => base.DeleteAsync(id);

    [HttpGet("{id}")]
    public override Task<ActionResult<ItemResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken) 
        => base.GetByIdAsync(id, cancellationToken);

    [HttpGet("list")]
    public async Task<ActionResult<IReadOnlyCollection<ItemListResponse>>> GetCompanyItemsList(CancellationToken ct)
      => HandleResult(await _itemsService.GetCompanyItemsListAsync(ct));
}
