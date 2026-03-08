using ByteBuy.Services.DTO.SaleOffer;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.InfraContracts.HttpClients.Company;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class SaleOfferService(ICompanySaleOfferHttpClient httpClient) : ISaleOfferService
{
    public async Task<Result<CreatedResponse>> AddAsync(SaleOfferAddRequest request)
        => await httpClient.PostSaleOfferAsync(request);

    public async Task<Result> DeleteByIdAsync(Guid id)
        => await httpClient.DeleteByIdAsync(id);

    public async Task<Result<SaleOfferResponse>> GetByIdAsync(Guid id)
        => await httpClient.GetByIdAsync(id);

    public async Task<Result<PagedList<SaleOfferListResponse>>> GetListAsync(SaleOfferListQuery query)
        => await httpClient.GetListAsync(query);

    public async Task<Result<UpdatedResponse>> UpdateAsync(Guid id, SaleOfferUpdateRequest request)
        => await httpClient.PutRentOfferAsync(id, request);
}
