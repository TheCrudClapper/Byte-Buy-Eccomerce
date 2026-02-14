using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.Public.Rental;
using ByteBuy.Core.Filtration.Rental;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.Pagination;
using ByteBuy.Infrastructure.DbContexts;
using ByteBuy.Infrastructure.Extensions;
using ByteBuy.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.Repositories;

public class RentalRepository : EfBaseRepository<Rental>, IRentalRepository
{
    public RentalRepository(ApplicationDbContext context) : base(context) { }

    public async Task<PagedList<CompanyRentalLenderListResponse>> GetCompanyRentalsListAsync(
        Guid companyId, RentalListQuery queryParams, CancellationToken ct = default)
    {
        var query = _context.Rentals
            .AsNoTracking()
            .Where(r => r.Lender.Type == SellerType.Company
                   && r.Lender.SellerId == companyId)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(queryParams.ItemName))
            query = query.Where(r => EF.Functions.ILike(r.ItemName, $"%{queryParams.ItemName}%"));

        if (!string.IsNullOrWhiteSpace(queryParams.BorrowerEmail))
            query = query.Where(r => EF.Functions.ILike(r.Borrower.Email!, $"%{queryParams.BorrowerEmail}%"));

        if (queryParams.RentalDaysFrom.HasValue)
            query = query.Where(r => r.RentalDays >= queryParams.RentalDaysFrom.Value);

        if (queryParams.RentalDaysTo.HasValue)
            query = query.Where(r => r.RentalDays <= queryParams.RentalDaysTo.Value);

        if (queryParams.RentalStartPeriod.HasValue)
            query = query.Where(r => r.RentalStartDate >= DateTime.SpecifyKind(queryParams.RentalStartPeriod.Value, DateTimeKind.Utc));

        if (queryParams.RentalEndPeriod.HasValue)
            query = query.Where(r => r.RentalEndDate <= DateTime.SpecifyKind(queryParams.RentalEndPeriod.Value, DateTimeKind.Utc));

        var projection = query
            .Select(RentalMappings.CompanyRentalLenderResponseProjection);

        return await projection
            .ToPagedListAsync(queryParams.PageNumber, queryParams.PageSize, ct);
    }

    public async Task<PagedList<RentalLenderResponse>> GetUserLenderRentalsAsync(UserRentalLenderQuery queryParams, Guid userId, CancellationToken ct = default)
    {
        var query = _context.Rentals
           .Where(r => r.Lender.SellerId == userId)
           .AsNoTracking()
           .AsQueryable();

        if (!string.IsNullOrWhiteSpace(queryParams.ItemName))
            query = query.Where(r => EF.Functions.ILike(r.ItemName, $"%{queryParams.ItemName}%"));

        if (queryParams.RentalStartDate.HasValue)
            query = query.Where(r => r.RentalStartDate >= DateTime.SpecifyKind(queryParams.RentalStartDate.Value, DateTimeKind.Utc));

        if (queryParams.RentalEndDate.HasValue)
            query = query.Where(r => r.RentalEndDate <= DateTime.SpecifyKind(queryParams.RentalEndDate.Value, DateTimeKind.Utc));

        if (queryParams.RentalStatus.HasValue)
            query = query.Where(r => r.Status == queryParams.RentalStatus);

        var projection = query
            .Select(RentalMappings.UserRentalLenderResponseProjection);

        return await projection
            .ToPagedListAsync(queryParams.PageNumber, queryParams.PageSize, ct);
    }

    public async Task<PagedList<UserRentalBorrowerResponse>> GetUserBorrowerRentalsAsync(UserRentalBorrowerQuery queryParams, Guid userId, CancellationToken ct = default)
    {
        var query = _context.Rentals
            .Where(r => r.BorrowerId == userId)
            .AsNoTracking()
            .AsQueryable();

        var projection = query
            .Select(RentalMappings.UserRentalBorrowerResponseProjection);

        return await projection.
            ToPagedListAsync(queryParams.PageNumber, queryParams.PageSize, ct);
    }

    public async Task<Rental?> GetUserRentalAsync(Guid userId, Guid rentalId, CancellationToken ct = default)
    {
        return await _context.Rentals
            .FirstOrDefaultAsync(r => r.Id == rentalId && r.BorrowerId == userId, ct);
    }
}