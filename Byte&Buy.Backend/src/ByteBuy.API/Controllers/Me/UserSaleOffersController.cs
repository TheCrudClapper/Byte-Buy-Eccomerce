using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Offer.SaleOffer;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers.Me;

[Resource("user-sale-offer")]
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
    [HasPermission("{resource}:create")]
    public async Task<ActionResult<CreatedResponse>> PostAsync([FromForm] UserSaleOfferAddRequest request)
        => HandleResult(await _saleOfferService.AddAsync(CurrentUserId, request));

    [HttpPut("{id:guid}")]
    [HasPermission("{resource}:update")]
    public async Task<ActionResult<UpdatedResponse>> PutAsync(Guid id, [FromForm] UserSaleOfferUpdateRequest request)
       => HandleResult(await _saleOfferService.UpdateAsync(CurrentUserId, id, request));

    [HttpGet("{id:guid}")]
    [HasPermission("{resource}:read")]
    public async Task<ActionResult<UserSaleOfferResponse>> GetByIdAsync(Guid id)
        => HandleResult(await _saleOfferService.GetByIdAsync(CurrentUserId, id));

    [HttpDelete("{id:guid}")]
    [HasPermission("{resource}:delete")]
    public async Task<IActionResult> DeleteAsync(Guid id)
        => HandleResult(await _saleOfferService.DeleteAsync(CurrentUserId, id));
}
