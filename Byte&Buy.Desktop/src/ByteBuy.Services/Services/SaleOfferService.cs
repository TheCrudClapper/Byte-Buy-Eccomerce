using ByteBuy.Services.DTO.SaleOffer;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class SaleOfferService(ISaleOfferHttpClient httpClient) : ISaleOfferService
{
    public async Task<Result<CreatedResponse>> Add(SaleOfferAddRequest request)
        => await httpClient.PostSaleOffer(request);

    public async Task<Result> DeleteById(Guid id)
        => await httpClient.DeleteByIdAsync(id);

    public async Task<Result<SaleOfferResponse>> GetById(Guid id)
        => await httpClient.GetByIdAsync(id);

    public async Task<Result<PagedList<SaleOfferListResponse>>> GetList(SaleOfferListQuery query)
        => await httpClient.GetListAsync(query);

    public async Task<Result<UpdatedResponse>> Update(Guid id, SaleOfferUpdateRequest request)
        => await httpClient.PutRentOfferAsync(id, request);
}
