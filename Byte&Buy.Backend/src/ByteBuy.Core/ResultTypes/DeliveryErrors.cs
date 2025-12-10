namespace ByteBuy.Core.ResultTypes;

public static class DeliveryErrors
{
    public static readonly Error AlreadyExists = new Error
        (400, "Delivery with this name already exists");

    public static readonly Error InUse = new Error
      (400, "Delivery is used, cannot be deleted");
}
