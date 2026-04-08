using Ardalis.Specification;
using AutoFixture;
using ByteBuy.Core.Domain.Permissions;
using ByteBuy.Core.Domain.Permissions.Errors;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.Domain.RepositoryContracts.UoW;
using ByteBuy.Core.DTO.Public.Permission;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.ServiceContracts;
using ByteBuy.Core.Services;
using FluentAssertions;
using Moq;
using static ByteBuy.Core.Specification.PermissionSpecifications;
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
        Permission? permission = null;
        Guid userId = Guid.NewGuid();
        _permissionRepositoryMock.Setup(item => item.GetByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(permission);

        //Act
        bool result = await _service.HasPermissionAsync(userId, "TestPermission");

        //Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task HasPermissionAsync_UserPermissionDenyOrAbsentInRole_ReturnsFalse()
    {
        //Arrange
        Permission permission = Permission.Create("test", "test").Value;
        Guid userId = Guid.NewGuid();

        _permissionRepositoryMock.Setup(item => item.GetByNameAsync(permission.Name, It.IsAny<CancellationToken>()))
            .ReturnsAsync(permission);

        _permissionRepositoryMock.Setup(item => item.HasUserOrRolePermissionAsync(userId, permission.Id))
            .ReturnsAsync(false);

        //Act
        bool result = await _service.HasPermissionAsync(userId, permission.Name);

        //Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task HasPermissionAsync_UserPermissionGrantOrInRole_ReturnsTrue()
    {
        //Arrange
        Permission permission = Permission.Create("test", "test").Value;
        Guid userId = Guid.NewGuid();

        _permissionRepositoryMock.Setup(item => item.GetByNameAsync(permission.Name, It.IsAny<CancellationToken>()))
            .ReturnsAsync(permission);

        _permissionRepositoryMock.Setup(item => item.HasUserOrRolePermissionAsync(userId, permission.Id))
            .ReturnsAsync(true);

        //Act
        bool result = await _service.HasPermissionAsync(userId, permission.Name);

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
    #region DeleteAsync Method Tests
    [Fact]
    public async Task DeleteAsync_HasActiveRelationsReturnsTrue_ReturnsFailure()
    {
        //Arrange
        var permissionId = Guid.NewGuid();
        _permissionRepositoryMock.Setup(p => p.HasActiveRelations(permissionId)).ReturnsAsync(true);

        //Act
        var result = await _service.DeleteAsync(permissionId);

        //Assert
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeEquivalentTo(PermissionErrors.HasActiveRelations);
    }

    [Fact]
    public async Task DeleteAsync_GetByIdReturnsNull_ReturnsResultFailure()
    {
        //Arrange
        Permission? permission = null;
        var permissionId = Guid.NewGuid();
        _permissionRepositoryMock.Setup(p => p.HasActiveRelations(permissionId)).ReturnsAsync(false);
        _permissionRepositoryMock.Setup(p => p.GetByIdAsync(permissionId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(permission);

        //Act
        var result = await _service.DeleteAsync(permissionId);

        //Assert
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeEquivalentTo(PermissionErrors.NotFound);
    }

    [Fact]
    public async Task DeleteAsync_PermissionExistsAndHasNoActiveRelations_ReturnsResultTrue()
    {
        //Arrange
        Permission permission = Permission.Create("test", "test").Value;
        _permissionRepositoryMock.Setup(p => p.HasActiveRelations(permission.Id)).ReturnsAsync(false);
        _permissionRepositoryMock.Setup(p => p.GetByIdAsync(permission.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(permission);

        //Act
        var result = await _service.DeleteAsync(permission.Id);

        //Assert
        result.IsSuccess.Should().BeTrue();
        permission.IsActive.Should().BeFalse();
        permission.DateDeleted.Should().NotBeNull();
    }
    #endregion
    #region GetByIdAsync Methods Test
    [Fact]
    public async Task GetByIdAsync_PermissionNotFound_ReturnsResultFailure()
    {
        //Arrange
        var permissionId = Guid.NewGuid();
        PermissionResponse? expected = null;
        _permissionRepositoryMock.Setup(p => p.GetBySpecAsync(It.IsAny<PermissionResponseSpec>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        //Act
        var result = await _service.GetByIdAsync(permissionId, CancellationToken.None);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeEquivalentTo(PermissionErrors.NotFound);
    }

    [Fact]
    public async Task GetByIdAsync_PermissionFound_ReturnsResultSuccess()
    {
        //Arrange
        var permissionId = Guid.NewGuid();
        PermissionResponse expected = new PermissionResponse(permissionId, "test", "test");
        _permissionRepositoryMock.Setup(p => p.GetBySpecAsync(It.IsAny<PermissionResponseSpec>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        //Act
        var result = await _service.GetByIdAsync(permissionId, CancellationToken.None);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(expected);
    }
    #endregion
}