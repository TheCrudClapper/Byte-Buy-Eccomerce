using ByteBuy.Services.DTO.Order.Enums;
using ByteBuy.UI.ModelsUI.Order;
using System;

namespace ByteBuy.UI.Mappings;

public static class OrderMappings
{
    public static OrderListItem ToListItem(this CompanyOrderListResponse dto, int index)
    {
        return new OrderListItem()
        {
            Id = dto.Id,
            RowNumber = index + 1,
            Status = dto.Status,
            BuyerEmail = dto.BuyerEmail,
            BuyerFullName = dto.BuyerFullName,
            LinesCount = dto.LinesCount,
            PurchaseDate = dto.PurchasedDate.ToLocalTime().ToString("dd/MM/yyyy"),
            TotalCost = $"{dto.TotalCost.Amount} {dto.TotalCost.Currency}"
        };
    }

    public static string MapOrderStatusIcon(OrderStatus status)
    {
        return status switch
        {
            OrderStatus.AwaitingPayment => "avares://ByteBuy.UI/Assets/Images/regular/hourglass-end-solid-full.svg",
            OrderStatus.Paid => "avares://ByteBuy.UI/Assets/Images/regular/coins-solid-full.svg",
            OrderStatus.Canceled => "avares://ByteBuy.UI/Assets/Images/regular/ban-solid-full.svg",
            OrderStatus.Delivered => "avares://ByteBuy.UI/Assets/Images/regular/truck-ramp-box-solid-full.svg",
            OrderStatus.Returned => "avares://ByteBuy.UI/Assets/Images/regular/arrow-rotate-left-solid-full.svg",
            OrderStatus.Shipped => "avares://ByteBuy.UI/Assets/Images/regular/truck-solid-full.svg",
            _ => throw new NotSupportedException()
        };
    }

    public static string MapOrderStatusText(OrderStatus status)
    {
        return status switch
        {
            OrderStatus.AwaitingPayment => "Awaiting Payment",
            OrderStatus.Paid => "Paid",
            OrderStatus.Shipped => "Shipped",
            OrderStatus.Delivered => "Delivered",
            OrderStatus.Canceled => "Cancelled",
            OrderStatus.Returned => "Returned",
            _ => "Unknown"
        };
    }
}
