using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.PortalUser;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PortalUsersController 
    : CrudControllerBase<Guid, PortalUserAddRequest, PortalUserUpdateRequest, PortalUserResponse>
{
    private readonly IPortalUserService _portalUserService;
    public PortalUsersController(IPortalUserService portalUserService) : base(portalUserService)
       => _portalUserService = portalUserService;

    [HttpPost]
    public override Task<ActionResult<CreatedResponse>> PostAsync(PortalUserAddRequest request) 
        => base.PostAsync(request);

    [HttpDelete("{id}")]
    public override Task<IActionResult> DeleteAsync(Guid id) 
        => base.DeleteAsync(id);

    [HttpPut("{id}")]
    public override Task<ActionResult<UpdatedResponse>> PutAsync(Guid id, PortalUserUpdateRequest request) 
        => base.PutAsync(id, request);

    [HttpGet("{id}")]
    public override Task<ActionResult<PortalUserResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken) 
        => base.GetByIdAsync(id, cancellationToken);

    [HttpGet("list")]
    public async Task<ActionResult<IEnumerable<PortalUserListResponse>>> GetPortalUsersList(CancellationToken ct)
        => HandleResult(await _portalUserService.GetPortalUsersListAsync(ct));

}
