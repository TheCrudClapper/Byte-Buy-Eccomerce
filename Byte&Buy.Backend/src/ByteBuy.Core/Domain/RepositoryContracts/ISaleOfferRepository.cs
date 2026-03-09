using ByteBuy.Core.Domain.Offers.Entities;
using ByteBuy.Core.Domain.RepositoryContracts.Base;
using ByteBuy.Core.DTO.Public.Offer.SaleOffer;
using ByteBuy.Core.Filtration.SaleOffer;
using ByteBuy.Core.Pagination;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface ISaleOfferRepository : IRepositoryBase<SaleOffer>
{
    Task<SaleOffer?> GetAggregateAsync(Guid id, CancellationToken ct = default);
    Task<PagedList<SaleOfferListResponse>> GetSaleOffersListAsync(SaleOfferListQuery queryParams, CancellationToken ct = default);
}
