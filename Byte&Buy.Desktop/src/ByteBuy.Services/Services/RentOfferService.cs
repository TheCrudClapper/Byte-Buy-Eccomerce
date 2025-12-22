using ByteBuy.Services.DTO.RentOffer;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class RentOfferService(IRentOfferHttpClient httpClient) : IRentOfferService
{
    public async Task<Result<CreatedResponse>> Add(RentOfferAddRequest request)
        =>  await httpClient.PostRentOfferAsync(request);

    public async Task<Result> DeleteById(Guid id)
        => await httpClient.DeleteByIdAsync(id);

    public async Task<Result<RentOfferResponse>> GetById(Guid id)
        => await httpClient.GetByIdAsync(id);

    public async Task<Result<IReadOnlyCollection<RentOfferListResponse>>> GetList()
        => await httpClient.GetListAsync();

    public Task<Result<UpdatedResponse>> Update(Guid id, RentOfferUpdateRequest request)
        => httpClient.PutRentOfferAsync(id, request);
}
