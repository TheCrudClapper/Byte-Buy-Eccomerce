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
using Xunit.Abstractions;
using Xunit.Sdk;
using static ByteBuy.Core.Specification.PermissionSpecifications;
namespace ByteBuy.ServiceTests;

public class PermissionServiceTests
{
    private readonly IPermissionService _service;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IPermissionRepository> _permissionRepositoryMock;
    private readonly IFixture _fixture;
    public PermissionServiceTests()
    {
        _permissionRepositoryMock = new Mock<IPermissionRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _service = new PermissionService(_permissionRepositoryMock.Object, _unitOfWorkMock.Object);
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
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
    #endregion
    #region GetByIdAsync Methods Test
    [Fact]
    public async Task GetByIdAsync_PermissionNotFound_ReturnsFailureResult()
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
    public async Task GetByIdAsync_PermissionFound_ReturnsSuccessResult()
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
    #region AddASync Method Tests
    [Fact]
    public async Task AddAsync_ExistsWithGivenName_ReturnsFailureResult()
    {
        //Arrange
        PermissionAddRequest request = _fixture.Create<PermissionAddRequest>();
        _permissionRepositoryMock.Setup(p => p.ExistsWithNameAsync(It.IsAny<string>(), null))
            .ReturnsAsync(true);

        //Act
        var result = await _service.AddAsync(request);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeEquivalentTo(PermissionErrors.AlreadyExists);
    }

    [Fact]
    public async Task AddAsync_CreateReturnsFailure_ReturnsFailureResult()
    {
        //Arrange
        PermissionAddRequest request = new("", "test");
        _permissionRepositoryMock.Setup(p => p.ExistsWithNameAsync(It.IsAny<string>(), null))
            .ReturnsAsync(false);

        //Act
        var result = await _service.AddAsync(request);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeEquivalentTo(PermissionErrors.InvalidName);
    }

    [Fact]
    public async Task AddAsync_ValidInput_ReturnsSuccessResult()
    {
        //Arrange
        PermissionAddRequest request = new("test", "test");
        _permissionRepositoryMock.Setup(p => p.ExistsWithNameAsync(It.IsAny<string>(), null))
            .ReturnsAsync(false);

        //Act
        var result = await _service.AddAsync(request);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeOfType<CreatedResponse>();
        result.Value.DateCreated.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
        _permissionRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Permission>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
    #endregion
    #region UpdateAsync Method Tests
    [Fact]
    public async Task UpdateAsync_ExistsWithGivenName_ReturnsFailureResult()
    {
        //Arrange
        PermissionUpdateRequest request = new("test", "test");
        Guid permissionId = Guid.NewGuid();
        _permissionRepositoryMock.Setup(p => p.ExistsWithNameAsync(It.IsAny<string>(), It.IsAny<Guid>()))
            .ReturnsAsync(true);

        //Act
        var result = await _service.UpdateAsync(permissionId, request);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeEquivalentTo(PermissionErrors.AlreadyExists);
    }

    [Fact]
    public async Task UpdateAsync_NotFound_ReturnsFailureResult()
    {
        //Arrange
        PermissionUpdateRequest request = new("test", "test");
        Permission? permission = null;
        Guid permissionId = Guid.NewGuid();
        _permissionRepositoryMock.Setup(p => p.ExistsWithNameAsync(It.IsAny<string>(), It.IsAny<Guid>()))
            .ReturnsAsync(false);
        _permissionRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(permission);

        //Act
        var result = await _service.UpdateAsync(permissionId, request);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeEquivalentTo(PermissionErrors.NotFound);
    }

    [Fact]
    public async Task UpdateAsync_UpdateReturnsFailure_ReturnsFailure()
    {
        //Arrange
        PermissionUpdateRequest request = new("", "test");
        Permission permission = Permission.Create("test", "test").Value;
        _permissionRepositoryMock.Setup(p => p.ExistsWithNameAsync(It.IsAny<string>(), It.IsAny<Guid>()))
            .ReturnsAsync(false);
        _permissionRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(permission);

        //Act
        var result = await _service.UpdateAsync(permission.Id, request);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeEquivalentTo(PermissionErrors.InvalidName);
    }

    [Fact]
    public async Task UpdateAsync_ValidInput_ReturnsSuccessResult()
    {
        //Arrange
        PermissionUpdateRequest request = new("updated", "updated");
        Permission permission = Permission.Create("test", "test").Value;
        _permissionRepositoryMock.Setup(p => p.ExistsWithNameAsync(It.IsAny<string>(), It.IsAny<Guid>()))
            .ReturnsAsync(false);
        _permissionRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(permission);

        //Act
        var result = await _service.UpdateAsync(permission.Id, request);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeOfType<UpdatedResponse>();
        result.Value.Id.Should().Be(permission.Id);
        result.Value.DateEdited.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
    #endregion
    #region GetPermissionListAsync Methods Test
    [Fact]
    public async Task GetPermissionListAsync_DBEmpty_ReturnsEmptyCollection()
    {
        //Arrange
        IReadOnlyCollection<PermissionResponse> collection = [];
        _permissionRepositoryMock.Setup(p => p.GetPermissionListAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(collection);

        //Act
        var result = await _service.GetPermissionListAsync();

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEmpty();
    }
    [Fact]
    public async Task GetPermissionListAsync_DBNotEmpty_ReturnsCollection()
    {
        //Arrange
        IReadOnlyCollection<PermissionResponse> collection = _fixture.CreateMany<PermissionResponse>()
            .ToList();
        _permissionRepositoryMock.Setup(p => p.GetPermissionListAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(collection);

        //Act
        var result = await _service.GetPermissionListAsync();

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(collection);
    }
    #endregion
}