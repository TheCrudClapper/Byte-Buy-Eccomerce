using ByteBuy.API.Attributes;
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
        if (userId is null || !Guid.TryParse(userId, out var userIdGuid))
            return;

        var permissionName = requirement.Permission;

        if (permissionName.Contains("{resource}") &&
         context.Resource is HttpContext httpContext)
        {
            var endpoint = httpContext.GetEndpoint();
            var resourceAttr = endpoint?.Metadata.GetMetadata<ResourceAttribute>();

            if (resourceAttr is null)
                return;

            permissionName = permissionName.Replace("{resource}", resourceAttr.Name);
        }

        if (await _permissionService.HasPermissionAsync(userIdGuid, permissionName))
            context.Succeed(requirement);
    }
}
