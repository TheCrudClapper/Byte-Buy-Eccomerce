using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.Domain.Factories;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.Domain.RepositoryContracts.UoW;
using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.DTO.Internal.Address;
using ByteBuy.Core.DTO.Internal.Seller;
using ByteBuy.Core.DTO.Public.Order;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;
using static ByteBuy.Core.Specification.AddressSpecifications;
using static ByteBuy.Core.Specification.CartSpecifications;
using static ByteBuy.Core.Specification.CompanyInfoSpecifications;
using static ByteBuy.Core.Specification.DeliverySpecifications;
using static ByteBuy.Core.Specification.PortalUserSpecifications;

namespace ByteBuy.Core.Services;

public class OrderCreateService : IOrderCreateService
{
    private readonly ICartRepository _cartRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderRepository _orderRepository;
    private readonly IAddressReadRepository _addressReadRepository;
    private readonly IDeliveryRepository _deliveryRepository;
    private readonly IPortalUserRepository _portalUserRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IPaymentRepository _paymentRepository;

    public OrderCreateService(ICartRepository cartRepository,
        IOrderRepository orderRepository,
        IDeliveryRepository deliveryRepository,
        IPortalUserRepository portalUserRepository,
        ICompanyRepository companyRepository,
        IAddressReadRepository addressReadRepository,
        IPaymentRepository paymentRepository,
        IUnitOfWork unitOfWork)
    {
        _cartRepository = cartRepository;
        _orderRepository = orderRepository;
        _deliveryRepository = deliveryRepository;
        _portalUserRepository = portalUserRepository;
        _companyRepository = companyRepository;
        _addressReadRepository = addressReadRepository;
        _paymentRepository = paymentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<OrderCreatedReponse>> AddAsync(Guid userId, OrderAddRequest request)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var buyerSnapshotResult = await CreateUserSnapshot(userId);
            if (buyerSnapshotResult.IsFailure)
                return Result.Failure<OrderCreatedReponse>(buyerSnapshotResult.Error);

            //Get whole cart aggregate with cart offer + offer for further calc
            var cartSpec = new UserCartAggegateWithOffersAggregateSpec(userId);
            var cart = await _cartRepository.GetBySpecAsync(cartSpec);

            //var cart = await _cartRepository.GetCartOffersForCheckout(userId);
            if (cart is null)
                return Result.Failure<OrderCreatedReponse>(CartErrors.NotFound);

            if (cart.CartOffers.Count == 0)
                return Result.Failure<OrderCreatedReponse>(OrderErrors.NoCartOffersFound);

            var paymentMethod = (PaymentMethod)request.PaymentMethodId;

            var offersBySeller = cart.CartOffers
                .GroupBy(o => o.Offer.Seller.Id);

            // download all needed deliveries
            var deliveriesSpec = new DeliveryOrderQuerySpec(request.SelectedDeliveries.Select(d => d.DeliveryId));
            var deliveries = await _deliveryRepository.GetListBySpecAsync(deliveriesSpec);

            var sellerIds = cart.CartOffers
              .Select(co => (co.Offer.Seller.Id, co.Offer.Seller.Type))
              .Distinct()
              .ToList();

            SellerSnapshotQueryModel? companySnapshot = null;
            if (sellerIds.Any(type => type.Type == SellerType.Company))
            {
                var companySpec = new CompanySellerSnapshotSpec();
                companySnapshot = await _companyRepository.GetBySpecAsync(companySpec);

                if (companySnapshot is null)
                    return Result.Failure<OrderCreatedReponse>(CompanyInfoErrors.NotFound);
            }

            var privateSellerSpec = new PrivateSellersSnapshotSpec(sellerIds
                .Where(i => i.Type != SellerType.Company)
                .Select(i => i.Id));

            var privateSellers = await _portalUserRepository.GetListBySpecAsync(privateSellerSpec);

            // creating lookups for faster and easier acceess
            var deliveryLookup = deliveries
                .ToDictionary(d => d.Id);

            var sellerLookup = privateSellers
                .ToDictionary(s => s.SellerId, s => s);

            if (companySnapshot is not null)
                sellerLookup[companySnapshot.SellerId] = companySnapshot;

            // if in request any courier is selected donwload queryResult's address
            UserShippingAddressQueryModel? shippingAddress = null;
            var courierDeliveryRequest = request.SelectedDeliveries
                .FirstOrDefault(d => d.ShippingAddressId is not null
                             && deliveryLookup[d.DeliveryId].Channel == DeliveryChannel.Courier);

            if (courierDeliveryRequest is not null)
            {
                var addressSpec = new UserShippingAddressQueryModelSpec(userId, courierDeliveryRequest.ShippingAddressId!.Value);
                shippingAddress = await _addressReadRepository.GetBySpecAsync(addressSpec);

                if (shippingAddress is null)
                    return Result.Failure<OrderCreatedReponse>(OrderDeliveryErrors.InvalidShippingAddress);
            }

            // sellers
            var orders = new List<Order>();


            foreach (var sellerGroup in offersBySeller)
            {
                var sellerId = sellerGroup.Key;

                var deliveryRequest = request.SelectedDeliveries
                    .FirstOrDefault(d => d.SellerId == sellerId);

                if (deliveryRequest is null)
                    return Result.Failure<OrderCreatedReponse>(OrderErrors.MisingDeliveryPerSeller);

                if (!deliveryLookup.TryGetValue(deliveryRequest.DeliveryId, out var deliveryDto))
                    return Result.Failure<OrderCreatedReponse>(OrderErrors.InvalidDelivery);

                if (!sellerLookup.TryGetValue(sellerId, out var sellerDto))
                    return Result.Failure<OrderCreatedReponse>(OrderErrors.InvalidSeller);

                var orderId = Guid.NewGuid();

                // orders lines
                var lines = new List<OrderLine>();
                foreach (var cartOffer in sellerGroup)
                {
                    var quantityResult = cartOffer.Offer.DecreaseQuantity(cartOffer.Quantity);
                    if (quantityResult.IsFailure)
                        return Result.Failure<OrderCreatedReponse>(quantityResult.Error);

                    var lineResult = OrderLineFactory.FromCartOffer(orderId, cartOffer);
                    if (lineResult.IsFailure)
                        return Result.Failure<OrderCreatedReponse>(lineResult.Error);

                    lines.Add(lineResult.Value);
                }

                // seller snapshotResult
                var sellerSnapshot = SellerSnapshotFactory.CreateSnapshot(sellerDto);

                // orders delivery
                var deliveryResult = OrderDeliveryFactory.CreateOrderDelivery(
                    orderId,
                    deliveryRequest,
                    deliveryDto,
                    deliveryDto.Channel == DeliveryChannel.Courier ? shippingAddress : null);

                if (deliveryResult.IsFailure)
                    return Result.Failure<OrderCreatedReponse>(deliveryResult.Error);

                var orderResult = Order.CreateNewOrder(
                    userId,
                    deliveryResult.Value,
                    sellerSnapshot,
                    buyerSnapshotResult.Value.Copy(),
                    lines);

                orders.Add(orderResult.Value);

                await _orderRepository.AddAsync(orderResult.Value);
            }

            var orderAmounts = orders.Select(o => (o.Id, o.Total));

            var paymentResult = Payment.CreateNewPayment(paymentMethod, orderAmounts);
            if (paymentResult.IsFailure)
                return Result.Failure<OrderCreatedReponse>(paymentResult.Error);

            var cartResult = await ClearUserCart(userId);
            if (cartResult.IsFailure)
                return Result.Failure<OrderCreatedReponse>(cartResult.Error);

            await _paymentRepository.AddAsync(paymentResult.Value);

            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();

            return new OrderCreatedReponse(paymentResult.Value.Id, paymentResult.Value.Method);
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            return Result.Failure<OrderCreatedReponse>(OrderErrors.FailedToCreateOrder);
        }

    }

    private async Task<Result> ClearUserCart(Guid userId)
    {
        var cartSpec = new UserCartAggregateSpec(userId);
        var cart = await _cartRepository.GetBySpecAsync(cartSpec);
        if (cart is null)
            return Result.Failure(CartErrors.NotFound);

        cart.ClearCart();

        return Result.Success();
    }

    private async Task<Result<BuyerSnapshot>> CreateUserSnapshot(Guid userId)
    {
        var userSpec = new BuyserSnapshotQueryModelSpec(userId);
        var queryResult = await _portalUserRepository.GetBySpecAsync(userSpec);
        if (queryResult is null)
            return Result.Failure<BuyerSnapshot>(CommonUserErrors.NotFound);

        var snapshotResult = BuyerSnapshot.Create(
            queryResult.FirstName,
            queryResult.LastName,
            queryResult.Phone,
            queryResult.Email,
            queryResult.Address);

        return snapshotResult;
    }
}
