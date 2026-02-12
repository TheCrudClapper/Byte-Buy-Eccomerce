using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Category;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Filtration.Category;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Authorize]
[Resource("categories")]
[Route("api/[controller]")]
[ApiController]
public class CategoriesController
    : CrudControllerBase<Guid, CategoryAddRequest, CategoryUpdateRequest, CategoryResponse>
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService) : base(categoryService)
        => _categoryService = categoryService;

    [HttpGet("list")]
    //[HasPermission("category:read:many")]
    public async Task<ActionResult<PagedList<CategoryListResponse>>> GetCategoriesList([FromQuery] CategoryListQuery queryParams, CancellationToken ct)
        => HandleResult(await _categoryService.GetCategoriesListAsync(queryParams, ct));

    [HttpGet("options")]
    //[HasPermission("category:read:options")]
    public async Task<ActionResult<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectList(CancellationToken ct)
        => HandleResult(await _categoryService.GetSelectListAsync(ct));
}
