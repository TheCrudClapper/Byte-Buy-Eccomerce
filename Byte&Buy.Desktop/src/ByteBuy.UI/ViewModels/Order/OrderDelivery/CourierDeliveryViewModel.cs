using ByteBuy.Services.DTO.OrderDelivery;

namespace ByteBuy.UI.ViewModels.Order.OrderDelivery;

public sealed class CourierDeliveryViewModel : OrderDeliveryDetailsViewModel
{
    public string Street { get; }
    public string HouseNumber { get; }
    public string? FlatNumber { get; }
    public string City { get; }
    public string PostalCity { get; }
    public string PostalCode { get; }

    public CourierDeliveryViewModel(CourierDeliveryDetails dto) : base(dto)
    {
        Street = dto.Street;
        HouseNumber = dto.HouseNumber;
        FlatNumber = $"/ {dto.FlatNumber}" ?? "";
        City = dto.City;
        PostalCode = dto.PostalCode;
        PostalCity = dto.PostalCity;
    }
}
