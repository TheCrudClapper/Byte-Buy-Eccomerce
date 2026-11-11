using ByteBuy.Core.Domain.RepositoryContracts.UoW;
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
        if (_transaction is not null)
            throw new InvalidOperationException("The transaction already started.");

        _transaction = await _context.Database.BeginTransactionAsync(ct);
    }

    public async Task CommitAsync(CancellationToken ct)
    {
        if (_transaction is null)
            throw new InvalidOperationException("No transaction started yet.");

        try
        {
            await _transaction.CommitAsync(ct);
        }
        finally
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }   
    }

    public async Task RollbackAsync(CancellationToken ct)
    {
        if (_transaction is null) 
            return;

        try
        {
            await _transaction.RollbackAsync(ct);
        }
        finally
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }    
    }
}
