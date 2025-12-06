using ByteBuy.Infrastructure.DbContexts;

namespace ByteBuy.Infrastructure.Repositories;

public abstract class BaseRepository
{
    protected readonly ApplicationDbContext _context;
    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
