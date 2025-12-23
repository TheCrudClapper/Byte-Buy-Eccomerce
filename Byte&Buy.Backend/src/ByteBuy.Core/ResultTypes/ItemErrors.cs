namespace ByteBuy.Core.ResultTypes;

public class ItemErrors
{
    public static readonly Error NotFound
        = new Error(404, "Item of given Id doesn't exists");

    public static readonly Error InUse
        = new Error(400, "This Item is used, cannot be deleted");
}
