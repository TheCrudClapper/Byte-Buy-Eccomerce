using ByteBuy.Services.DTO.RentOffer;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.InfraContracts.HttpClients.Company;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class RentOfferService(ICompanyRentOfferHttpClient httpClient) : IRentOfferService
{
    public async Task<Result<CreatedResponse>> AddAsync(RentOfferAddRequest request)
        => await httpClient.PostRentOfferAsync(request);

    public async Task<Result> DeleteByIdAsync(Guid id)
        => await httpClient.DeleteByIdAsync(id);

    public async Task<Result<RentOfferResponse>> GetByIdAsync(Guid id)
        => await httpClient.GetByIdAsync(id);

    public async Task<Result<PagedList<RentOfferListResponse>>> GetListAsync(RentOfferListQuery query)
        => await httpClient.GetListAsync(query);

    public Task<Result<UpdatedResponse>> UpdateAsync(Guid id, RentOfferUpdateRequest request)
        => httpClient.PutRentOfferAsync(id, request);
}
