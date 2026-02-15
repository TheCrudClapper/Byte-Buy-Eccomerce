using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.Public.Offer.Common;
using ByteBuy.Core.DTO.Public.Offer.RentOffer;
using ByteBuy.Core.DTO.Public.Offer.SaleOffer;
using ByteBuy.Core.Filtration.Offer;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;
using static ByteBuy.Core.Specification.OfferSpecifications;
using static ByteBuy.Core.Specification.RentOfferSpecifications;
using static ByteBuy.Core.Specification.SaleOfferSpecifications;

namespace ByteBuy.Core.Services;

public class OfferReadService : IOfferReadService
{
    private readonly IRentOfferRepository _rentOfferRepository;
    private readonly ISaleOfferRepository _saleOfferRepository;
    private readonly IOfferRepository _offerRepository;
    public OfferReadService(IRentOfferRepository rentOfferRepository,
        ISaleOfferRepository saleOfferRepository,
        IOfferRepository offerRepository)
    {
        _rentOfferRepository = rentOfferRepository;
        _saleOfferRepository = saleOfferRepository;
        _offerRepository = offerRepository;
    }

    public async Task<Result<PagedList<OfferBrowserItemResponse>>> BrowseAsync(OfferBrowserQuery queryParams, CancellationToken ct)
    {
        var query = await _offerRepository.BrowserAsync(queryParams, ct);
        return query.ToResponse();
    }

    public async Task<Result<RentOfferDetailsResponse>> GetRentOfferDetails(Guid id, CancellationToken ct = default)
    {
        var spec = new RentOfferDetailsResponseSpec(id);
        var detailsDto = await _rentOfferRepository.GetBySpecAsync(spec, ct);
        return detailsDto is null
            ? Result.Failure<RentOfferDetailsResponse>(OfferErrors.NotFound)
            : detailsDto;
    }

    public async Task<Result<SaleOfferDetailsResponse>> GetSaleOfferDetails(Guid id, CancellationToken ct = default)
    {
        var spec = new SaleOfferDetailsResponseSpec(id);
        var detailsDto = await _saleOfferRepository.GetBySpecAsync(spec, ct);
        return detailsDto is null
            ? Result.Failure<SaleOfferDetailsResponse>(OfferErrors.NotFound)
            : detailsDto;
    }

    public async Task<Result<IReadOnlyCollection<UserPanelOfferResponse>>> GetUserPanelOffers(Guid userId, CancellationToken ct = default)
    {
        var spec = new UserOffersPanelSpec(userId);
        var userOffers = await _offerRepository.GetListBySpecAsync(spec, ct);
        return userOffers
            .Select(uo => uo.ToUserOfferPanelResponse())
            .ToList();
    }

    public async Task<Result<PagedList<UserPanelOfferResponse>>> GetUserPanelOffers(UserOffersQuery queryParams, Guid userId, CancellationToken ct = default)
    {
        var query = await _offerRepository.GetUserOffersAsync(queryParams, userId, ct);
        return query.ToResponse();
    }
}
