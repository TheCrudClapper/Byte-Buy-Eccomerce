using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.Domain.Exceptions;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.DTO.Public.Order;
using ByteBuy.Core.DTO.Public.Order.Common;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;
using static ByteBuy.Core.Specification.OrderSpecifications;

namespace ByteBuy.Core.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IRentalRepository _rentalRepository;
    public OrderService(IOrderRepository orderRepository,
        ICompanyRepository companyRepository,
        IRentalRepository rentalRepository)
    {
        _orderRepository = orderRepository;
        _companyRepository = companyRepository;
        _rentalRepository = rentalRepository;
    }

    public async Task<Result<UpdatedResponse>> CancelOrder(Guid userId, Guid orderId)
    {
        var order = await _orderRepository.GetUserOrder(userId, orderId);
        if (order is null)
            return Result.Failure<UpdatedResponse>(OrderErrors.NotFound);

        var cancelationResult = order.CancelOrder();
        if (cancelationResult.IsFailure)
            return Result.Failure<UpdatedResponse>(cancelationResult.Error);

        await _orderRepository.UpdateAsync(order);
        await _orderRepository.CommitAsync();

        return order.ToUpdatedResponse();
    }

    public async Task<Result<UpdatedResponse>> DeliverOrder(Guid sellerId, Guid orderId)
    {
        var order = await _orderRepository.GetSellerOrder(sellerId, orderId);
        if (order is null)
            return Result.Failure<UpdatedResponse>(OrderErrors.NotFound);

        var deliveryResult = order.DeliverOrder();

        if (deliveryResult.IsFailure)
            return Result.Failure<UpdatedResponse>(deliveryResult.Error);

        var rentOrderLines = order.Lines
            .OfType<RentOrderLine>()
            .ToList();

        if (rentOrderLines.Count > 0)
            await CreateRentals(order, rentOrderLines);

        await _orderRepository.UpdateAsync(order);
        await _orderRepository.CommitAsync();
        return order.ToUpdatedResponse();
    }

    public async Task<Result<UpdatedResponse>> ShipOrder(Guid sellerId, Guid orderId)
    {
        var order = await _orderRepository.GetSellerOrder(sellerId, orderId);
        if (order is null)
            return Result.Failure<UpdatedResponse>(OrderErrors.NotFound);

        var shippedResult = order.ShipOrder();

        if (shippedResult.IsFailure)
            return Result.Failure<UpdatedResponse>(shippedResult.Error);

        await _orderRepository.UpdateAsync(order);
        await _orderRepository.CommitAsync();

        return order.ToUpdatedResponse();
    }

    public async Task<Result<IReadOnlyCollection<UserOrderListResponse>>> GetAllUserOrdersAsync(Guid userId, CancellationToken ct = default)
    {
        var spec = new UserOrderListQuerySpec(userId);
        var queryResult = await _orderRepository.GetListBySpecAsync(spec, ct);

        return queryResult
            .Select(o => o.ToUserOrderListResponse())
            .ToList();
    }

    public async Task<Result<OrderDetailsResponse>> GetOrderDetailsAsync(Guid userId, Guid orderId, CancellationToken ct = default)
    {
        var spec = new OrderDetailsResponseSpec(userId, orderId);
        var queryResult = await _orderRepository.GetBySpecAsync(spec, ct);

        return queryResult is null
            ? Result.Failure<OrderDetailsResponse>(OrderErrors.NotFound)
            : queryResult.ToOrderDetailResponse();
    }

    public async Task<Result<UpdatedResponse>> ReturnOrder(Guid userId, Guid orderId)
    {
        var order = await _orderRepository.GetUserOrder(userId, orderId);
        if (order is null)
            return Result.Failure<UpdatedResponse>(OrderErrors.NotFound);

        var returnResult = order.ReturnOrder();
        if (returnResult.IsFailure)
            return Result.Failure<UpdatedResponse>(returnResult.Error);

        await _orderRepository.UpdateAsync(order);
        await _orderRepository.CommitAsync();

        return order.ToUpdatedResponse();
    }

    public async Task<Result<IReadOnlyCollection<UserOrderListResponse>>> GetSellerOrdersAsync(Guid sellerId, CancellationToken ct = default)
    {
        var spec = new SellerOrdersListQuerySpec(sellerId);
        var queryResult = await _orderRepository.GetListBySpecAsync(spec, ct);

        return queryResult
            .Select(o => o.ToUserOrderListResponse())
            .ToList();
    }

    public async Task<Result<IReadOnlyCollection<CompanyOrderListResponse>>> GetCompanyOrdersList(CancellationToken ct = default)
    {
        var companyId = await _companyRepository.GetCompanyId(ct);

        var spec = new CompanyOrderListResponseSpec(companyId);
        return await _orderRepository.GetListBySpecAsync(spec, ct);
    }

    /// <summary>
    /// Method that creates rentals per each rent order line found in given order
    /// </summary>
    /// <param name="order"></param>
    /// <param name="rentLines"></param>
    /// <returns></returns>
    /// <exception cref="DomainInvariantException"></exception>
    private async Task CreateRentals(Order order, List<RentOrderLine> rentLines)
    {
        if (order.DateDelivered == null)
            throw new DomainInvariantException($"{order.Id} is marked as delivered but delivery date is null");

        var deliveryDate = order.DateDelivered.Value;

        foreach (var line in rentLines)
        {
            var creationResult = Rental.CreateRental(
                line.Id,
                order.BuyerId,
                line.Thumbnail.ImagePath,
                line.Thumbnail.AltText,
                line.PricePerDay.Amount,
                line.PricePerDay.Currency,
                line.ItemName,
                line.Quantity,
                order.SellerSnapshot,
                line.RentalDays,
                deliveryDate);

            if (creationResult.IsFailure)
                throw new DomainInvariantException($"Failed to create rental for order line {line.Id}: {creationResult.Error.Description}");

            await _rentalRepository.AddAsync(creationResult.Value);
        }
    }
}
