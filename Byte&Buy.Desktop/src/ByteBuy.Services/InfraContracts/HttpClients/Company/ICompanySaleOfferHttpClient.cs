using ByteBuy.Services.DTO.SaleOffer;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.InfraContracts.HttpClients.Company;

public interface ICompanySaleOfferHttpClient
{
    Task<Result<CreatedResponse>> PostSaleOfferAsync(SaleOfferAddRequest request);
    Task<Result<SaleOfferResponse>> GetByIdAsync(Guid id);
    Task<Result> DeleteByIdAsync(Guid id);
    Task<Result<UpdatedResponse>> PutRentOfferAsync(Guid id, SaleOfferUpdateRequest request);
    Task<Result<PagedList<SaleOfferListResponse>>> GetListAsync(SaleOfferListQuery query);
}
