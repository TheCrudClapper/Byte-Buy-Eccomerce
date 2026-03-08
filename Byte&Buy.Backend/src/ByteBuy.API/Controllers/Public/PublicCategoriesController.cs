using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers.Public;

[Route("api/categories")]
[ApiController]
public class PublicCategoriesController : BaseApiController
{
    private readonly ICategoryService _categoryService;

    public PublicCategoriesController(ICategoryService categoryService)
        => _categoryService = categoryService;

    [HttpGet("options")]
    public async Task<ActionResult<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectListAsync(CancellationToken ct)
    => HandleResult(await _categoryService.GetSelectListAsync(ct));
}
