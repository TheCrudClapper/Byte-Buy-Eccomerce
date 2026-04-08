using Ardalis.Specification;
using AutoFixture;
using ByteBuy.Core.Domain.Permissions;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.Domain.RepositoryContracts.UoW;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.ServiceContracts;
using ByteBuy.Core.Services;
using FluentAssertions;
using Moq;
namespace ByteBuy.ServiceTests;

public class PermissionServiceTests
{
    private readonly IPermissionService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly IPermissionRepository _permissionRepository;
    private readonly Mock<IPermissionRepository> _permissionRepositoryMock;
    private readonly IFixture _fixture;
    public PermissionServiceTests()
    {
        _permissionRepositoryMock = new Mock<IPermissionRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _permissionRepository = _permissionRepositoryMock.Object;
        _unitOfWork = _unitOfWorkMock.Object;

        _service = new PermissionService(_permissionRepository, _unitOfWork);
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
        bool result = await _service.HasPermissionAsync(Guid.NewGuid(), "TestPermission");

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

        _permissionRepositoryMock.Setup(item => item.HasUserOrRolePermissionAsync(It.IsAny<Guid>(), returnedPermission.Id))
            .ReturnsAsync(false);

        //Act
        bool result = await _service.HasPermissionAsync(Guid.NewGuid(), returnedPermission.Name);

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

        _permissionRepositoryMock.Setup(item => item.HasUserOrRolePermissionAsync(It.IsAny<Guid>(), returnedPermission.Id))
            .ReturnsAsync(true);

        //Act
        bool result = await _service.HasPermissionAsync(Guid.NewGuid(), returnedPermission.Name);

        //Assert
        result.Should().BeTrue();
    }
    #endregion

    #region GetSelectListAsync Method Tests
    [Fact]
    public async Task GetSelectListAsync_DBNotEmpty_ReturnsCollection()
    {
        //Arrange
        var collection = _fixture
          .CreateMany<SelectListItemResponse<Guid>>()
          .ToList();

        _permissionRepositoryMock
            .Setup(p => p.GetListBySpecAsync(
                It.IsAny<ISpecification<Permission, SelectListItemResponse<Guid>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(collection);

        //Act
        var result = await _service.GetSelectListAsync();

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(collection);
    }

    [Fact]
    public async Task GetSelectListAsync_DBEmpty_ReturnsEmptyCollection()
    {
        //Arrange
        List<SelectListItemResponse<Guid>> collection = [];

        _permissionRepositoryMock
            .Setup(p => p.GetListBySpecAsync(
                It.IsAny<ISpecification<Permission, SelectListItemResponse<Guid>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(collection);

        //Act
        var result = await _service.GetSelectListAsync();

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEmpty();
    }
    #endregion
}
