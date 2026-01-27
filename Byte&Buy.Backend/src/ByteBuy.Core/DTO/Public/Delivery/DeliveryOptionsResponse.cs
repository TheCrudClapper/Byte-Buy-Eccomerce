namespace ByteBuy.Core.DTO.Public.Delivery;

public class DeliveryOptionsResponse
{
    public IReadOnlyCollection<DeliveryOptionResponse> ParcelLockerDeliveries { get; set; } = null!;
    public IReadOnlyCollection<DeliveryOptionResponse> CourierDeliveries { get; set; } = null!;
    public IReadOnlyCollection<DeliveryOptionResponse> PickupPointDeliveries { get; set; } = null!;
}
