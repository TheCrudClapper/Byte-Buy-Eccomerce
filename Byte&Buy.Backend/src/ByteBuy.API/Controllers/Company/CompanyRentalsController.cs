using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Rental;
using ByteBuy.Core.Filtration.Rental;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers.Company;

[Resource("company-rentals")]
[Route("api/company/rentals")]
[ApiController]
public class CompanyRentalsController : BaseApiController
{
    private readonly IRentalService _rentalService;
    public CompanyRentalsController(IRentalService rentalService)
      => _rentalService = rentalService;

    [HttpGet]
    [HasPermission("company-rentals:read:many")]
    public async Task<ActionResult<PagedList<CompanyRentalLenderListResponse>>> GetCompanyRentalsListAsync(
         [FromQuery] RentalListQuery queryParams, CancellationToken ct = default)
       => HandleResult(await _rentalService.GetCompanyLenderRentalsListAsync(queryParams, ct));


    [HttpGet("{rentalId:guid}")]
    [HasPermission("company-rentals:read:one")]
    public async Task<ActionResult<RentalLenderResponse>> GetCompanyRentalAsync(Guid rentalId, CancellationToken ct = default)
        => HandleResult(await _rentalService.GetCompanyRentalAsync(rentalId, ct));
}
