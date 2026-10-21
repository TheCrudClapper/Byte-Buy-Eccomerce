using Microsoft.AspNetCore.Authorization;

namespace ByteBuy.API.Controllers.Public;

[Route("api/categories")]
[ApiController]
[AllowAnonymous]
public class PublicCategoriesController : BaseApiController
{
    private readonly ICategoryService _categoryService;

    public PublicCategoriesController(ICategoryService categoryService)
        => _categoryService = categoryService;

    [HttpGet("options")]
    public async Task<ActionResult<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectListAsync(CancellationToken ct)
    => HandleResult(await _categoryService.GetSelectListAsync(ct));
}
