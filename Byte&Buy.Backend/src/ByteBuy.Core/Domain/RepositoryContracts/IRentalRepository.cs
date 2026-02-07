using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts.Base;
using System.Linq.Expressions;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface IRentalRepository : IRepositoryBase<Rental>
{
    Task<Rental?> GetUserRental(Guid userId, Guid rentalId, CancellationToken ct = default);
}
