using ByteBuy.Core.DTO;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : BaseApiController
    {
        private readonly IPermissionService _permissionService;
        public PermissionsController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet("options")]
        public async Task<ActionResult<SelectListItemResponse>> GetSelectList()
            => HandleResult(await _permissionService.GetSelectList());
    }
}
