using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.Public.Checkout;
using ByteBuy.Core.DTO.Public.Money;
using ByteBuy.Core.DTO.Public.Offer.Common;
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

        var companySellerSpec = new CompanyInfoToSellerCheckoutResponseSpec();
        var companyData = await _companyRepository.GetBySpecAsync(companySellerSpec, ct);
        if (companyData is null)
            return Result.Failure<CheckoutResponse>(CompanyInfoErrors.NotFound);

        var privateSellersIds = cartOffers
            .Where(co => co.SellerType == SellerType.PrivatePerson)
            .Select(co => co.SellerId)
            .ToList();

        var privateSellersSpec = new PortalUserToSellerCheckoutSpec(privateSellersIds);
        var privateSellers = await _portalUserRepository.GetListBySpecAsync(privateSellersSpec, ct);

        // Transformation
        var sellerLookup = new Dictionary<Guid, (string Name, string Email)>();

        foreach (var seller in privateSellers)
            sellerLookup[seller.SellerId] = (seller.SellerDisplayName, seller.SellerEmail);

        sellerLookup[companyData.SellerId] = (companyData.SellerDisplayName, companyData.SellerEmail);

        var sellerGroups = cartOffers
            .GroupBy(co => co.SellerId)
            .Select(g =>
            {
                var seller = sellerLookup[g.Key];

                var items = g
                    .Select(ci => ci.MapToCheckoutItem())
                    .ToList();

                return new SellerGroup(
                    g.Key,
                    seller.Name,
                    seller.Email,
                    new MoneyDto(items.Sum(i => i.Subtotal.Amount), "PLN"),
                    items);

            }).ToList();

        var itemsCost = sellerGroups.Sum(sg => sg.ItemsWorth.Amount);
        var tax = Decimal.Multiply(itemsCost, 0.23m);

        var dto = new CheckoutResponse(
            userData.FirstName,
            userData.LastName,
            userData.PhoneNumber!,
            sellerGroups,
            new MoneyDto(itemsCost, "PLN"),
            new MoneyDto(tax, "PLN"),
            new MoneyDto(itemsCost, "PLN"));

        return dto;
    }
}

