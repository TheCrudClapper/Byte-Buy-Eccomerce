namespace ByteBuy.Core.ResultTypes;

public class ItemErrors
{
    public static readonly Error NotFound
        = new Error(404, "Item of given Id doesn't exists");
}
