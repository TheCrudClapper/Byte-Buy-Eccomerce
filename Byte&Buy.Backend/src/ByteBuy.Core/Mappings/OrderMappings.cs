using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.Internal.Order;
using ByteBuy.Core.DTO.Internal.Order.Enum;
using ByteBuy.Core.DTO.Public.ImageThumbnail;
using ByteBuy.Core.DTO.Public.Money;
using ByteBuy.Core.DTO.Public.Order.Common;
using ByteBuy.Core.DTO.Public.Order.Rent;
using ByteBuy.Core.DTO.Public.Order.Sale;
using System.Linq.Expressions;

namespace ByteBuy.Core.Mappings;

public static class OrderMappings
{
    public static Expression<Func<Order, UserOrderListQuery>> UserOrderListQueryProjection
        => o => new UserOrderListQuery(
              o.Id,
              o.Status,
              o.DateCreated,
              o.SellerSnapshot.DisplayName,
              o.Lines.Count,
              new MoneyDto(o.LinesTotal.Amount, o.LinesTotal.Currency),
              new MoneyDto(o.Delivery.Price.Amount, o.Delivery.Price.Currency),
              new MoneyDto(o.Total.Amount, o.Total.Currency),
              o.Lines.Select(line => new UserOrderLineQuery(
                  line.ItemName,

                   line is RentOrderLine
                    ? OrderLineType.Rent
                    : OrderLineType.Sale,

                   line.Quantity,

                  new MoneyDto(line.TotalPrice.Amount, line.TotalPrice.Currency),
                  new ImageThumbnailDto(line.Thumbnail.ImagePath, line.Thumbnail.AltText),

                  line is RentOrderLine
                    ? null
                    : ((SaleOrderLine)line).PricePerItem.ToMoneyDto(),

                  line is RentOrderLine
                    ? ((RentOrderLine)line).PricePerDay.ToMoneyDto()
                    : null,

                  line is RentOrderLine
                    ? ((RentOrderLine)line).RentalDays
                    : null
                 )).ToList()
        );

    public static UserOrderListResponse ToUserOrderListResponse(this UserOrderListQuery query)
    {
        return new UserOrderListResponse(
            query.OrderId,
            query.Status,
            query.PurchasedDate,
            query.SellerDisplayName,
            query.LinesCount,
            query.TotalLinesCost,
            query.DeliveryCost,
            query.TotalCost,
            query.Lines.Select(line => line.Type switch 
            {
                OrderLineType.Sale => (UserOrderLineResponse)new UserSaleOrderLineResponse()
                {
                    ItemTitle = line.ItemTitle,
                    Quantity = line.Quantity,
                    Thumbnail = line.Thumbnail,
                    Total = line.Total,
                    PricePerItem = line.PricePerItem!,
                },
                OrderLineType.Rent => (UserOrderLineResponse)new UserRentOrderLineResponse()
                {
                    ItemTitle = line.ItemTitle,
                    Quantity = line.Quantity,
                    Thumbnail = line.Thumbnail,
                    Total = line.Total,
                    PricePerDay = line.PricePerDay!,
                    RentalDays = line.RentalDays!.Value,
                },
                _ => throw new ArgumentOutOfRangeException(nameof(line), $"Unsupported line type: {line.Type}")
            })
            .ToList());
    }
}
