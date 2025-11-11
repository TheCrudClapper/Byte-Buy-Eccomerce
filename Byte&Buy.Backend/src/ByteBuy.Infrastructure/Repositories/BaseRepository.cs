using ByteBuy.Infrastructure.DbContexts;

namespace ByteBuy.Infrastructure.Repositories;

public class BaseRepository
{
    protected readonly ApplicationDbContext _context;
    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
    }
}
