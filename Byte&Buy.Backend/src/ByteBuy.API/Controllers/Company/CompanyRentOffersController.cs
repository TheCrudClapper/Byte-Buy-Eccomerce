using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Offer.RentOffer;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Filtration.RentOffer;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers.Company;

[Resource("rentoffers")]
[Route("api/company/rent-offers")]
[ApiController]
public class CompanyRentOffersController : BaseApiController
{
    private readonly IRentOfferService _rentOfferService;
    public CompanyRentOffersController(IRentOfferService rentOfferService)
        => _rentOfferService = rentOfferService;

    [HttpPost]
    public async Task<ActionResult<CreatedResponse>> PostAsync(RentOfferAddRequest request)
        => HandleResult(await _rentOfferService.AddAsync(CurrentUserId, request));

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<UpdatedResponse>> PutAsync(Guid id, RentOfferUpdateRequest request)
        => HandleResult(await _rentOfferService.UpdateAsync(id, request));

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
        => HandleResult(await _rentOfferService.DeleteAsync(id));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RentOfferResponse>> GetByIdAsync(Guid id, CancellationToken ct)
        => HandleResult(await _rentOfferService.GetByIdAsync(id, ct));

    [HttpGet("list")]
    public async Task<ActionResult<PagedList<RentOfferListResponse>>> GetListAsync([FromQuery] RentOfferListQuery queryParams, CancellationToken ct)
        => HandleResult(await _rentOfferService.GetListAsync(queryParams, ct));
}
