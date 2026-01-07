using ByteBuy.Core.DTO.Offer;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface IOfferBrowserService
{
    //in future implement pagedresult and query obj to filter
    Task<Result<IReadOnlyCollection<OfferListResponse>>> BrowseAsync();
}
