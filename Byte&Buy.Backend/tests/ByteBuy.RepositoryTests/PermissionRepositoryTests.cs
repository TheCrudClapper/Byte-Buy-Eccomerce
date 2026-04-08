using AutoFixture;
using ByteBuy.Core.Domain.Permissions;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.Domain.Shared.ResultTypes;
using ByteBuy.Infrastructure.DbContexts;
using ByteBuy.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;

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
    #endregion
    #region GetByNameAsync Method Tests
    [Fact]
    public async Task GetPermissionByName_PermissionExists_ReturnsPermission()
    {
        //Arrange
        Permission expected = Permission.Create("test", "test").Value;
        await _context.Permissions.AddAsync(expected);
        await _context.SaveChangesAsync();

        //Act
        var actualPermission = await _permissionRepository.GetByNameAsync(expected.Name, It.IsAny<CancellationToken>());

        //Assert
        actualPermission.Should().Be(expected);
    }

    [Fact]
    public async Task GetPermissionByName_PermissionDoesntExist_ReturnsNull()
    {
        //Act
        var actualPermission = await _permissionRepository.GetByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>());

        //Assert
        actualPermission.Should().Be(null);
    }

    #endregion

}
