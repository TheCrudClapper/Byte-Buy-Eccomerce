using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Rental;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Authorize]
[Route("api/rentals")]
[ApiController]
public class CompanyRentalsController : BaseApiController
{
    private readonly IRentalService _rentalService;
    public CompanyRentalsController(IRentalService rentalService)
      => _rentalService = rentalService;


    [HttpGet("company")]
    public async Task<ActionResult<IReadOnlyCollection<CompanyRentalLenderResponse>>> GetCompanyRentals(CancellationToken ct = default)
       => HandleResult(await _rentalService.GetCompanyRentalsListAsync(CurrentUserId, ct));

    [HttpGet("{rentalId:guid}/company")]
    public async Task<ActionResult<RentalLenderResponse>> GetCompanyRental(Guid rentalId, CancellationToken ct = default)
        => HandleResult(await _rentalService.GetCompanyRentalAsync(rentalId, ct));
}
