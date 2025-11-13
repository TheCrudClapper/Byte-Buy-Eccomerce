using Xunit;
using Moq;
using AutoFixture;
using ByteBuy.Core.ServiceContracts;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.Domain.Entities;
using FluentAssertions;
using ByteBuy.Core.Services;
namespace ByteBuy.ServiceTests;

public class PermissionServiceTests
{
    private readonly IPermissionService _permissionService;
    private readonly IPermissionRepository _permissionRepository;
    private readonly Mock<IPermissionRepository> _permissionRepositoryMock;
    private readonly IFixture _fixture;
    public PermissionServiceTests()
    {
        _permissionRepositoryMock = new Mock<IPermissionRepository>();
        _permissionRepository = _permissionRepositoryMock.Object;
        _permissionService = new PermissionService(_permissionRepository);
        _fixture = new Fixture();
    }

    #region HasPermissionAsync Method Tests
    [Fact]
    public async Task HasPermission_PermissionNotFound_ReturnsFalse()
    {
        //Arrange
        Permission? returnedPermission = null;
        _permissionRepositoryMock.Setup(item => item.GetByNameAsync(It.IsAny<string>(), CancellationToken.None))
            .ReturnsAsync(returnedPermission);

        //Act
        bool result = await _permissionService.HasPermissionAsync(Guid.NewGuid(), "TestPermission");

        //Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task HasPermission_UserPermissionGranted_ReturnsTrue()
    {
        //Arrange
        Guid permissionId = Guid.NewGuid();
        Permission permission = _fixture
            .Build<Permission>()
            .With(p => p.Id, permissionId)
            .Without(p => p.RolePermissions)
            .Without(p => p.UserPermissions)
            .Create();

        _permissionRepositoryMock.Setup(item => item.GetByNameAsync(It.IsAny<string>(), CancellationToken.None))
            .ReturnsAsync(permission);

        _permissionRepositoryMock.Setup(item => item.CheckUserPermissionGrant(It.IsAny<Guid>(), permissionId, CancellationToken.None))
            .ReturnsAsync(true);

        //Act
        bool result = await _permissionService.HasPermissionAsync(Guid.NewGuid(), permission.Name);

        //Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task HasPermission_UserPermissionNotGranted_ReturnsFalse()
    {
        //Arrange
        Guid permissionId = Guid.NewGuid();
        Permission permission = _fixture
            .Build<Permission>()
            .With(p => p.Id, permissionId)
            .Without(p => p.RolePermissions)
            .Without(p => p.UserPermissions)
            .Create();

        _permissionRepositoryMock.Setup(item => item.GetByNameAsync(It.IsAny<string>(), CancellationToken.None))
            .ReturnsAsync(permission);

        _permissionRepositoryMock.Setup(item => item.CheckUserPermissionGrant(It.IsAny<Guid>(), permissionId, CancellationToken.None))
            .ReturnsAsync(false);

        _permissionRepositoryMock.Setup(item => item.CheckUserPermissionNotGrant(It.IsAny<Guid>(), permissionId, CancellationToken.None))
            .ReturnsAsync(true);

        //Act
        bool result = await _permissionService.HasPermissionAsync(Guid.NewGuid(), permission.Name);

        //Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task HasPermission_RoleIdNotFound_ReturnFalse()
    {
        //Arrange
        Guid? userRoleId = null;
        Guid permissionId = Guid.NewGuid();
        Permission permission = _fixture
            .Build<Permission>()
            .With(p => p.Id, permissionId)
            .Without(p => p.RolePermissions)
            .Without(p => p.UserPermissions)
            .Create();

        _permissionRepositoryMock.Setup(item => item.GetByNameAsync(It.IsAny<string>(), CancellationToken.None))
            .ReturnsAsync(permission);

        _permissionRepositoryMock.Setup(item => item.CheckUserPermissionGrant(It.IsAny<Guid>(), permissionId, CancellationToken.None))
            .ReturnsAsync(false);

        _permissionRepositoryMock.Setup(item => item.CheckUserPermissionNotGrant(It.IsAny<Guid>(), permissionId, CancellationToken.None))
            .ReturnsAsync(false);

        _permissionRepositoryMock.Setup(item => item.GetUserRoleId(It.IsAny<Guid>(), CancellationToken.None))
            .ReturnsAsync(userRoleId);

        //Act
        bool result = await _permissionService.HasPermissionAsync(Guid.NewGuid(), permission.Name);

        //Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task HasPermission_RoleHasPermission_ReturnTrue()
    {
        //Arrange
        Guid? userRoleId = Guid.NewGuid();
        Guid permissionId = Guid.NewGuid();
        Permission permission = _fixture
            .Build<Permission>()
            .With(p => p.Id, permissionId)
            .Without(p => p.RolePermissions)
            .Without(p => p.UserPermissions)
            .Create();

        _permissionRepositoryMock.Setup(item => item.GetByNameAsync(It.IsAny<string>(), CancellationToken.None))
            .ReturnsAsync(permission);

        _permissionRepositoryMock.Setup(item => item.CheckUserPermissionGrant(It.IsAny<Guid>(), permissionId, CancellationToken.None))
            .ReturnsAsync(false);

        _permissionRepositoryMock.Setup(item => item.CheckUserPermissionNotGrant(It.IsAny<Guid>(), permissionId, CancellationToken.None))
            .ReturnsAsync(false);

        _permissionRepositoryMock.Setup(item => item.GetUserRoleId(It.IsAny<Guid>(), CancellationToken.None))
            .ReturnsAsync(userRoleId);

        _permissionRepositoryMock.Setup(item => item.CheckIfRoleHasPermission(userRoleId.Value, permissionId, CancellationToken.None))
            .ReturnsAsync(true);

        //Act
        bool result = await _permissionService.HasPermissionAsync(Guid.NewGuid(), permission.Name);

        //Assert
        result.Should().BeTrue();
    }
    #endregion
}
