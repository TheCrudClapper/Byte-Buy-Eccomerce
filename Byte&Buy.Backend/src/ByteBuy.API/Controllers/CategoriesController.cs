using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Category;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController 
    : CrudControllerBase<Guid, CategoryAddRequest, CategoryUpdateRequest, CategoryResponse>
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService) : base(categoryService)
        => _categoryService = categoryService;

    [HttpPost]
    //[HasPermission("category:create")]
    public override Task<ActionResult<CreatedResponse>> PostAsync(CategoryAddRequest request)
        => base.PostAsync(request);

    [HttpPut("{id}")]
    //[HasPermission("category:update")]
    public override Task<ActionResult<UpdatedResponse>> PutAsync(Guid id, CategoryUpdateRequest request) 
        => base.PutAsync(id, request);

    [HttpDelete("{id}")]
    //[HasPermission("category:delete")]
    public override Task<IActionResult> DeleteAsync(Guid id) 
        => base.DeleteAsync(id);

    [HttpGet("{id}")]
    //[HasPermission("category:read")]
    public override Task<ActionResult<CategoryResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken) 
        => base.GetByIdAsync(id, cancellationToken);

    [HttpGet("list")]
    //[HasPermission("category:read:many")]
    public async Task<ActionResult> GetCategoriesList(CancellationToken ct)
        => HandleResult(await _categoryService.GetCategoriesListAsync(ct));

    [HttpGet("options")]
    //[HasPermission("category:read:options")]
    public async Task<ActionResult> GetSelectList(CancellationToken ct)
        => HandleResult(await _categoryService.GetSelectListAsync(ct));
}
