using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Infrastructure.DbContexts;
using ByteBuy.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.Repositories;

public class UserRepository : EfBaseRepository<ApplicationUser>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context) { }

    public Task<bool> ExistByEmailAsync(string email, CancellationToken ct)
    {
        return _context.Users
            .AsNoTracking()
            .AnyAsync(e => e.Email == email, ct);
    }
}
