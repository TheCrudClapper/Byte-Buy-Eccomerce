namespace ByteBuy.Services.DTO.Delivery;

public class DeliveryOptionsResponse
{
    public IReadOnlyCollection<DeliveryOptionResponse> ParcelLockerDeliveries { get; set; } = null!;
    public IReadOnlyCollection<DeliveryOptionResponse> CourierDeliveries { get; set; } = null!;
    public IReadOnlyCollection<DeliveryOptionResponse> PickupPointDeliveries { get; set; } = null!;
}

