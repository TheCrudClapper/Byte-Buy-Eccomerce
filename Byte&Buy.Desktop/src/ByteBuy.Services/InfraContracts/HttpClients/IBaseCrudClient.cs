using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.InfraContracts.HttpClients;

public interface IBaseCrudClient<PostRequestDto, PutRequestDto, ResponseDto, ListResponseDto>
{
    Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectListAsync();
    Task<Result<IEnumerable<ListResponseDto>>> GetList();
    Task<Result<ResponseDto>> GetByIdAsync(Guid resourceId);
    Task<Result<CreatedResponse>> PostConditionAsync(PostRequestDto request);
    Task<Result<UpdatedResponse>> PutCategoryAsync(Guid resourceId, PutRequestDto request);
    Task<Result> DeleteAsync(Guid resourceId);
}
