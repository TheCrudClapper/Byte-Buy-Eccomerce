using ByteBuy.Core.Domain.RepositoryContracts.UoW;
using ByteBuy.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore.Storage;

namespace ByteBuy.Infrastructure.Repositories.UnitOfWork;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(ApplicationDbContext context)
      => _context = context;

    public Task<int> SaveChangesAsync(CancellationToken ct)
      => _context.SaveChangesAsync(ct);


    public async Task BeginTransactionAsync(CancellationToken ct)
    {
        _transaction = await _context.Database.BeginTransactionAsync(ct);
    }

    public async Task CommitAsync(CancellationToken ct)
    {
        if (_transaction is null)
            throw new InvalidOperationException("No transaction started yet.");

        await _transaction.CommitAsync(ct);
        await _transaction.DisposeAsync();
        _transaction = null;
    }

    public async Task RollbackAsync(CancellationToken ct)
    {
        if (_transaction is null)
            throw new InvalidOperationException("No transaction started yet.");

        await _transaction.RollbackAsync(ct);
        await _transaction.DisposeAsync();
        _transaction = null;
    }
}
