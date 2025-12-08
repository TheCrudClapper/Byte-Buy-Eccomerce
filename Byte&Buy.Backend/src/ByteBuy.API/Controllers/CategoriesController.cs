using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Category;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : BaseApiController
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
        => _categoryService = categoryService;

    [HttpPost]
    //[HasPermission("category:create")]
    public async Task<ActionResult<CreatedResponse>> PostCategory(CategoryAddRequest request)
        => HandleResult(await _categoryService.AddCategory(request));

    [HttpPut("{categoryId}")]
    //[HasPermission("category:update")]
    public async Task<ActionResult<UpdatedResponse>> PutCategory(Guid categoryId, CategoryUpdateRequest request, CancellationToken ct)
        => HandleResult(await _categoryService.UpdateCategory(categoryId, request));

    [HttpDelete("{categoryId}")]
    //[HasPermission("category:delete")]
    public async Task<IActionResult> DeleteCategory(Guid categoryId, CancellationToken ct)
        => HandleResult(await _categoryService.DeleteCategory(categoryId));

    [HttpGet("{categoryId}")]
    //[HasPermission("category:read")]
    public async Task<ActionResult<CategoryResponse>> GetCategory(Guid categoryId, CancellationToken ct)
        => HandleResult(await _categoryService.GetCategory(categoryId, ct));

    [HttpGet]
    //[HasPermission("category:read:many")]
    public async Task<ActionResult> GetCategoriesList(CancellationToken ct)
        => HandleResult(await _categoryService.GetCategoriesList(ct));

    [HttpGet("options")]
    //[HasPermission("category:read:options")]
    public async Task<ActionResult> GetSelectList(CancellationToken ct)
        => HandleResult(await _categoryService.GetSelectList(ct));
}
