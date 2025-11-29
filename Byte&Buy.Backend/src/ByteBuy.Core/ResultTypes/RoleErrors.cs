namespace ByteBuy.Core.ResultTypes;

public class RoleErrors
{
    public static readonly Error RoleAlreadyExist = new Error(
        409, "Role of given name already exitst");

    public static readonly Error NotFound = new Error(
       404, "Role provided doesnt exists");

    public static readonly Error RoleHasActiveUsers = new Error(
        400, "Can't delete role with active users!");
}
