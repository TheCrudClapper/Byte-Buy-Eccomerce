using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.Internal.Checkout;
using ByteBuy.Core.DTO.Public.Checkout;
using ByteBuy.Core.DTO.Public.Money;
using ByteBuy.Core.Helpers;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;
using static ByteBuy.Core.Specification.CompanyInfoSpecifications;
using static ByteBuy.Core.Specification.PortalUserSpecifications;

namespace ByteBuy.Core.Services;

public class CheckoutService : ICheckoutService
{
    private readonly IPortalUserRepository _portalUserRepository;
    private readonly ICartRepository _cartRepository;
    private readonly ICompanyRepository _companyRepository;
    public CheckoutService(IPortalUserRepository portalUserRepository,
        ICartRepository cartRepository,
        ICompanyRepository companyRepository)
    {
        _portalUserRepository = portalUserRepository;
        _cartRepository = cartRepository;
        _companyRepository = companyRepository;
    }

    public async Task<Result<CheckoutResponse>> GetCheckout(Guid userId, CancellationToken ct)
    {
        // Gathering data
        var spec = new PortalUserToUserBasicInfoResponseSpec(userId);
        var userData = await _portalUserRepository.GetBySpecAsync(spec, ct);

        if (userData is null)
            return Result.Failure<CheckoutResponse>(CommonUserErrors.NotFound);

        var cartOffers = await _cartRepository.GetCartOffersForCheckout(userId, ct);

        var sellerIds = cartOffers
            .Select(co => (co.SellerId, co.SellerType))
            .Distinct()
            .ToList();

        SellerCheckoutResponse? companyData = null;
        if (sellerIds.Any(s => s.SellerType == SellerType.Company))
        {
            var companySellerSpec = new CompanyInfoToSellerCheckoutResponseSpec();
            companyData = await _companyRepository.GetBySpecAsync(companySellerSpec, ct);

            if (companyData is null)
                return Result.Failure<CheckoutResponse>(CompanyInfoErrors.NotFound);
        }

        var privateSellersSpec = new PortalUserToSellerCheckoutSpec(sellerIds
            .Where(s => s.SellerType != SellerType.Company)
            .Select(i => i.SellerId));

        var privateSellers = await _portalUserRepository.GetListBySpecAsync(privateSellersSpec, ct);

        // Transformation
        var sellerLookup = privateSellers
            .ToDictionary(s => s.SellerId, s => (s.SellerDisplayName, s.SellerEmail));

        if (companyData is not null)
            sellerLookup[companyData.SellerId] = (companyData.SellerDisplayName, companyData.SellerEmail);

        var sellerGroups = cartOffers
            .GroupBy(co => co.SellerId)
            .Select(g =>
            {
                if (!sellerLookup.TryGetValue(g.Key, out var seller))
                    throw new InvalidOperationException($"Seller of ID: {g.Key} not found in lookup");

                var items = g
                    .Select(ci => ci.MapToCheckoutItem())
                    .ToList();

                return new SellerGroup(
                    SellerId: g.Key,
                    SellerDisplayName: seller.SellerDisplayName,
                    SellerEmail: seller.SellerEmail,
                    ItemsWorth: MoneyHelper.Sum(items.Select(i => i.Subtotal)),
                    CheckoutItems: items);

            }).ToList();

        var itemsCost = MoneyHelper.Sum(sellerGroups.Select(sq => sq.ItemsWorth));
        var tax = MoneyHelper.From(Decimal.Multiply(itemsCost.Amount, 0.23m), itemsCost);

        var dto = new CheckoutResponse(
            userData.FirstName,
            userData.LastName,
            userData.PhoneNumber!,
            sellerGroups,
            itemsCost,
            tax,
            itemsCost);

        return dto;
    }

}

