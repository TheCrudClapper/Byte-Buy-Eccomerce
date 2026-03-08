using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface IBaseService
{
    Task<Result> DeleteByIdAsync(Guid id);
}
