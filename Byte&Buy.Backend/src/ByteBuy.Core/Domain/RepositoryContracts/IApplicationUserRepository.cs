namespace ByteBuy.Core.Domain.RepositoryContracts;

/// <summary>
/// This repository is used to work with user manager but
/// methods provided are faster and better suited for this application
/// </summary>
public interface IApplicationUserRepository
{
    Task<bool> ExistByEmailAsync(string email, CancellationToken ct = default);
}
