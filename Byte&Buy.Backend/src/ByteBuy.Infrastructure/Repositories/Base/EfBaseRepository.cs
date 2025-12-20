using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ByteBuy.Infrastructure.Repositories.Base;

public class EfBaseRepository<T> : Core.Domain.RepositoryContracts.Base.IRepositoryBase<T>
    where T : class, IEntity
{
    protected readonly ApplicationDbContext _context;
    private readonly ISpecificationEvaluator _specEval
        = SpecificationEvaluator.Default;

    public EfBaseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public virtual Task AddAsync(T entity)
    {
        _context.Set<T>().Add(entity);
        return Task.CompletedTask;
    }

    public virtual Task UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        return Task.CompletedTask;
    }

    public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await _context.Set<T>().FirstOrDefaultAsync(item => item.Id == id, ct);

    public virtual async Task<T?> GetBySpecAsync(ISpecification<T> spec, CancellationToken ct = default)
        => await ApplySpecification(spec).FirstOrDefaultAsync(ct);

    public virtual async Task<TResult?> GetBySpecAsync<TResult>(ISpecification<T, TResult> spec, CancellationToken ct = default)
        => await ApplySpecification(spec).FirstOrDefaultAsync(ct);

    public virtual async Task<List<T>> GetListBySpecAsync(ISpecification<T> spec, CancellationToken ct = default)
        => await ApplySpecification(spec).ToListAsync(ct);

    public virtual async Task<List<TResult>> GetListBySpecAsync<TResult>(ISpecification<T, TResult> spec, CancellationToken ct = default)
        => await ApplySpecification(spec).ToListAsync(ct);

    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        => _specEval.GetQuery(_context.Set<T>(), spec);

    private IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> spec)
        => _specEval.GetQuery(_context.Set<T>(), spec);

    public virtual Task<int> CommitAsync(CancellationToken ct = default)
        => _context.SaveChangesAsync(ct);

    public Task<List<T>> GetAllByConditionAsync(Expression<Func<T, bool>> expression, CancellationToken ct = default)
        => _context.Set<T>().Where(expression).ToListAsync(ct);

    public async Task<T?> GetByConditionAsync(Expression<Func<T, bool>> expression, CancellationToken ct = default)
        => await _context.Set<T>().FirstOrDefaultAsync(expression, ct);

    public async Task<bool> ExistsByCondition(Expression<Func<T, bool>> expression, CancellationToken ct = default)
        => await _context.Set<T>().AnyAsync(expression, ct);
}
