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
    public async Task HasPermissionAsync_UserPermissionDenyOrAbsentInRole_ReturnsFalse()
    {
        //Arrange
        Permission? returnedPermission = _fixture.Build<Permission>()
            .Without(p => p.RolePermissions)
            .Without(p => p.UserPermissions)
            .Create();

        _permissionRepositoryMock.Setup(item => item.GetByNameAsync(It.IsAny<string>(), CancellationToken.None))
            .ReturnsAsync(returnedPermission);

        _permissionRepositoryMock.Setup(item => item.HasUserOrRolePermissionAsync(It.IsAny<Guid>(), returnedPermission.Id, CancellationToken.None))
            .ReturnsAsync(false);

        //Act
        bool result = await _permissionService.HasPermissionAsync(Guid.NewGuid(), returnedPermission.Name);

        //Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task HasPermissionAsync_UserPermissionGrantOrInRole_ReturnsTrue()
    {
        //Arrange
        Permission? returnedPermission = _fixture.Build<Permission>()
            .Without(p => p.RolePermissions)
            .Without(p => p.UserPermissions)
            .Create();

        _permissionRepositoryMock.Setup(item => item.GetByNameAsync(It.IsAny<string>(), CancellationToken.None))
            .ReturnsAsync(returnedPermission);

        _permissionRepositoryMock.Setup(item => item.HasUserOrRolePermissionAsync(It.IsAny<Guid>(), returnedPermission.Id, CancellationToken.None))
            .ReturnsAsync(true);

        //Act
        bool result = await _permissionService.HasPermissionAsync(Guid.NewGuid(), returnedPermission.Name);

        //Assert
        result.Should().BeTrue();
    }
    #endregion
}
