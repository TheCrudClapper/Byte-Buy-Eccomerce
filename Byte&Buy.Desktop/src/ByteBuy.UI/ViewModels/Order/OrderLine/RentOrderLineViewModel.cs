using ByteBuy.Services.DTO.Order.OrderLine;
using ByteBuy.UI.ViewModels.Shared;

namespace ByteBuy.UI.ViewModels.Order.OrderLine;

public sealed class RentOrderLineViewModel : OrderLineViewModel
{
    public MoneyViewModel PricePerDay { get; }
    public int RentalDays { get; }

    public RentOrderLineViewModel(UserRentOrderLineResponse dto)
        : base(dto)
    {
        PricePerDay = new MoneyViewModel(dto.PricePerDay);
        RentalDays = dto.RentalDays;
    }
}
