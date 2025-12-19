namespace ByteBuy.Services.DTO.Delivery;

public class DeliveryOptionsResponse
{
    public IEnumerable<DeliveryOptionResponse> ParcelLockerDeliveries { get; set; } = null!;
    public IEnumerable<DeliveryOptionResponse> CourierDeliveries { get; set; } = null!;
    public IEnumerable<DeliveryOptionResponse> PickupPointDeliveries { get; set; } = null!;
}

