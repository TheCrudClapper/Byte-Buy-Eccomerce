using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.DTO.Internal.Order;
using ByteBuy.Core.DTO.Internal.Order.Enum;
using ByteBuy.Core.DTO.Internal.OrderDelivery;
using ByteBuy.Core.DTO.Public.AddressValueObj;
using ByteBuy.Core.DTO.Public.ImageThumbnail;
using ByteBuy.Core.DTO.Public.Money;
using ByteBuy.Core.DTO.Public.Order;
using ByteBuy.Core.DTO.Public.Order.OrderLine;
using ByteBuy.Core.DTO.Public.OrderDelivery;
using ByteBuy.Core.DTO.Public.PortalUser;
using ByteBuy.Core.Pagination;
using System.Linq.Expressions;

namespace ByteBuy.Core.Mappings;

public static class OrderMappings
{
    public static Expression<Func<Order, UserOrderListQueryModel>> UserOrderListQueryModelProjection
        => o => new UserOrderListQueryModel(
              o.Id,
              o.Status,
              o.DateCreated,
              o.SellerSnapshot.DisplayName,
              o.BuyerSnapshot.FullName,
              o.Lines.Count,
              new MoneyDto(o.LinesTotal.Amount, o.LinesTotal.Currency),
              new MoneyDto(o.Delivery.Price.Amount, o.Delivery.Price.Currency),
              new MoneyDto(o.Total.Amount, o.Total.Currency),
              o.Lines.Select(line => new UserOrderLineQueryModel(
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

    public static UserOrderListResponse ToUserOrderListResponse(this UserOrderListQueryModel query)
    {
        return new UserOrderListResponse(
            query.OrderId,
            query.Status,
            query.PurchasedDate,
            query.SellerDisplayName,
            query.BuyerDisplayName,
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

    public static Expression<Func<Order, OrderDetailsQueryModel>> OrderDetailsQueryModelProjection
      => o => new OrderDetailsQueryModel(
          o.Id,
          o.Status == OrderStatus.AwaitingPayment ? o.Payment.PaymentId : null,
          o.Status,
          o.DateCreated,
          o.DateDelivered,
          o.SellerSnapshot.DisplayName,
          o.Lines.Count,
          new MoneyDto(o.LinesTotal.Amount, o.LinesTotal.Currency),
          new MoneyDto(o.Total.Amount, o.Total.Currency),

          new OrderDeliveryQueryModel(
              o.Delivery.CarrierCode,
              o.Delivery.DeliveryName,
              o.Delivery.Channel,
              new MoneyDto(o.Delivery.Price.Amount, o.Delivery.Price.Currency),

              o.Delivery.Channel == DeliveryChannel.Courier ? o.Delivery.Street : null,
              o.Delivery.Channel == DeliveryChannel.Courier ? o.Delivery.HouseNumber : null,
              o.Delivery.Channel == DeliveryChannel.Courier ? o.Delivery.FlatNumber : null,
              o.Delivery.Channel == DeliveryChannel.Courier ? o.Delivery.City : null,
              o.Delivery.Channel == DeliveryChannel.Courier ? o.Delivery.PostalCity : null,
              o.Delivery.Channel == DeliveryChannel.Courier ? o.Delivery.PostalCode : null,


              o.Delivery.Channel == DeliveryChannel.PickupPoint ? o.Delivery.PickupPointName : null,
              o.Delivery.Channel == DeliveryChannel.PickupPoint ? o.Delivery.PickupPointId : null,
              o.Delivery.Channel == DeliveryChannel.PickupPoint ? o.Delivery.PickupStreet : null,
              o.Delivery.Channel == DeliveryChannel.PickupPoint ? o.Delivery.PickupCity : null,
              o.Delivery.Channel == DeliveryChannel.PickupPoint ? o.Delivery.PickupLocalNumber : null,

              o.Delivery.Channel == DeliveryChannel.ParcelLocker ? o.Delivery.ParcelLockerId : null
          ),
          new BuyerSnapshotQueryModel(
              o.BuyerSnapshot.FullName,
              o.BuyerSnapshot.Email,
              o.BuyerSnapshot.PhoneNumber!,
              new HomeAddressDto(
                  o.BuyerSnapshot.Address.Street,
                  o.BuyerSnapshot.Address.HouseNumber,
                  o.BuyerSnapshot.Address.PostalCity,
                  o.BuyerSnapshot.Address.PostalCode,
                  o.BuyerSnapshot.Address.City,
                  o.BuyerSnapshot.Address.Country,
                  o.BuyerSnapshot.Address.FlatNumber
              )
          ),
          o.Lines.Select(line => new UserOrderLineQueryModel(
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
          ))
          .ToList()
      );

    public static OrderDetailsResponse ToOrderDetailResponse(this OrderDetailsQueryModel query)
    {
        OrderDeliveryDetails deliveryDto;

        if (query.DeliveryQuery.Channel == DeliveryChannel.Courier)
        {
            deliveryDto = new CourierDeliveryDetails(
                query.DeliveryQuery.Street!,
                query.DeliveryQuery.HouseNumber!,
                query.DeliveryQuery.FlatNumber!,
                query.DeliveryQuery.City!,
                query.DeliveryQuery.PostalCity!,
                query.DeliveryQuery.PostalCode!
            )
            {
                CarrierCode = query.DeliveryQuery.CarrierCode,
                DeliveryName = query.DeliveryQuery.DeliveryName,
                Channel = query.DeliveryQuery.Channel,
                Price = query.DeliveryQuery.Price
            };
        }
        else if (query.DeliveryQuery.Channel == DeliveryChannel.PickupPoint)
        {
            deliveryDto = new PickupPointDeliveryDetails(
                query.DeliveryQuery.PickupPointName!,
                query.DeliveryQuery.PickupPointId!,
                query.DeliveryQuery.PickupStreet!,
                query.DeliveryQuery.PickupCity!,
                query.DeliveryQuery.PickupLocalNumber!
            )
            {
                CarrierCode = query.DeliveryQuery.CarrierCode,
                DeliveryName = query.DeliveryQuery.DeliveryName,
                Channel = query.DeliveryQuery.Channel,
                Price = query.DeliveryQuery.Price
            };

        }
        else
        {
            deliveryDto = new ParcelLockerDeliveryDetails(
                query.DeliveryQuery.ParcelLockerId!
            )
            {
                CarrierCode = query.DeliveryQuery.CarrierCode,
                DeliveryName = query.DeliveryQuery.DeliveryName,
                Channel = query.DeliveryQuery.Channel,
                Price = query.DeliveryQuery.Price
            };

        }

        var linesDto = query.Lines.Select(line => line.Type switch
        {
            OrderLineType.Sale => (UserOrderLineResponse)new UserSaleOrderLineResponse
            {
                ItemTitle = line.ItemTitle,
                Quantity = line.Quantity,
                Thumbnail = line.Thumbnail,
                Total = line.Total,
                PricePerItem = line.PricePerItem!
            },
            OrderLineType.Rent => (UserOrderLineResponse)new UserRentOrderLineResponse
            {
                ItemTitle = line.ItemTitle,
                Quantity = line.Quantity,
                Thumbnail = line.Thumbnail,
                Total = line.Total,
                PricePerDay = line.PricePerDay!,
                RentalDays = line.RentalDays!.Value
            },
            _ => throw new ArgumentOutOfRangeException(nameof(line.Type), "Unsupported line type")
        })
        .ToList();

        return new OrderDetailsResponse(
            query.OrderId,
            query.PaymentId,
            query.Status,
            query.PurchasedDate,
            query.DateDelivered,
            query.SellerDisplayName,
            query.LinesCount,
            query.TotalLinesCost,
            query.TotalCost,
            deliveryDto,
            new BuyerSnapshotResponse(
                query.BuyerDetailsQuery.FullName,
                query.BuyerDetailsQuery.Email,
                query.BuyerDetailsQuery.PhoneNumber,
                query.BuyerDetailsQuery.Address
            ),
            linesDto);
    }

    public static Expression<Func<Order, CompanyOrderListResponse>> CompanyOrderListProjection
        => o => new CompanyOrderListResponse(
            o.Id,
            o.Status.ToString(),
            o.DateCreated,
            o.Lines.Count,
            o.BuyerSnapshot.Email,
            o.BuyerSnapshot.FullName,
            new MoneyDto(o.Total.Amount, o.Total.Currency));

    public static Expression<Func<Order, OrderDashboardListResponse>> OrderDashboardProjection
        => o => new OrderDashboardListResponse(
            o.Id,
            o.BuyerSnapshot.Email,
            new MoneyDto(o.Total.Amount, o.Total.Currency),
            o.Lines.Count,
            o.DateCreated,
            o.Status);

    public static PagedList<UserOrderListResponse> ToResponse(this PagedList<UserOrderListQueryModel> pagedList)
        => new PagedList<UserOrderListResponse>
        {
            Items = pagedList.Items
                .Select(o => o.ToUserOrderListResponse())
                .ToList(),

            Metadata = pagedList.Metadata
        };

}
