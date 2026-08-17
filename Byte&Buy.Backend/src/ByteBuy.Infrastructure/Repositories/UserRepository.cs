using ByteBuy.Core.Domain.Users.Base;

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
