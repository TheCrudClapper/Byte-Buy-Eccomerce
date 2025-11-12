using Microsoft.AspNetCore.Authorization;

namespace ByteBuy.API.Attributes;

public class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(string permission)
    {
        Policy = permission;
    }
}
