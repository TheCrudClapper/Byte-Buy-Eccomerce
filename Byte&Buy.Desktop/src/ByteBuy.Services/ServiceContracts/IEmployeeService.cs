using ByteBuy.Services.DTO.Auth;
using ByteBuy.Services.DTO.Employee;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface IEmployeeService : IBaseService
{
    Task<Result<PagedList<EmployeeListResponse>>> GetList(EmployeeListQuery query);
    Task<Result<EmployeeProfileResponse>> GetSelf();
    Task<Result> ChangePassword(PasswordChangeRequest request);
    Task<Result<UpdatedResponse>> UpdateAddress(EmployeeAddressUpdateRequest request);
    Task<Result<CreatedResponse>> Add(EmployeeAddRequest request);
    Task<Result<UpdatedResponse>> Update(Guid id, EmployeeUpdateRequest request);
    Task<Result<IEnumerable<EmployeeResponse>>> GetAll();
    Task<Result<EmployeeResponse>> GetById(Guid id);
}