using ByteBuy.API.Controllers.Base;
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
        //[HasPermission("permission:read:options")]
        public async Task<ActionResult<SelectListItemResponse<Guid>>> GetSelectList()
            => HandleResult(await _permissionService.GetSelectListAsync());
    }
}
