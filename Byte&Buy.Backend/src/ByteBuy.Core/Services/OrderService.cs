using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.Exceptions;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.Domain.RepositoryContracts.UoW;
using ByteBuy.Core.DTO.Public.Order;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Filtration.Order;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ResultTypes;
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

    public async Task<Result<UpdatedResponse>> CancelOrder(Guid userId, Guid orderId)
    {
        var order = await _orderRepository.GetUserOrder(userId, orderId);
        if (order is null)
            return Result.Failure<UpdatedResponse>(OrderErrors.NotFound);

        var cancelationResult = order.CancelOrder();
        if (cancelationResult.IsFailure)
            return Result.Failure<UpdatedResponse>(cancelationResult.Error);

        //get offers for quantity correction
        var offers = await _offerReposiotry
            .GetOffersByIdsAsync(order.Lines.Select(l => l.OfferId));

        var offerLookup = offers.ToDictionary(o => o.Id);

        foreach(var line in order.Lines)
        {
            if(offerLookup.TryGetValue(line.OfferId, out var offer))
            {
                offer.RestoreQuantity(line.Quantity);
            }        
        }

        await _orderRepository.UpdateAsync(order);
        await _unitOfWork.SaveChangesAsync();

        return order.ToUpdatedResponse();
    }

    public async Task<Result<UpdatedResponse>> ShipOrderAsCompany(Guid orderId)
    {
        var companyId = await _companyRepository.GetCompanyId();
        return await ShipInternal(() => _orderRepository.GetSellerOrder(companyId, orderId));
    }

    public async Task<Result<UpdatedResponse>> DeliverOrderAsCompany(Guid orderId)
    {
        var companyId = await _companyRepository.GetCompanyId();
        return await DeliverOrderInternal(() => _orderRepository.GetSellerOrder(companyId, orderId));
    }

    public async Task<Result<UpdatedResponse>> DeliverOrderAsPrivateSeller(Guid sellerId, Guid orderId)
        => await DeliverOrderInternal(() => _orderRepository.GetSellerOrder(sellerId, orderId));

    public async Task<Result<UpdatedResponse>> ShipOrderAsPrivateSeller(Guid sellerId, Guid orderId)
        => await ShipInternal(() => _orderRepository.GetSellerOrder(sellerId, orderId));


    public async Task<Result<IReadOnlyCollection<UserOrderListResponse>>> GetUserOrdersAsync(Guid userId, CancellationToken ct = default)
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

    public async Task<Result<OrderDetailsResponse>> GetCompanyOrderDetailsAsync(Guid orderId, CancellationToken ct = default)
    {
        var companyId = await _companyRepository.GetCompanyId(ct);

        var spec = new CompanyOrderDetalsResponseSpec(companyId, orderId);
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
        await _unitOfWork.SaveChangesAsync();

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

    public async Task<Result<PagedList<CompanyOrderListResponse>>> GetCompanyOrdersListAsync(OrderCompanyListQuery queryParams,CancellationToken ct = default)
    {
        var companyId = await _companyRepository.GetCompanyId(ct);
        return await _orderRepository.GetOrdersList(queryParams, companyId, ct);
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
        var spec = new DashboardOrdersSpec();
        return await _orderRepository.GetListBySpecAsync(spec, ct);
    }
}
