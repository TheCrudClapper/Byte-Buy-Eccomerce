using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.Offer;
using ByteBuy.Core.DTO.Offer.RentOffer;
using ByteBuy.Core.DTO.Offer.SaleOffer;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;
using static ByteBuy.Core.Specification.RentOfferSpecifications;
using static ByteBuy.Core.Specification.SaleOfferSpecifications;

namespace ByteBuy.Core.Services;

public class OfferReadService : IOfferReadService
{
    private readonly IRentOfferRepository _rentOfferRepository;
    private readonly ISaleOfferRepository _saleOfferRepository;
    public OfferReadService(IRentOfferRepository rentOfferRepository,
        ISaleOfferRepository saleOfferRepository)
    {
        _rentOfferRepository = rentOfferRepository;
        _saleOfferRepository = saleOfferRepository;
    }
    public Task<Result<IReadOnlyCollection<OfferListResponse>>> BrowseAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Result<RentOfferDetailsResponse>> GetRentOfferDetails(Guid id, CancellationToken ct = default)
    {
        var spec = new RentOfferDetailsSpec(id);
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
}
