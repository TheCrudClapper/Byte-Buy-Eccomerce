using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.Internal.Checkout;
using ByteBuy.Core.DTO.Public.Checkout;
using ByteBuy.Core.DTO.Public.Delivery;
using ByteBuy.Core.Helpers;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;
using static ByteBuy.Core.Specification.CompanyInfoSpecifications;
using static ByteBuy.Core.Specification.DeliverySpecifications;
using static ByteBuy.Core.Specification.PortalUserSpecifications;

namespace ByteBuy.Core.Services;

public class CheckoutService : ICheckoutService
{
    private readonly IPortalUserRepository _portalUserRepository;
    private readonly ICartRepository _cartRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IDeliveryRepository _deliveryRepository;
    public CheckoutService(IPortalUserRepository portalUserRepository,
        ICartRepository cartRepository,
        ICompanyRepository companyRepository,
        IDeliveryRepository deliveryRepository)
    {
        _portalUserRepository = portalUserRepository;
        _cartRepository = cartRepository;
        _companyRepository = companyRepository;
        _deliveryRepository = deliveryRepository;
    }

    public async Task<Result<CheckoutResponse>> GetCheckoutAsync(Guid userId, CancellationToken ct)
    {
        // Gathering data
        var spec = new UserBasicInfoResponseSpec(userId);
        var userData = await _portalUserRepository.GetBySpecAsync(spec, ct);

        if (userData is null)
            return Result.Failure<CheckoutResponse>(CheckoutErrors.UserDataNotFound);

        var checkoutItemsQuery = await _cartRepository.GetCartOffersAsCheckoutItemQuery(userId, ct);

        var sellerIds = checkoutItemsQuery
            .Select(co => (co.SellerId, co.SellerType))
            .Distinct()
            .ToList();

        SellerCheckoutQueryModel? companyData = null;
        if (sellerIds.Any(s => s.SellerType == SellerType.Company))
        {
            var companySellerSpec = new CompanyInfoToSellerCheckoutResponseSpec();
            companyData = await _companyRepository.GetBySpecAsync(companySellerSpec, ct);

            if (companyData is null)
                return Result.Failure<CheckoutResponse>(CompanyInfoErrors.NotFound);
        }

        var privateSellersSpec = new UserSellerCheckoutSpec(sellerIds
            .Where(s => s.SellerType != SellerType.Company)
            .Select(i => i.SellerId));

        var privateSellers = await _portalUserRepository.GetListBySpecAsync(privateSellersSpec, ct);

        // Transformation
        var sellerLookup = privateSellers
            .ToDictionary(s => s.SellerId, s => (s.SellerDisplayName, s.SellerEmail));

        if (companyData is not null)
            sellerLookup[companyData.SellerId] = (companyData.SellerDisplayName, companyData.SellerEmail);

        // Group deliveries into dictionary [key: sellerid, value [..deliveryids..]
        var sellerDeliveryIds = checkoutItemsQuery
            .GroupBy(co => co.SellerId)
            .ToDictionary(
                g => g.Key,
                g => ResolveCommonDeliveries(g)
            );

        // Select only distincs values from deliveries
        var allDeliveries = sellerDeliveryIds
            .SelectMany(x => x.Value)
            .Distinct()
            .ToList();

        var deliverySpec = new DeliveryOptionByIdsSpec(allDeliveries);
        var deliveryOptions = await _deliveryRepository.GetListBySpecAsync(deliverySpec, ct);

        var deliveryLookup = deliveryOptions.ToDictionary(d => d.Id);

        var sellerGroups = checkoutItemsQuery
            .GroupBy(co => co.SellerId)
            .Select(g =>
            {
                if (!sellerLookup.TryGetValue(g.Key, out var seller))
                    throw new InvalidOperationException($"Seller of ID: {g.Key} not found in lookup");

                var deliveryOptions = sellerDeliveryIds[g.Key]
                .Select(id => deliveryLookup[id]).ToList();

                var groupedDeliveries = MapDeliveries(deliveryOptions);

                var items = g
                    .Select(ci => ci.MapToCheckoutItem())
                    .ToList();

                return new SellerGroup(
                    SellerId: g.Key,
                    SellerDisplayName: seller.SellerDisplayName,
                    SellerEmail: seller.SellerEmail,
                    ItemsWorth: MoneyHelper.Sum(items.Select(i => i.Subtotal)),
                    CheckoutItems: items,
                    groupedDeliveries);

            }).ToList();

        var itemsCost = MoneyHelper.Sum(sellerGroups.Select(sq => sq.ItemsWorth));
        var tax = MoneyHelper.From(Decimal.Multiply(itemsCost.Amount, 0.23m), itemsCost);

        var dto = new CheckoutResponse(
            userData.FirstName,
            userData.LastName,
            userData.PhoneNumber!,
            sellerGroups,
            EnumToSelectListMapper.EnumToSelectLists<PaymentMethod>(),
            itemsCost,
            tax,
            itemsCost,
            sellerGroups
                .SelectMany(s => s.CheckoutItems)
                .All(i => i.CanFinalize));

        return dto;
    }


    public static IReadOnlyCollection<Guid> ResolveCommonDeliveries(IEnumerable<CheckoutItemQueryModel> sellerOffers)
    {
        var offers = sellerOffers.ToList();

        if (offers.Count == 0)
            return [];

        var common = offers
            .First()
            .AvaliableDeliveriesIds
            .ToHashSet();

        // skip first because common is our first record
        foreach (var offer in offers.Skip(1))
        {
            // left in common just those ids that are also in offer.avaliabledelieriesids
            common.IntersectWith(offer.AvaliableDeliveriesIds);

            if (common.Count == 0)
                break;
        }

        return common.ToList();

    }

    public static DeliveryOptionsResponse MapDeliveries(IEnumerable<DeliveryOptionResponse> avaliableDeliveries)
    {
        var deliveries = avaliableDeliveries.ToList();

        if (deliveries.Count == 0)
            return new DeliveryOptionsResponse
            {
                PickupPointDeliveries = [],
                CourierDeliveries = [],
                ParcelLockerDeliveries = []
            };

        var parcel = deliveries
            .Where(d => d.DeliveryChannel == DeliveryChannel.ParcelLocker.ToString())
            .ToList();

        var pickup = deliveries
            .Where(d => d.DeliveryChannel == DeliveryChannel.PickupPoint.ToString())
            .ToList();

        var courier = deliveries
           .Where(d => d.DeliveryChannel == DeliveryChannel.Courier.ToString())
           .ToList();

        return new DeliveryOptionsResponse
        {
            CourierDeliveries = courier,
            ParcelLockerDeliveries = parcel,
            PickupPointDeliveries = pickup
        };
    }
}

