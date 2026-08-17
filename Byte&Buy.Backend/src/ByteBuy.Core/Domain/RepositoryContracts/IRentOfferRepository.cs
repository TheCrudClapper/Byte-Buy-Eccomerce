using ByteBuy.Core.Domain.Offers;
using ByteBuy.Core.Domain.RepositoryContracts.Base;
using ByteBuy.Core.DTO.Public.Offer.RentOffer;
using ByteBuy.Core.Filtration.RentOffer;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface IRentOfferRepository : IRepositoryBase<RentOffer>
{
    Task<PagedList<RentOfferListResponse>> GetRentOffersListAsync(
        RentOfferListQuery queryParams, CancellationToken ct = default);
}
