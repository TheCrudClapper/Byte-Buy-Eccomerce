using ByteBuy.Services.DTO.OrderDelivery;

namespace ByteBuy.UI.ViewModels.Order.OrderDelivery;

public class PickupPointDeliveryViewModel : OrderDeliveryDetailsViewModel
{
    public string PickupPointName { get; }
    public string PickupPointId { get; }
    public string PickupStreet { get; }
    public string PickupCity { get; }
    public string PickupLocalNumber { get; }

    public PickupPointDeliveryViewModel(PickupPointDeliveryDetails dto) : base(dto)
    {
        PickupCity = dto.PickupCity;
        PickupLocalNumber = dto.PickupLocalNumber;
        PickupStreet = dto.PickupStreet;
        PickupPointId = dto.PickupPointId;
        PickupPointName = dto.PickupPointName;
    }
}
