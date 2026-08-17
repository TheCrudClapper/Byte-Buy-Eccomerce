using ByteBuy.Core.DTO.Public.Category;
using ByteBuy.Core.Filtration.Category;

namespace ByteBuy.API.Controllers.Company;

[Resource("company-categories")]
[Route("api/company/categories")]
[ApiController]
public class CompanyCategoriesController
    : CrudControllerBase<Guid, CategoryAddRequest, CategoryUpdateRequest, CategoryResponse>
{
    private readonly ICategoryService _categoryService;

    public CompanyCategoriesController(ICategoryService categoryService) : base(categoryService)
        => _categoryService = categoryService;

    [HttpGet("list")]
    [HasPermission("{resource}:read:many")]
    public async Task<ActionResult<PagedList<CategoryListResponse>>> GetCategoriesList([FromQuery] CategoryListQuery queryParams, CancellationToken ct)
        => HandleResult(await _categoryService.GetCategoriesListAsync(queryParams, ct));
}
