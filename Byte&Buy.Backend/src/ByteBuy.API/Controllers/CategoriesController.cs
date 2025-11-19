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
    public async Task<ActionResult> PostCategory(CategoryAddRequest request, CancellationToken ct)
        => HandleResult(await _categoryService.AddCategory(request, ct));

    [HttpPut("{categoryId}")]
    //[HasPermission("category:update")]
    public async Task<ActionResult> PutCategory(Guid categoryId, CategoryUpdateRequest request, CancellationToken ct)
        => HandleResult(await _categoryService.UpdateCategory(categoryId, request, ct));

    [HttpDelete("{categoryId}")]
    //[HasPermission("category:delete")]
    public async Task<ActionResult> DeleteCategory(Guid categoryId, CancellationToken ct)
        => HandleResult(await _categoryService.DeleteCategory(categoryId, ct));

    [HttpGet("{categoryId}")]
    //[HasPermission("category:read")]
    public async Task<ActionResult> GetCategory(Guid categoryId, CancellationToken ct)
        => HandleResult(await _categoryService.GetCategory(categoryId, ct));

    [HttpGet]
    //[HasPermission("category:read:many")]
    public async Task<ActionResult> GetCategories(CancellationToken ct)
        => HandleResult(await _categoryService.GetCategories(ct));

    [HttpGet("Options")]
    //[HasPermission("category:read:options")]
    public async Task<ActionResult> GetSelectList(CancellationToken ct)
        => HandleResult(await _categoryService.GetSelectList(ct));
}
