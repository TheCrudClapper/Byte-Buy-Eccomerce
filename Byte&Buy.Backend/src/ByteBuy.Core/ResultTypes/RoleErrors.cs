namespace ByteBuy.Core.ResultTypes;

public class RoleErrors
{
    public static readonly Error RoleAlreadyExist = new Error(
        409, "Role of given name already exitst");
}
