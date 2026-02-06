using ByteBuy.Services.DTO.OrderDelivery;

namespace ByteBuy.UI.ViewModels.Order.OrderDelivery;

public sealed class ParcelLockerDeliveryViewModel : OrderDeliveryDetailsViewModel
{
    public string ParcelLockerId { get; }
    public ParcelLockerDeliveryViewModel(ParcelLockerDeliveryDetails dto) : base(dto)
    {
        ParcelLockerId = dto.ParcelLockerId;
    }
}
