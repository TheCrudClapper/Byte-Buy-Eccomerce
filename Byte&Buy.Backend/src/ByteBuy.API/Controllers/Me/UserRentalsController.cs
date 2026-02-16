using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Rental;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Filtration.Rental;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers.Me;

[Resource("user-rentals")]
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
    [HasPermission("user-rentals:read:borrower")]
    public async Task<ActionResult<PagedList<UserRentalBorrowerResponse>>> GetBorrowerRentalsAsync([FromQuery] UserRentalBorrowerQuery queryParams, CancellationToken ct = default)
        => HandleResult(await _rentalService.GetUserBorrowerRentalsAsync(queryParams, CurrentUserId, ct));

    [HttpGet("lender")]
    [HasPermission("user-rentals:read:lender")]
    public async Task<ActionResult<IReadOnlyCollection<RentalLenderResponse>>> GetLenderRentalsAsync([FromQuery] UserRentalLenderQuery queryParams, CancellationToken ct = default)
        => HandleResult(await _rentalService.GetUserLenderRentalsAsync(queryParams, CurrentUserId, ct));

    [HttpPut("{rentalId:guid}/return")]
    [HasPermission("user-rentals:update:return")]
    public async Task<ActionResult<UpdatedResponse>> ReturnRentedItemAsync(Guid rentalId)
        => HandleResult(await _rentalService.ReturnItemToLenderAsync(CurrentUserId, rentalId));

}
