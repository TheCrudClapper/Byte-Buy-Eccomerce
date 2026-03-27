using ByteBuy.Core.Domain.Orders;
using ByteBuy.Core.Domain.Orders.Entities;
using ByteBuy.Core.Domain.Orders.Errors;
using ByteBuy.Core.Domain.Rentals;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.Domain.RepositoryContracts.UoW;
using ByteBuy.Core.Domain.Shared.Exceptions;
using ByteBuy.Core.Domain.Shared.ResultTypes;
using ByteBuy.Core.DTO.Public.Order;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Filtration.Order;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ServiceContracts;
using static ByteBuy.Core.Specification.OrderSpecifications;

namespace ByteBuy.Core.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOfferRepository _offerReposiotry;
    private readonly ICompanyRepository _companyRepository;
    private readonly IRentalRepository _rentalRepository;
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(IOrderRepository orderRepository,
        ICompanyRepository companyRepository,
        IRentalRepository rentalRepository,
        IOfferRepository offerReposiotry,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _companyRepository = companyRepository;
        _rentalRepository = rentalRepository;
        _offerReposiotry = offerReposiotry;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<UpdatedResponse>> CancelOrderAsync(Guid userId, Guid orderId)
    {
        var order = await _orderRepository.GetUserOrderAsync(userId, orderId);
        if (order is null)
            return Result.Failure<UpdatedResponse>(OrderErrors.NotFound);

        var cancelationResult = order.CancelOrder();
        if (cancelationResult.IsFailure)
            return Result.Failure<UpdatedResponse>(cancelationResult.Error);

        //get offers for quantity correction
        var offers = await _offerReposiotry
            .GetOffersByIdsAsync(order.Lines.Select(l => l.OfferId));

        var offerLookup = offers.ToDictionary(o => o.Id);

        foreach (var line in order.Lines)
        {
            if (offerLookup.TryGetValue(line.OfferId, out var offer))
            {
                offer.RestoreQuantity(line.Quantity);
            }
        }

        await _orderRepository.UpdateAsync(order);
        await _unitOfWork.SaveChangesAsync();

        return order.ToUpdatedResponse();
    }

    public async Task<Result<PagedList<UserOrderListResponse>>> GetUserOrdersAsync(UserOrderListQuery queryParams, Guid userId, CancellationToken ct = default)
    {
        var query = await _orderRepository.GetUserOrdersListAsync(queryParams, userId, ct);
        return query.ToResponse();
    }

    public async Task<Result<UpdatedResponse>> ShipOrderAsCompanyAsync(Guid orderId)
    {
        var companyId = await _companyRepository.GetCompanyId();
        return await ShipInternal(() => _orderRepository.GetSellerOrderAsync(companyId, orderId));
    }

    public async Task<Result<UpdatedResponse>> DeliverOrderAsCompanyAsync(Guid orderId)
    {
        var companyId = await _companyRepository.GetCompanyId();
        return await DeliverOrderInternal(() => _orderRepository.GetSellerOrderAsync(companyId, orderId));
    }

    public async Task<Result<UpdatedResponse>> DeliverOrderAsPrivateSellerAsync(Guid sellerId, Guid orderId)
        => await DeliverOrderInternal(() => _orderRepository.GetSellerOrderAsync(sellerId, orderId));

    public async Task<Result<UpdatedResponse>> ShipOrderAsPrivateSellerAsync(Guid sellerId, Guid orderId)
        => await ShipInternal(() => _orderRepository.GetSellerOrderAsync(sellerId, orderId));

    public async Task<Result<OrderDetailsResponse>> GetOrderDetailsAsync(Guid userId, Guid orderId, CancellationToken ct = default)
    {
        var spec = new OrderDetailsQueryModelSpec(userId, orderId);
        var queryResult = await _orderRepository.GetBySpecAsync(spec, ct);

        return queryResult is null
            ? Result.Failure<OrderDetailsResponse>(OrderErrors.NotFound)
            : queryResult.ToOrderDetailResponse();
    }

    public async Task<Result<OrderDetailsResponse>> GetCompanyOrderDetailsAsync(Guid orderId, CancellationToken ct = default)
    {
        var companyId = await _companyRepository.GetCompanyId(ct);

        var spec = new CompanyOrderDetailsQueryModelSpec(companyId, orderId);
        var queryResult = await _orderRepository.GetBySpecAsync(spec, ct);

        return queryResult is null
            ? Result.Failure<OrderDetailsResponse>(OrderErrors.NotFound)
            : queryResult.ToOrderDetailResponse();
    }

    public async Task<Result<UpdatedResponse>> ReturnOrderAsync(Guid userId, Guid orderId)
    {
        var order = await _orderRepository.GetUserOrderAsync(userId, orderId);
        if (order is null)
            return Result.Failure<UpdatedResponse>(OrderErrors.NotFound);

        var returnResult = order.ReturnOrder();
        if (returnResult.IsFailure)
            return Result.Failure<UpdatedResponse>(returnResult.Error);

        await _orderRepository.UpdateAsync(order);
        await _unitOfWork.SaveChangesAsync();

        return order.ToUpdatedResponse();
    }

    public async Task<Result<PagedList<CompanyOrderListResponse>>> GetCompanyOrdersListAsync(OrderCompanyListQuery queryParams, CancellationToken ct = default)
    {
        var companyId = await _companyRepository.GetCompanyId(ct);
        return await _orderRepository.GetCompanyOrdersListAsync(queryParams, companyId, ct);
    }

    /// <summary>
    /// Method that creates rentals per each rent order line found in given order
    /// </summary>
    /// <param name="order"></param>
    /// <param name="rentLines"></param>
    /// <returns></returns>
    /// <exception cref="DomainInvariantException">Is thrown when there is domain invariant inconsistence within processed objects</exception>
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

    /// <summary>
    /// Method encapsulates common logic for shipping order (either from company or private 
    /// seller perspective)
    /// </summary>
    /// <param name="getOrder"></param>
    /// <returns></returns>
    private async Task<Result<UpdatedResponse>> ShipInternal(Func<Task<Order?>> getOrder)
    {
        var order = await getOrder();
        if (order is null)
            return Result.Failure<UpdatedResponse>(OrderErrors.NotFound);

        var shippedResult = order.ShipOrder();

        if (shippedResult.IsFailure)
            return Result.Failure<UpdatedResponse>(shippedResult.Error);

        await _orderRepository.UpdateAsync(order);
        await _unitOfWork.SaveChangesAsync();

        return order.ToUpdatedResponse();
    }


    /// <summary>
    /// Method encapsulates commong logic from private seller and company realted use cases.
    /// </summary>
    /// <param name="getOrder"></param>
    /// <returns></returns>
    private async Task<Result<UpdatedResponse>> DeliverOrderInternal(Func<Task<Order?>> getOrder)
    {
        var order = await getOrder();
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
        await _unitOfWork.SaveChangesAsync();

        return order.ToUpdatedResponse();
    }

    public async Task<Result<IReadOnlyCollection<OrderDashboardListResponse>>> GetDashboardOrdersAsync(CancellationToken ct)
    {
        var spec = new OrderDashboardListResponseSpec();
        return await _orderRepository.GetListBySpecAsync(spec, ct);
    }

    public async Task<Result<PagedList<UserOrderListResponse>>> GetSellerOrdersAsync(UserOrderSellerListQuery queryParams, Guid sellerId, CancellationToken ct = default)
    {
        var queryResult = await _orderRepository.GetUserSellerOrdersListAsync(queryParams, sellerId, ct);
        return queryResult.ToResponse();
    }

    public async Task<Result<IReadOnlyCollection<UserOrderListResponse>>> GetSellerOrdersAsync(Guid sellerId, CancellationToken ct = default)
    {
        var spec = new SellerOrdersListQueryModelSpec(sellerId);
        var queryResult = await _orderRepository.GetListBySpecAsync(spec, ct);

        return queryResult
            .Select(o => o.ToUserOrderListResponse())
            .ToList();
    }
}
