using ByteBuy.Core.Domain.RepositoryContracts.Base;
using ByteBuy.Core.Domain.Users.Base;

namespace ByteBuy.Core.Domain.RepositoryContracts;

/// <summary>
/// This repository is used to work with user manager but
/// methods provided here are faster and better suited for this application
/// </summary>
public interface IUserRepository : IRepositoryBase<ApplicationUser>
{
    Task<bool> ExistByEmailAsync(string email, CancellationToken ct = default);
}
