using ByteBuy.Services.DTO.SaleOffer;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.InfraContracts.HttpClients;

public interface ISaleOfferHttpClient
{
    Task<Result<CreatedResponse>> PostSaleOffer(SaleOfferAddRequest request);
    Task<Result<SaleOfferResponse>> GetByIdAsync(Guid id);
    Task<Result> DeleteByIdAsync(Guid id);
    Task<Result<UpdatedResponse>> PutRentOfferAsync(Guid id, SaleOfferUpdateRequest request);
    Task<Result<PagedList<SaleOfferListResponse>>> GetListAsync(SaleOfferListQuery query);
}
