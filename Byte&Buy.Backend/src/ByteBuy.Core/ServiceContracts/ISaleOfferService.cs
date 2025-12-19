using ByteBuy.Core.DTO.SaleOffer;
using ByteBuy.Core.ServiceContracts.Base;

namespace ByteBuy.Core.ServiceContracts;

public interface ISaleOfferService 
    : IBaseCrudService<Guid, SaleOfferAddRequest, SaleOfferUpdateRequest, SaleOfferResponse>
{

}
