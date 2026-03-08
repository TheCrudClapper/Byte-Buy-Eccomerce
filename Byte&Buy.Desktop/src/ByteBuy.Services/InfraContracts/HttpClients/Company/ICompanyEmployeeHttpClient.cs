using ByteBuy.Services.DTO.Employee;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.InfraContracts.HttpClients.Company;

public interface ICompanyEmployeeHttpClient
{
    Task<Result<EmployeeProfileResponse>> GetEmployeeProfileData();
    Task<Result<UpdatedResponse>> PutEmployeeAddressAsync(EmployeeAddressUpdateRequest request);
    Task<Result<IEnumerable<EmployeeResponse>>> GetAllAsync();
    Task<Result<CreatedResponse>> PostEmployeeAsync(EmployeeAddRequest request);
    Task<Result<EmployeeResponse>> GetByIdAsync(Guid id);
    Task<Result<UpdatedResponse>> PutEmployeeAsync(Guid id, EmployeeUpdateRequest request);
    Task<Result> DeleteByIdAsync(Guid id);
    Task<Result<PagedList<EmployeeListResponse>>> GetListAsync(EmployeeListQuery query);
}