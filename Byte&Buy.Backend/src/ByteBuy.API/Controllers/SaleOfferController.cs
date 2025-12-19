using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.SaleOffer;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SaleOfferController : CrudControllerBase<Guid, SaleOfferAddRequest, SaleOfferUpdateRequest, SaleOfferResponse>
{
    private readonly ISaleOfferService _saleOfferService;
    public SaleOfferController(ISaleOfferService saleOfferService) : base(saleOfferService)
        =>  _saleOfferService = saleOfferService;
    
}
