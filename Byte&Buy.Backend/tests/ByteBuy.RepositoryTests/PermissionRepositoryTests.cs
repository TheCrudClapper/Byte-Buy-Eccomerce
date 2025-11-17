using AutoFixture;
using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Infrastructure.DbContexts;
using ByteBuy.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.RepositoryTests;

public class PermissionRepositoryTests
{
    private readonly ApplicationDbContext _context;
    private readonly IPermissionRepository _permissionRepository;
    private readonly Fixture _fixture;
    public PermissionRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new ApplicationDbContext(options);
        _permissionRepository = new PermissionRepository(_context);
        _fixture = new Fixture();
    }


    #region  HasUserOrRolePermissionAsync Method Tests
    [Fact]
    public async Task HasUserOrRolePermissionAsync_ExplicitGrant_ReturnsTrue()
    {
        ////Arrange
        //var userResult = PortalUser.Create("Jan", "Kowalski", "test123@gmail.com");
        //var user = userResult.Value;

        //await _context.Users.AddAsync(user);
        //await _context.SaveChangesAsync();

    }
    #endregion
    #region GetByNameAsync Method Tests
    [Fact]
    public async Task GetPermissionByName_PermissionExists_ReturnsPermission()
    {
        //Arrange
        Permission expected = _fixture.Build<Permission>()
            .With(p => p.IsActive, true)
            .Without(p => p.UserPermissions)
            .Without(p => p.RolePermissions)
            .Create();

        await _context.Permissions.AddAsync(expected);
        await _context.SaveChangesAsync();

        //Act
        var actualPermission = await _permissionRepository.GetByNameAsync(expected.Name, CancellationToken.None);

        //Assert
        actualPermission.Should().Be(expected);
    }

    [Fact]
    public async Task GetPermissionByName_PermissionDoesntExist_ReturnsNull()
    {
        //Arrange
        Permission expected = _fixture.Build<Permission>()
            .With(p => p.IsActive, true)
            .Without(p => p.UserPermissions)
            .Without(p => p.RolePermissions)
            .Create();

        //Act
        var actualPermission = await _permissionRepository.GetByNameAsync(expected.Name, CancellationToken.None);

        //Assert
        actualPermission.Should().Be(null);
    }

    #endregion
}
