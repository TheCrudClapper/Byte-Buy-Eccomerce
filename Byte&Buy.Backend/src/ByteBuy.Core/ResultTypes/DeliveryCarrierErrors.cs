namespace ByteBuy.Core.ResultTypes;

public static class DeliveryCarrierErrors
{
    public static readonly Error NotFound = new Error(
        404, "Delivery Carrier was not found");

    public static readonly Error AlreadyExists = new Error
       (400, "Delivery Carrier with this name or code already exists");

    public static readonly Error InUse = new Error
      (400, "Delivery Carrier is used, cannot be deleted");
}
