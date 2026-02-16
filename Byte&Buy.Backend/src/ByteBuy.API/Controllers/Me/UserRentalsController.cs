using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Rental;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Filtration.Rental;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers.Me;

[Route("api/me/rentals")]
[ApiController]
public class UserRentalsController : BaseApiController
{
    private readonly IRentalService _rentalService;
    public UserRentalsController(IRentalService rentalService)
    {
        _rentalService = rentalService;
    }

    [HttpGet("borrower")]
    public async Task<ActionResult<PagedList<UserRentalBorrowerResponse>>> GetBorrowerRentalsAsync([FromQuery] UserRentalBorrowerQuery queryParams, CancellationToken ct = default)
        => HandleResult(await _rentalService.GetUserBorrowerRentalsAsync(queryParams, CurrentUserId, ct));

    [HttpGet("lender")]
    public async Task<ActionResult<IReadOnlyCollection<RentalLenderResponse>>> GetLenderRentalsAsync([FromQuery] UserRentalLenderQuery queryParams, CancellationToken ct = default)
        => HandleResult(await _rentalService.GetUserLenderRentalsAsync(queryParams, CurrentUserId, ct));

    [HttpPut("{rentalId:guid}/return")]
    public async Task<ActionResult<UpdatedResponse>> ReturnRentedItemAsync(Guid rentalId)
        => HandleResult(await _rentalService.ReturnItemToLenderAsync(CurrentUserId, rentalId));

}
