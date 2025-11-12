using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ByteBuy.API.Authorization;

public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IPermissionService _permissionService;
    public PermissionHandler(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId is null)
            return;

        if (!Guid.TryParse(userId, out var userIdGuid))
            return;

        if (await _permissionService.HasPermissionAsync(userIdGuid, requirement.Permission))
            context.Succeed(requirement);
    }
}
