namespace ByteBuy.Core.ResultTypes;

public class RoleErrors
{
    public static readonly Error RoleAlreadyExist = new Error(
        409, "Role of given name already exitst");

    public static readonly Error NotFound = new Error(
       404, "Role provided doesnt exists");
}
