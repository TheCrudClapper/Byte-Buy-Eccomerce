using ByteBuy.Services.DTO.Auth;
using ByteBuy.Services.DTO.Employee;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface IEmployeeService : IBaseService
{
    Task<Result<PagedList<EmployeeListResponse>>> GetListAsync(EmployeeListQuery query);
    Task<Result<EmployeeProfileResponse>> GetCurrentEmployeeDetailsAsync();
    Task<Result> ChangePasswordAsync(PasswordChangeRequest request);
    Task<Result<UpdatedResponse>> UpdateCurrentEmployeeAddressAsync(EmployeeAddressUpdateRequest request);
    Task<Result<CreatedResponse>> AddAsync(EmployeeAddRequest request);
    Task<Result<UpdatedResponse>> UpdateAsync(Guid id, EmployeeUpdateRequest request);
    Task<Result<IReadOnlyCollection<EmployeeResponse>>> GetListAsync();
    Task<Result<EmployeeResponse>> GetByIdAsync(Guid id);
}