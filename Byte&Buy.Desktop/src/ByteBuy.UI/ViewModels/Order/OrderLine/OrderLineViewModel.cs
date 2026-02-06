using ByteBuy.Services.DTO.Order.OrderLine;
using ByteBuy.UI.ViewModels.Shared;
using System;

namespace ByteBuy.UI.ViewModels.Order.OrderLine;

public abstract class OrderLineViewModel
{
    public string ItemTitle { get; }
    public int Quantity { get; }
    public MoneyViewModel Total { get; }
    public ImageThumbnailViewModel Thumbnail { get; }

    protected OrderLineViewModel(UserOrderLineResponse dto)
    {
        ItemTitle = dto.ItemTitle;
        Quantity = dto.Quantity;
        Total = new MoneyViewModel(dto.Total);
        Thumbnail = new ImageThumbnailViewModel(dto.Thumbnail.ImagePath);
    }

    public static OrderLineViewModel From(UserOrderLineResponse response) =>
            response switch
            {
                UserSaleOrderLineResponse sale => new SaleOrderLineViewModel(sale),
                UserRentOrderLineResponse rent => new RentOrderLineViewModel(rent),
                _ => throw new NotSupportedException(),
            };

}
