using ByteBuy.Core.DTO.Offer;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;

namespace ByteBuy.Core.Services;

public class OfferBrowserService : IOfferBrowserService
{
    public Task<Result<IReadOnlyCollection<OfferListResponse>>> BrowseAsync()
    {
        throw new NotImplementedException();
    }
}
