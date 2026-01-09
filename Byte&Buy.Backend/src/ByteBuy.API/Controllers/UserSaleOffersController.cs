using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Offer.SaleOffer;
using ByteBuy.Core.DTO.Shared;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Route("api/me/sale-offer")]
[ApiController]
public class UserSaleOffersController : BaseApiController
{
    private readonly IUserSaleOfferService _saleOfferService;
    public UserSaleOffersController(IUserSaleOfferService saleOfferService)
    {
        _saleOfferService = saleOfferService;
    }

    [HttpPost]
    public async Task<ActionResult<CreatedResponse>> PostAsync(UserSaleOfferAddRequest request)
        => HandleResult(await _saleOfferService.AddAsync(CurrentUserId, request));

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<UpdatedResponse>> PutAsync(Guid id, UserSaleOfferUpdateRequest request)
       => HandleResult(await _saleOfferService.UpdateAsync(CurrentUserId, id, request));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserSaleOfferResponse>> GetByIdAsync(Guid id)
        => HandleResult(await _saleOfferService.GetByIdAsync(CurrentUserId, id));

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
        => HandleResult(await _saleOfferService.DeleteAsync(CurrentUserId, id));
}
