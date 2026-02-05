using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Rental;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Route("api/rentals")]
[ApiController]
public class RentalsController : BaseApiController
{
    private readonly IRentalService _rentalService;
    public RentalsController(IRentalService rentalService)
    {
        _rentalService = rentalService;
    }

    [HttpGet("borrower")]
    public async Task<ActionResult<IReadOnlyCollection<UserRentalBorrowerResponse>>> GetUserRentals(CancellationToken ct = default)
        => HandleResult(await _rentalService.GetUserRentalsAsync(CurrentUserId, ct));

    [HttpGet("lender")]
    public async Task<ActionResult<IReadOnlyCollection<UserRentalLenderResponse>>> GetLenderRentals(CancellationToken ct = default)
        => HandleResult(await _rentalService.GetSellerRentalsAsync(CurrentUserId, ct));

    [HttpPut("{rentalId:guid}/return")]
    public async Task<ActionResult<UpdatedResponse>> ReturnRentedItem(Guid rentalId)
        => HandleResult(await _rentalService.ReturnItemToLenderAsync(CurrentUserId, rentalId));

    [HttpGet("company")]
    public async Task<ActionResult<IReadOnlyCollection<CompanyRentalLenderResponse>>> GetCompanyRentals(CancellationToken ct = default)
       => HandleResult(await _rentalService.GetCompanyRentalsListAsync(CurrentUserId, ct));
}
